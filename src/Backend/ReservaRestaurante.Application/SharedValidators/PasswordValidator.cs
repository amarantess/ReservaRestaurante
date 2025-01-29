using FluentValidation;
using FluentValidation.Validators;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.SharedValidators
{
	//Estou passando a responsabilidade de quem utilizar a classe informar a requisição
	public class PasswordValidator<T> : PropertyValidator<T, string> // O tipo da propriedade que será validada
	{
		public override bool IsValid(ValidationContext<T> context, string password)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_EMPTY);

				return false;
			}

			if(password.Length < 6)
			{
				context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_INVALID);

				return false;
			}

			return true;
		}

		public override string Name => "PasswordValidator";

		// Essa função tem a responsabilidade de responder as mensagens
		protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
	}
}
