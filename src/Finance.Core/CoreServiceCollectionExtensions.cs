using System;
using Finance.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Core
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services) 
        {
            // Register dependencies
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
