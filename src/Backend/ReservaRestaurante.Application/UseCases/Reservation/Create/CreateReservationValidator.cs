using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;
using System.Globalization;

namespace ReservaRestaurante.Application.UseCases.Reservation.Create
{
	public class CreateReservationValidator : AbstractValidator<RequestCreateReservation>
	{
		public CreateReservationValidator()
		{
			RuleFor(r => r.TableNumber)
				.GreaterThan(0).WithMessage(ResourceMessagesException.TABLE_NUMBER_INVALID);

			RuleFor(r => r.PeopleNumber)
				.GreaterThan(0).WithMessage(ResourceMessagesException.PEOPLE_NUMBER_INVALID);

			RuleFor(r => r.ReservationDateTime)
				.Must(BeValidDate).WithMessage(ResourceMessagesException.DATE_TIME_FORMAT_INVALID);

			When(r => BeValidDate(r.ReservationDateTime), () =>
			{
				RuleFor(r => r.ReservationDateTime)
					.Must(BeFutureDate).WithMessage(ResourceMessagesException.DATE_TIME_INVALID);
			});
		}

		// Valida se a string é uma data válida no formato esperado
		private bool BeValidDate(string dateStr)
		{
			return DateTime.TryParseExact(dateStr, "MM/dd/yyyy HH:mm",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
		}

		// Valida se a data é futura
		private bool BeFutureDate(string dateStr)
		{
			if (DateTime.TryParseExact(dateStr, "MM/dd/yyyy HH:mm",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
			{
				return parsedDate > DateTime.Now; // Deve ser maior que agora
			}
			return false;
		}
	}
}
