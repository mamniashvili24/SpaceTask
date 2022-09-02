namespace CQRS.Command.Abstraction;

public interface ICommandBus
{
    Task SendAsync(object command, CancellationToken cancellationToken = default(CancellationToken));
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default(CancellationToken));
    Task<object> SendAsync<TResponse>(object command, CancellationToken cancellationToken = default(CancellationToken));
    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default(CancellationToken));
}