using CQRS.Command.Abstraction;

namespace Application.Commands.ChangeWatchedStatus;

public class ChangeWatchedStatus : ICommand
{
    public Guid WatchlistId { get; set; }
    public bool IsWatched { get; set; }
}