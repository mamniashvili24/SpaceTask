namespace CQRS.Query.Abstraction;

public interface IQueryBus
{
    Task<object> SendAsync(object query, CancellationToken cancellationToken = default);
    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}