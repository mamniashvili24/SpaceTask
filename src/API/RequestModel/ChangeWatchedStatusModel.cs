namespace API.RequestModel;

public class ChangeWatchedStatusModel
{
    public bool IsWatched { get; set; }
    public Guid WatchlistId { get; set; }
}