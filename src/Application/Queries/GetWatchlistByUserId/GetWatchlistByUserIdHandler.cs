using Domain.Abstraction;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;
using CommonTypes.Implementations;
using Infrastructure.Database.Entities;
using Infrastructure.Repositories.Abstraction;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserIdHandler : IQueryHandler<GetWatchlistByUserId, IDataResponse<IEnumerable<IWatchlist>>>
{
	private readonly IRepository<Watchlist> _watchlistRepository;

	public GetWatchlistByUserIdHandler(IRepository<Watchlist> watchlistRepository)
	{
		_watchlistRepository = watchlistRepository;
	}
    public async Task<IDataResponse<IEnumerable<IWatchlist>>> Handle(GetWatchlistByUserId request, CancellationToken cancellationToken)
    {
		try
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

			return new DataResponse<IEnumerable<IWatchlist>>(watchlists);
		}
		catch (Exception ex)
		{
			return new DataResponse<IEnumerable<IWatchlist>>(ex);
		}
    }
}
