using CQRS;
using Infrastructure;
using Message.Extensions;
using Application.Helper;
using Application.Helper.Abstraction;
using Application.Helper.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddCQRSWithFluentValidation();


        var baseUrl = configuration.GetValue<string>(HttpClientStrings.IMDBClient);
        services.AddHttpClient(HttpClientStrings.IMDBClient, client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });

        var section = configuration.GetSection(HttpClientStrings.ImdbClientApiKey);
        services.Configure<ImdbClientKey>(section);

        services.AddScoped<IImdbService, ImdbService>();
        services.AddMessage();

        return services;
    }
}