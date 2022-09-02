namespace CQRS.Command.Implementation;

public class CommandBus : ICommandBus
{
    #region [ Private Field(s) ]

    private readonly IMediator _mediator;

    #endregion

    #region [ Constructor(s) ]

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region [ Public Method(s) ]

    public virtual async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result;
    }
    
    public virtual async Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
    }

    public virtual async Task SendAsync(object command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
    }

    public virtual async Task<object> SendAsync<TResponse>(object command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result;
    }

    #endregion
}