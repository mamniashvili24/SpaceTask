using CommonTypes.Abstractions;
using CQRS.Command.Abstraction;

namespace Application.Commands.AddFilmToWatchList;

public class AddFilmToWatchList : ICommand<IDataResponse>
{
    public Guid UserId { get; set; }
    public string FilmId { get; set; }
    public string LanguageCode { get; set; }
}