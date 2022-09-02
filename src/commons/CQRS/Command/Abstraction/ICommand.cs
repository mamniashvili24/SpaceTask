namespace CQRS.Command.Abstraction;

public interface ICommand<out TResponse> : IRequest<TResponse> { }
public interface ICommand : IRequest { }