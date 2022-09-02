namespace CQRS.Query.Implementation;

public class QueryBus : IQueryBus
{
    #region [ Private Field(s) ]

    private readonly IMediator _mediator;

    #endregion

    #region [ Constructor(s) ]

    public QueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region [ Public Method(s) ]

    public virtual async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);

        return result;
    }

    public virtual async Task<object> SendAsync(object query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return result;
    }

    #endregion
}