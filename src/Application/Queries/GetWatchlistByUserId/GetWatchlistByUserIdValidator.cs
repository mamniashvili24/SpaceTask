using Domain.Errors;
using FluentValidation;

namespace Application.Queries.GetWatchlistByUserId;

public class GetWatchlistByUserIdValidator : AbstractValidator<GetWatchlistByUserId>
{
	public GetWatchlistByUserIdValidator()
	{
		RuleFor(o => o.UserId).NotNull().NotEmpty().WithMessage(ErrorMessages.UserIdShouldNotBeNullOrEmpty);
	}
}