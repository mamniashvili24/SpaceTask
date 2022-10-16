using Domain.Implementation;
using CQRS.Query.Abstraction;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserId : IQuery<IEnumerable<Watchlist>>
{
    public Guid UserId { get; set; }
}