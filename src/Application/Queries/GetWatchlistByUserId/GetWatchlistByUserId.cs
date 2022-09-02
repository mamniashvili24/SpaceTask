using Domain.Abstraction;
using CQRS.Query.Abstraction;
using CommonTypes.Abstractions;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserId : IQuery<IDataResponse<IEnumerable<IWatchlist>>>
{
    public Guid UserId { get; set; }
}