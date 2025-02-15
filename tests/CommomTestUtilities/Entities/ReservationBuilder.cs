using Bogus;
using ReservaRestaurante.Domain.Entities;

namespace CommomTestUtilities.Entities
{
    public class ReservationBuilder
    {
        public static Reservation Build()
        {
            return new Faker<Reservation>()
                .RuleFor(r => r.Id, () => 1)
                .RuleFor(r => r.ReservationDate, (f) => f.Date.Soon(1, DateTime.UtcNow))
                .RuleFor(r => r.Status, () => "Active")
                .RuleFor(r => r.TableId, (f) => f.Random.Int(1, 20))
                .RuleFor(r => r.UserId, (f) => f.Random.Int(1, 20));
        }
    }
}
