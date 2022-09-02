using Domain.Errors;
using CommonTypes.Abstractions;
using CQRS.Command.Abstraction;
using CommonTypes.Implementations;
using Infrastructure.Database.Entities;
using Infrastructure.Repositories.Abstraction;

namespace Application.Commands.ChangeWatchedStatus;

public class ChangeWatchedStatusHandler : ICommandHandler<ChangeWatchedStatus, IDataResponse>
{
    private readonly IRepository<Watchlist> _watchlistRepository;

    public ChangeWatchedStatusHandler(IRepository<Watchlist> watchlistRepository)
    {
        _watchlistRepository = watchlistRepository;
    }
    public async Task<IDataResponse> Handle(ChangeWatchedStatus request, CancellationToken cancellationToken)
    {
        try
        {
            var film = await _watchlistRepository.FindOneAsync(watchlist => watchlist.Id == request.WatchlistId, cancellationToken);
            if (film == null)
                return new DataResponse(ErrorMessages.FilmNotExistInWatchList);

            film.Type = (WatchlistType)Convert.ToInt32(request.IsWatched);

            await _watchlistRepository.SaveChangesAsync(cancellationToken);

            return new DataResponse();
        }
        catch (Exception ex)
        {
            return new DataResponse(ex);
        }
    }
}