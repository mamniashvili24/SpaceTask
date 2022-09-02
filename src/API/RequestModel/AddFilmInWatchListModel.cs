namespace API.RequestModel;

public class AddFilmInWatchListModel
{
    public Guid UserId { get; set; }
    public string FilmId { get; set; }
}