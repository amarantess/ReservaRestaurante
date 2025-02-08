using FluentValidation;
using FluentValidation.Validators;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.SharedValidators
{
	//Estou passando a responsabilidade de quem utilizar a classe informar a requisição
	public class CapacityValidator<T> : PropertyValidator<T, int> // O tipo da propriedade que será validada
	{
		public override bool IsValid(ValidationContext<T> context, int capacity)
		{
			if (capacity < 1)
			{
				context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.TABLE_CAPACITY_INVALID);

				return false;
			}

			return true;
		}

		public override string Name => "CapacityValidator";

		// Essa função tem a responsabilidade de responder as mensagens
		protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
	}
}
