namespace CQRS.Query.Abstraction;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse> { }