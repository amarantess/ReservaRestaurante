using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.UseCases.User.Promote
{
	public class PromoteUserValidator : AbstractValidator<RequestPromoteUser>
	{
		public PromoteUserValidator()
		{
			RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

			When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
			{
				RuleFor(request => request.Email)
				.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
			});
		}
	}
}
