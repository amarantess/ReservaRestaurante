namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableReadOnlyRepository
	{
		public Task<bool> ExistTableWithNumber(int number);
		public Task<bool> ExistTableWithId(long id);
		public Task<List<Entities.Table>> ListTables();
		public Task<bool> IsCapacityValid(long tableId, int capacity);
		public Task<Entities.Table> GetTableByNumber(int tableNumber);
		public Task<List<int>> GetTablesNumber(List<Entities.Reservation> reservations);
	}
}
