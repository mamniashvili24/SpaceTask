namespace Mapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services, params Type[] types)
    {
        services.AddAutoMapper(types);

        return services;
    }

    public static IServiceCollection AddMapperWithProfiles(this IServiceCollection services)
    {
        var currentAsembly = Assembly.GetCallingAssembly();
        var profiles = currentAsembly.GetTypes().Where(o => o.BaseType == typeof(MapperProfile)).Select(o => o).ToArray();
        if (profiles != null && profiles.Length != 0)
            services.AddMapper(profiles);

        return services;
    }
}