using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestCancelReservationBuilder
    {
        public static RequestCancelReservation Build()
        {
            return new Faker<RequestCancelReservation>()
                .RuleFor(r => r.TableNumber, (f) => f.Random.Int(1, 20))
                .RuleFor(r => r.ReservationDateTime, (f) => f.Date.Soon(1, DateTime.UtcNow).ToString("MM/dd/yyyy HH:mm"));
        }
    }
}
