using MediatR;
using Domain.Implementation;
using CQRS.Command.Abstraction;
using Application.Helper.Abstraction;
using Infrastructure.Repositories.Abstraction;

namespace Application.Commands.AddFilmToWatchList;

public class AddFilmToWatchListHandler : ICommandHandler<AddFilmToWatchList>
{
    private readonly IImdbService _imdbService;
    private readonly IRepository<Infrastructure.Database.Entities.Watchlist> _watchlistRepository;

    public AddFilmToWatchListHandler(IImdbService imdbService, IRepository<Infrastructure.Database.Entities.Watchlist> watchlistRepository)
    {
        _imdbService = imdbService;
        _watchlistRepository = watchlistRepository;
    }
    public async Task<Unit> Handle(AddFilmToWatchList request, CancellationToken cancellationToken)
    {
        var result = await _imdbService.GetAsync<Film>("Title", request.FilmId);

        await _watchlistRepository.AddAsync(new Infrastructure.Database.Entities.Watchlist
        {
            Poster = result.Image,
            FilmId = request.FilmId,
            UserId = request.UserId,
            Title = result.FullTitle,
            Description = result.Plot,
            Rating = result.ImDbRating
        }, cancellationToken);

        await _watchlistRepository.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}