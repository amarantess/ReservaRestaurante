namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableReadOnlyRepository
	{
		public Task<bool> ExistTableWithNumber(int number);
		public Task<bool> ExistTableWithId(long id);
		public Task<List<Entities.Table>> ListTables();
	}
}
