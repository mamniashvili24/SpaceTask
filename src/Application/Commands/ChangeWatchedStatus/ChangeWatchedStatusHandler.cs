using MediatR;
using Domain.Errors;
using CQRS.Command.Abstraction;
using Infrastructure.Database.Entities;
using Infrastructure.Repositories.Abstraction;

namespace Application.Commands.ChangeWatchedStatus;

public class ChangeWatchedStatusHandler : ICommandHandler<ChangeWatchedStatus>
{
    private readonly IRepository<Watchlist> _watchlistRepository;

    public ChangeWatchedStatusHandler(IRepository<Watchlist> watchlistRepository)
    {
        _watchlistRepository = watchlistRepository;
    }
    public async Task<Unit> Handle(ChangeWatchedStatus request, CancellationToken cancellationToken)
    {
        var film = await _watchlistRepository.FindOneAsync(watchlist => watchlist.Id == request.WatchlistId, cancellationToken);
        
        _ = film ?? throw new Exception(ErrorMessages.FilmNotExistInWatchList);            

        film.Type = (WatchlistType)Convert.ToInt32(request.IsWatched);

        await _watchlistRepository.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}