using CQRS.Command.Abstraction;

namespace Application.Commands.AddFilmToWatchList;

public class AddFilmToWatchList : ICommand
{
    public Guid UserId { get; set; }
    public string FilmId { get; set; }
}