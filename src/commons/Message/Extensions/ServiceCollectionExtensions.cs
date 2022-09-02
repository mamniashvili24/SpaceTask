using Message.Abstraction;
using Message.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Message.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessage(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}