using Domain.Implementation;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;

namespace Application.Queries.GetFilmByName;

public class GetFilmByName : IQuery<IDataResponse<IEnumerable<SearchedFilm>>>
{
    public string Name { get; set; }
    public string LanguageCode { get; set; }
}