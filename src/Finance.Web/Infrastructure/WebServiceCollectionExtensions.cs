using Microsoft.Extensions.DependencyInjection;
using Finance.Core.Services;

namespace Finance.Web.Infrastructure 
{
    public static class WebServiceCollectionExtensions 
    {
        public static IServiceCollection ConfigureWebServices(this IServiceCollection services) 
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITransactionService, TransactionService>();
            return services;
        }
    }
}