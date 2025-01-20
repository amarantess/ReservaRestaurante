using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.UseCases.User.Register
{
	public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
	{
		public RegisterUserValidator()
		{
			RuleFor(u => u.Name)
			.NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);

			RuleFor(u => u.Email)
				.NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

			RuleFor(u => u.Email)
				.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);

			RuleFor(u => u.Password.Length)
				.GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.EMAIL_INVALID);
		}
	}
}
