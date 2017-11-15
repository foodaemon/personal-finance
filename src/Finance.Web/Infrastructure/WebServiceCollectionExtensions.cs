using Microsoft.Extensions.DependencyInjection;
using Finance.Core.Services;
using Finance.Web.Infrastructure;

namespace Finance.Web.Infrastructure 
{
    public static class WebServiceCollectionExtensions 
    {
        public static IServiceCollection ConfigureWebServices(this IServiceCollection services) 
        {
            services.AddTransient<HangfireJobs>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITransactionService, TransactionService>();
            return services;
        }
    }
}