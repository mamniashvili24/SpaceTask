using FluentValidation;

namespace Application.Queries.GetFilmByName;

public class GetFilmByNameValidator : AbstractValidator<GetFilmByName>
{
    public GetFilmByNameValidator()
    {
        RuleFor(o => o.Name).NotEmpty().NotNull();
    }
}