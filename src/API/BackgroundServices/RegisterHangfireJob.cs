using Hangfire;
using CQRS.Query.Abstraction;
using Application.Queries.WatchlistRecuringJob;

namespace API.BackgroundServices;

public class RegisterHangfireJob : BackgroundService
{
    private readonly IServiceProvider _services;
    public RegisterHangfireJob(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = _services.CreateScope();

        var commandBus = serviceScope.ServiceProvider.GetService<IQueryBus>();
        RecurringJob.AddOrUpdate(() => commandBus.SendAsync(new WatchlistRecuringJob(), cancellationToken), Cron.Weekly(DayOfWeek.Sunday, 19, 30));
    }
}