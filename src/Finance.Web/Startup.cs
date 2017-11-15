using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Finance.Core;
using Finance.Web.Infrastructure;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.Redis;
using StackExchange.Redis;

namespace Finance.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // create policy to only allow authenticated users.
            var defaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            
            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(Configuration.GetConnectionString("Redis"));
            });
            
            services.AddAuthentication(o => 
            {
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options => 
            {
                options.LoginPath = "/Auth/Signin/";
                options.AccessDeniedPath = "/Error/Forbidden/";
                options.LogoutPath = "/Auth/Signout/";
            });

            services.AddMvc();

			// Register Services (Dependency Injection)
            services.ConfigureCoreServices();
            services.ConfigureWebServices();
			
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
            }
            
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // RecurringJob.AddOrUpdate(() => HangfireJobs.PrintToDebug($@"Hangfire recurring task started - {Guid.NewGuid()}"), Cron.Minutely);
        }
    }
}
