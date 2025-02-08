using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Table;

namespace CommomTestUtilities.Repositories
{
	public class TableUpdateOnlyRepositoryBuilder
	{
		private readonly Mock<ITableUpdateOnlyRepository> _repository;

		public TableUpdateOnlyRepositoryBuilder() => _repository = new Mock<ITableUpdateOnlyRepository>();

		public TableUpdateOnlyRepositoryBuilder GetById(long id, Table table)
		{
			if(table is not null)
				_repository.Setup(x => x.GetById(id)).ReturnsAsync(table);

			return this;
		}

		public ITableUpdateOnlyRepository Build() => _repository.Object;
	}
}
