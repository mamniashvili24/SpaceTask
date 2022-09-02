namespace CQRS.Query.Abstraction;

public interface IQuery<out TResponse> : IRequest<TResponse> { }