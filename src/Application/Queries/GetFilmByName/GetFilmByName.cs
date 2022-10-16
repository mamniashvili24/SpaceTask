using Domain.Implementation;
using CQRS.Query.Abstraction;

namespace Application.Queries.GetFilmByName;

public class GetFilmByName : IQuery<IEnumerable<SearchedFilm>>
{
    public string Name { get; set; }
}