using FluentValidation;
using ReservaRestaurante.Application.SharedValidators;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;

namespace ReservaRestaurante.Application.UseCases.Table.Update
{
	public class UpdateTableValidator : AbstractValidator<RequestUpdateTable>
	{
		private static readonly string[] ValidStatus = ["Available", "Reserved", "Inactive"];

		public UpdateTableValidator()
		{
			RuleFor(table => table.Capacity)
				.SetValidator(new CapacityValidator<RequestUpdateTable>());

			RuleFor(table => table.Status)
				.Must(status => ValidStatus.Contains(status)) //Verifica se o valor de 'status' corresponde a algum valor do array
				.WithMessage(ResourceMessagesException.TABLE_STATUS_INVALID);
		}
	}
}
