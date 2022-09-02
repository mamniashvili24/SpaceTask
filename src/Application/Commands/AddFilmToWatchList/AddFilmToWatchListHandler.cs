using Domain.Implementation;
using CommonTypes.Abstractions;
using CQRS.Command.Abstraction;
using CommonTypes.Implementations;
using Application.Helper.Abstraction;
using Infrastructure.Repositories.Abstraction;

namespace Application.Commands.AddFilmToWatchList;

public class AddFilmToWatchListHandler : ICommandHandler<AddFilmToWatchList, IDataResponse>
{
	private readonly IImdbService _imdbService;
	private readonly IRepository<Infrastructure.Database.Entities.Watchlist> _watchlistRepository;

	public AddFilmToWatchListHandler(IImdbService imdbService, IRepository<Infrastructure.Database.Entities.Watchlist> watchlistRepository)
	{
		_imdbService = imdbService;
		_watchlistRepository = watchlistRepository;
	}
    public async Task<IDataResponse> Handle(AddFilmToWatchList request, CancellationToken cancellationToken)
    {
		try
		{
            var result = await _imdbService.GetAsync<Film>("Title", request.LanguageCode, request.FilmId);
            if (result.IsError)
				return new DataResponse(result.Message);

			await _watchlistRepository.AddAsync(new Infrastructure.Database.Entities.Watchlist
			{
				FilmId = request.FilmId,
				UserId = request.UserId,
                Poster = result.Data.Image,
                Title = result.Data.FullTitle,
				Description = result.Data.Plot,
                Rating = result.Data.ImDbRating
            }, cancellationToken);

			await _watchlistRepository.SaveChangesAsync(cancellationToken);

            return new DataResponse();
        }
        catch (Exception ex)
		{
			return new DataResponse(ex);
		}
    }
}