namespace Infrastructure.Database.Entities;

public class Watchlist
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string FilmId { get; set; }
    public string Poster { get; set; }
    public decimal Rating { get; set; }
    public string Description { get; set; }
    public WatchlistType Type { get; set; }
}