using CQRS.Query.Abstraction;
using CQRS.Command.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Mapper.MapperConfiguration;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    private const string _languagecode = "languagecode";

    public BaseController(ICommandBus commandBus, IQueryBus queryBus)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
    }
 
    protected async Task<IActionResult> ProccessQueryAsync<TQuery, TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                                                                                     where TRequest : class
                                                                                     where TQuery : class, IQuery<TResponse>
    {
        var query = Mapping.Map<TRequest, TQuery>(request);
        var response = await _queryBus.SendAsync(query, cancellationToken);

        return Ok(response);
    }
    protected async Task<IActionResult> ProccessCommandAsync<TCommand, TRequest>(TRequest request, CancellationToken cancellationToken = default)
                                                                                             where TRequest : class
                                                                                             where TCommand : class, ICommand
    {
        var command = Mapping.Map<TRequest, TCommand>(request);
        await _commandBus.SendAsync(command, cancellationToken);

        return Ok();
    }
}