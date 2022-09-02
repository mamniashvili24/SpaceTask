using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.Repositories.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories.Implementation;

namespace Infrastructure;

public static class InfrastructureCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var contctionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Context>(options =>
                options.UseSqlServer(contctionString));

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        services.AddHangfire(x => x.UseSqlServerStorage(contctionString));
        services.AddHangfireServer(options => {

            options.ServerName = Environment.MachineName;

        });

        JobStorage.Current = new SqlServerStorage(contctionString);

        return services;
    }
}