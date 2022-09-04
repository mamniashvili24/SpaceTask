using Domain.Implementation;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;
using CommonTypes.Implementations;
using Application.Helper.Abstraction;

namespace Application.Queries.GetFilmByName;

public class GetFilmByNameHandler : IQueryHandler<GetFilmByName, IDataResponse<IEnumerable<SearchedFilm>>>
{
    private readonly IImdbService _imdbService;

    public GetFilmByNameHandler(IImdbService imdbService)
    {
        _imdbService = imdbService;
    }
    public async Task<IDataResponse<IEnumerable<SearchedFilm>>> Handle(GetFilmByName request, CancellationToken cancellationToken)
    {
        return await _imdbService.GetAsync<IEnumerable<SearchedFilm>>("Search", request.LanguageCode, request.Name);
    }
}