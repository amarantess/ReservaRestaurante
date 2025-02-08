using Moq;
using ReservaRestaurante.Domain.Repositories.Table;

namespace CommomTestUtilities.Repositories
{
	public class TableReadOnlyRepositoryBuilder
	{
		private readonly Mock<ITableReadOnlyRepository> _repository;

		public TableReadOnlyRepositoryBuilder() => _repository = new Mock<ITableReadOnlyRepository>();

		public void ExistTableWithNumber(int number)
		{
			_repository.Setup(x => x.ExistTableWithNumber(number)).ReturnsAsync(true);
		}

		public void ExistTableWithId(long id)
		{
			_repository.Setup(x => x.ExistTableWithId(id)).ReturnsAsync(true);
		}

		public ITableReadOnlyRepository Build() => _repository.Object;
	}
}
