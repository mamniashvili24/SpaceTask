using CQRS.Query.Abstraction;
using CQRS.Command.Abstraction;
using Microsoft.AspNetCore.Mvc;
using CommonTypes.Abstractions;
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
 
    private string GetLanguage(HttpRequest httpRequest)
    {
        var defaultName = "en";

        var languageHeadervalue = httpRequest.Headers["Accept-Language"].ToString();

        if (string.IsNullOrEmpty(languageHeadervalue))
            return defaultName;

        return languageHeadervalue;
    }
    private void SetLanguageCodeToCommand<TCommand>(TCommand command)
                                                                    where TCommand : class, ICommand<IDataResponse>
    {
        var languageCodeProperty = typeof(TCommand).GetProperties().FirstOrDefault(o => o.Name.ToLower() == _languagecode);
        if (languageCodeProperty != null)
        {
            var languageCode = GetLanguage(HttpContext.Request);
            languageCodeProperty.SetValue(command, languageCode);
        }
    }
    private void SetLanguageCodeToQuery<TQuery, TResponse>(TQuery query)
                                                                    where TQuery : class, IQuery<IDataResponse<TResponse>>
    {
        var languageCodeProperty = typeof(TQuery).GetProperties().FirstOrDefault(o => o.Name.ToLower() == _languagecode);
        if (languageCodeProperty != null)
        {
            var languageCode = GetLanguage(HttpContext.Request);
            languageCodeProperty.SetValue(query, languageCode);
        }
    }
    protected async Task<IActionResult> ProccessQueryAsync<TQuery, TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                                                                                     where TRequest : class
                                                                                     where TQuery : class, IQuery<IDataResponse<TResponse>>
    {
        var query = Mapping.Map<TRequest, TQuery>(request);
        SetLanguageCodeToQuery<TQuery, TResponse>(query);
        var response = await _queryBus.SendAsync(query, cancellationToken);

        return ReturnResponse(response);
    }
    protected async Task<IActionResult> ProccessCommandAsync<TCommand, TRequest>(TRequest request, CancellationToken cancellationToken = default)
                                                                                             where TRequest : class
                                                                                             where TCommand : class, ICommand<IDataResponse>
    {
        var command = Mapping.Map<TRequest, TCommand>(request);

        SetLanguageCodeToCommand(command);
        var response = await _commandBus.SendAsync(command);

        return ReturnResponse(response);
    }
    protected IActionResult ReturnResponse<T>(IDataResponse<T> response)
    {
        if (response.IsError)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    protected IActionResult ReturnResponse(IDataResponse response)
    {
        if (response.IsError)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}