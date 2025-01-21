using Moq;
using ReservaRestaurante.Domain.Repositories;

namespace CommomTestUtilities.Repositories
{
	public class UnitOfWorkBuilder
	{
		public static IUnitOfWork Build()
		{
			var mock = new Mock<IUnitOfWork>();

			return mock.Object;
		}
	}
}
