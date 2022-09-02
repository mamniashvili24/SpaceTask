namespace CQRS.FluentValidation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region [ Constructor(s) ]

    public ValidationBehavior(IValidatorFactory validationFactory)
    {
        _validationFactory = validationFactory;
    }

    #endregion

    #region [ Public Method(s) ]

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validator = _validationFactory.GetValidator(request.GetType());
        var result = validator?.Validate(new ValidationContext<TRequest>(request));

        if (!DoesValidationResultValid(result))
            throw new ValidationException(result?.Errors?.FirstOrDefault()?.ErrorMessage);

        var response = await next();

        return response;
    }

    #endregion

    #region [ Private Method(s) ]

    private static bool DoesValidationResultValid(ValidationResult result)
    {
        return result?.IsValid ?? false;
    }

    #endregion

    #region [ Private Field(s) ]

    private readonly IValidatorFactory _validationFactory;

    #endregion
}