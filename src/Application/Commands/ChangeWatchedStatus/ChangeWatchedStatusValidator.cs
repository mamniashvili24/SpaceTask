using Domain.Errors;
using FluentValidation;

namespace Application.Commands.ChangeWatchedStatus;

public class ChangeWatchedStatusValidator : AbstractValidator<ChangeWatchedStatus>
{
    public ChangeWatchedStatusValidator()
    {
        RuleFor(o => o.IsWatched).NotNull().NotEmpty().WithMessage(ErrorMessages.IsWatchedShouldNotBeNullOrEmpty);
        RuleFor(o => o.WatchlistId).NotNull().NotEmpty().WithMessage(ErrorMessages.WatchlistIdShouldNotBeNullOrEmpty);
    }
}