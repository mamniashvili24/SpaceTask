using Domain.Errors;
using FluentValidation;

namespace Application.Commands.AddFilmToWatchList;

public class AddFilmToWatchListValidator : AbstractValidator<AddFilmToWatchList>
{
    public AddFilmToWatchListValidator()
    {
        RuleFor(o => o.UserId).NotNull().NotEmpty().WithMessage(ErrorMessages.UserIdShouldNotBeNullOrEmpty);
        RuleFor(o => o.FilmId).NotNull().NotEmpty().WithMessage(ErrorMessages.FilmIdShouldNotBeNullOrEmpty);
    }
}