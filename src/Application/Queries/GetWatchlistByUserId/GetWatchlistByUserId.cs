using Domain.Implementation;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserId : IQuery<IDataResponse<IEnumerable<Watchlist>>>
{
    public Guid UserId { get; set; }
}