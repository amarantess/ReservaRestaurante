using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.UseCases.User.Update
{
	public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
	{
		public UpdateUserValidator()
		{
			RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
			RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

			When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
			{
				RuleFor(request => request.Email)
				.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
			});
		}
	}
}
