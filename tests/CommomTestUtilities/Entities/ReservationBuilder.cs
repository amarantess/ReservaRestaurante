using Bogus;
using ReservaRestaurante.Domain.Entities;

namespace CommomTestUtilities.Entities
{
    public class ReservationBuilder
    {
        public static List<Reservation> BuildList(List<Table> tables, User user)
        {
            return new Faker<Reservation>()
                .RuleFor(r => r.Id, (f) => f.Random.Int(1, 5))
                .RuleFor(r => r.ReservationDate, (f) => f.Date.Soon(1, DateTime.UtcNow))
                .RuleFor(r => r.Status, () => "Active")
                .RuleFor(r => r.TableId, (f) => f.PickRandom(tables).Id)
                .RuleFor(r => r.UserId, () => user.Id)
                .Generate(5);
        }

        public static Reservation Build(Table table, User user)
        {
            return new Faker<Reservation>()
                .RuleFor(r => r.Id, (f) => f.Random.Int(1, 5))
                .RuleFor(r => r.ReservationDate, (f) =>
                {
                    var date = f.Date.Soon(1, DateTime.UtcNow);
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, DateTimeKind.Utc);
				})
                .RuleFor(r => r.Status, () => "Active")
                .RuleFor(r => r.TableId, () => table.Id)
                .RuleFor(r => r.UserId, () => user.Id);
		}
    }
}
