using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.UseCases.Table.Create
{
	public class CreateTableValidator : AbstractValidator<RequestCreateTable>
	{
		public CreateTableValidator()
		{
			RuleFor(table => table.Number)
				.GreaterThan(0).WithMessage(ResourceMessagesException.TABLE_NUMBER_INVALID);

			RuleFor(table => table.Capacity)
				.GreaterThan(0).WithMessage(ResourceMessagesException.TABLE_CAPACITY_INVALID);
		}
	}
}
