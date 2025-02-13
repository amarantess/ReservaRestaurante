using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestCreateReservationBuilder
	{
		public static RequestCreateReservation Build()
		{
			return new Faker<RequestCreateReservation>()
				.RuleFor(reservation => reservation.TableNumber, (f) => f.Random.Int(1, 20))
				.RuleFor(reservation => reservation.ReservationDateTime, (f) => f.Date.Soon(1, DateTime.UtcNow).ToString("MM/dd/yyyy HH:mm"))
				.RuleFor(reservation => reservation.PeopleNumber, (f) => f.Random.Int(2, 8));
		}
	}
}
