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
        try
        {
            var result = await _imdbService.GetAsync<IEnumerable<SearchedFilm>>("Search", request.LanguageCode, request.Name);

            return result;
        }
        catch (Exception ex)
        {
            return new DataResponse<IEnumerable<SearchedFilm>>(ex);
        }
    }
}