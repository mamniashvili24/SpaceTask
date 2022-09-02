using Message.Abstraction;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;
using CommonTypes.Implementations;
using Infrastructure.Database.Entities;
using Infrastructure.Repositories.Abstraction;

namespace Application.Queries.WatchlistRecuringJob;

public class WatchlistRecuringJobHandler : IQueryHandler<WatchlistRecuringJob, IDataResponse>
{
    private readonly IRepository<Watchlist> _watchlistRepository;
    private readonly IMailService _maileService;

    public WatchlistRecuringJobHandler(IRepository<Watchlist> watchlistRepository, IMailService maileService)
    {
        _maileService = maileService;
        _watchlistRepository = watchlistRepository;
    }
    public async Task<IDataResponse> Handle(WatchlistRecuringJob request, CancellationToken cancellationToken)
    {
        try
        {
            var allWatchlist = await _watchlistRepository.GetAllAsync(cancellationToken);
            var userIds = allWatchlist.Select(o => o.UserId).Distinct();

            foreach (var userId in userIds)
            {
                if (allWatchlist.Count(o => o.UserId == userId && o.Type == WatchlistType.Unwanted) < 3)
                    continue;

                var film = allWatchlist.OrderByDescending(o => o.Rating).FirstOrDefault(o => o.UserId == userId && o.Type == WatchlistType.Unwanted);
                if (film != null)
                {
                    var doesEmailSend = await SendMailAsync(film);
                    if (doesEmailSend)
                        UpdateFilmType(film);
                }
            }
            await _watchlistRepository.SaveChangesAsync(cancellationToken);

            return new DataResponse();
        }
        catch (Exception ex)
        {
            return new DataResponse(ex);
        }
    }

    private void UpdateFilmType(Watchlist? film)
    {
        film.Type = WatchlistType.Recommended;
        _watchlistRepository.Update(film);
    }

    private async Task<bool> SendMailAsync(Watchlist film)
    {
        var body = $"Title = {film.Title}, rating = {film.Rating}, Poster = {film.Poster}, short description = {film.Description}";
        var doesEmailSend = await _maileService.SendEmailAsync("mamniashvili24@gmail.com", "password", "smtp.gmail.com", 587, body, "mamniashvili24@gmail.com", "Film");

        return doesEmailSend;
    }
}