namespace CQRS.Event.Implementation;

public class EventBus : IEventBus
{
    #region [ Private Field(s) ]

    private readonly IMediator _mediator;

    #endregion

    #region [ Constructor(s) ]

    public EventBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region [ Public Method(s) ]

    public async Task PublishAsync<T>(T @event) where T : IEvent
    {
        await _mediator.Publish(@event);
    }
 
    #endregion
}