using Bogus;
using ReservaRestaurante.Domain.Entities;

namespace CommomTestUtilities.Entities
{
	public class TableBuilder
	{
		public static Table Build()
		{
			var table = new Faker<Table>()
				.RuleFor(table => table.Id, () => 1)
				.RuleFor(table => table.Number, (f) => f.Random.Int(1, 100))
				.RuleFor(table => table.Capacity, (f) => f.Random.Int(1, 20))
				.RuleFor(table => table.Status, () => "Available");

			return table;
		}
	}
}
