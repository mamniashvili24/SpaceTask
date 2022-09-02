namespace Domain.Abstraction;

public interface IWatchlist
{
    Guid Id { get; set; }
    Guid UserId { get; set; }
    string Title { get; set; }
    string FilmId { get; set; }
    string Poster { get; set; }
    decimal Rating { get; set; }
    string Description { get; set; }
    string WatchType { get; set; }
}