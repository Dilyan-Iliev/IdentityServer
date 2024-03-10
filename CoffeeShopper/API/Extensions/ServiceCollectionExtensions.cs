using API.Services;
using DB.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDb(this IServiceCollection serviceCollection, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);

            string? connectionString = config?.GetConnectionString("Default");

            return serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);

            serviceCollection.AddScoped<ICoffeeShopService, CoffeeShopService>();

            return serviceCollection;
        }
    }
}
