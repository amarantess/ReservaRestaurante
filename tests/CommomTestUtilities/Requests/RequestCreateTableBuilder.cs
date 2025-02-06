using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestCreateTableBuilder
	{
		public static RequestCreateTable Build()
		{
			return new Faker<RequestCreateTable>()
				.RuleFor(table => table.Number, (f) => f.Random.Int(1, 100))
				.RuleFor(table => table.Capacity, (f) => f.Random.Int(1, 20));
		}
	}
}
