using CQRS.Query.Abstraction;
using Infrastructure.Database.Entities;
using Infrastructure.Repositories.Abstraction;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserIdHandler : IQueryHandler<GetWatchlistByUserId, IEnumerable<Domain.Implementation.Watchlist>>
{
    private readonly IRepository<Watchlist> _watchlistRepository;

    public GetWatchlistByUserIdHandler(IRepository<Watchlist> watchlistRepository)
    {
        _watchlistRepository = watchlistRepository;
    }
    public async Task<IEnumerable<Domain.Implementation.Watchlist>> Handle(GetWatchlistByUserId request, CancellationToken cancellationToken)
    {
        var watchlists = _watchlistRepository.Find(watchlist => watchlist.UserId == request.UserId)
            .Select(watchlist => new Domain.Implementation.Watchlist
            {
                Id = watchlist.Id,
                Title = watchlist.Title,
                FilmId = watchlist.FilmId,
                UserId = watchlist.UserId,
                WatchType = watchlist.Type.ToString()
            });

        return watchlists;
    }
}