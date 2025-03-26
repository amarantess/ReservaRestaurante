using FluentValidation;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;
using System.Globalization;

namespace ReservaRestaurante.Application.UseCases.Reservation.Cancel
{
    public class CancelReservationValidator : AbstractValidator<RequestCancelReservation>
    {
		public CancelReservationValidator()
		{
			RuleFor(r => r.TableNumber)
				.GreaterThan(0).WithMessage(ResourceMessagesException.TABLE_NUMBER_INVALID);

			RuleFor(r => r.ReservationDateTime)
				.Must(BeValidDate).WithMessage(ResourceMessagesException.DATE_TIME_FORMAT_INVALID);
		}

		private static bool BeValidDate(string dateStr)
		{
			return DateTime.TryParseExact(dateStr, "MM/dd/yyyy HH:mm",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
		}
	}
}
