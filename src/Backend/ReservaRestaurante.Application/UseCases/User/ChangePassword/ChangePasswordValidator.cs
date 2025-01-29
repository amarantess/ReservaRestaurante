using FluentValidation;
using ReservaRestaurante.Application.SharedValidators;
using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.User.ChangePassword
{
	public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
	{
		public ChangePasswordValidator()
		{
			RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePassword>());
		}
	}
}
