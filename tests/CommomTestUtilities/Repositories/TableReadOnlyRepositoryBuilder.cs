using Moq;
using ReservaRestaurante.Domain.Entities;
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

		public void GetTableByNumber(int tableNumber, Table table)
		{
			_repository.Setup(x => x.GetTableByNumber(tableNumber)).ReturnsAsync(table);
		}

		public void IsCapacityValid(int tableNumber, int capacity)
		{
			_repository.Setup(x => x.IsCapacityValid(tableNumber, capacity)).ReturnsAsync(true);
		}

		public void GetTablesNumber(List<Reservation> reservations, List<Table> tables)
		{
			var tablesNumber = tables.Select(t => t.Number).Distinct().ToList();

			_repository.Setup(x => x.GetTablesNumber(reservations)).ReturnsAsync(tablesNumber);
		}

		public void GetTableIdByNumber(int tableNumber, Table table)
		{
			_repository.Setup(x => x.GetTableIdByNumber(tableNumber)).ReturnsAsync(table.Id);
		}

		public ITableReadOnlyRepository Build() => _repository.Object;
	}
}
