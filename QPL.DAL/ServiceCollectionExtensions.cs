using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QPL.DAL.EntityFramework.Context;

namespace QPL.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQplDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

            return services;
        }
    }
}
