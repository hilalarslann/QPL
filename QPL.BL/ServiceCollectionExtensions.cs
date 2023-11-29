using Microsoft.Extensions.DependencyInjection;
using QPL.BL.Token;

namespace QPL.BL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQplBusinessLayer(this IServiceCollection services)
        {
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<BaseHandlerRequest>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITokenHandler, TokenHandler>();

            return services;
        }
    }
}
