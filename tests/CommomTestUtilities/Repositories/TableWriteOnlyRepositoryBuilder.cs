using Moq;
using ReservaRestaurante.Domain.Repositories.Table;

namespace CommomTestUtilities.Repositories
{
	public class TableWriteOnlyRepositoryBuilder
	{
		public static ITableWriteOnlyRepository Build()
		{
			var mock = new Mock<ITableWriteOnlyRepository>();

			return mock.Object;
		}
	}
}
