using Bogus;
using ReservaRestaurante.Domain.Entities;

namespace CommomTestUtilities.Entities
{
	public class TableBuilder
	{
		public static Table Build()
		{
			return new Faker<Table>()
				.RuleFor(table => table.Id, () => 1)
				.RuleFor(table => table.Number, (f) => f.Random.Int(1, 20))
				.RuleFor(table => table.Capacity, (f) => f.Random.Int(1, 20))
				.RuleFor(table => table.Status, () => "Available");
		}

		public static List<Table> BuildList()
		{
			return new Faker<Table>()
				.RuleFor(table => table.Id, (f) => f.Random.Int(1, 5))
				.RuleFor(table => table.Number, (f) => f.UniqueIndex + 1)
				.RuleFor(table => table.Capacity, (f) => f.Random.Int(1, 20))
				.RuleFor(table => table.Status, () => "Available")
				.Generate(5);
		}
	}
}
