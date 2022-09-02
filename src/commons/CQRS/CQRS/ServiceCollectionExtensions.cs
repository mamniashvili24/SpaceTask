namespace CQRS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.AddMediatR(assembly);

        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<ICommandBus, CommandBus>();

        return services;
    }

    public static IServiceCollection AddCQRSWithFluentValidation(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.AddMediatR(assembly);

        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped<ICommandBus, CommandBus>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblies(new[] { assembly }); });

        return services;
    }
}