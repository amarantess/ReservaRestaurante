using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestUpdateTableBuilder
	{
		private static readonly string[] ValidStatus = ["Available", "Reserved", "Inactive"];

		public static RequestUpdateTable Build()
		{
			return new Faker<RequestUpdateTable>()
				.RuleFor(table => table.Capacity, (f) => f.Random.Int(1, 20))
				.RuleFor(table => table.Status, (f) => f.PickRandom(ValidStatus));
		}
	}
}
