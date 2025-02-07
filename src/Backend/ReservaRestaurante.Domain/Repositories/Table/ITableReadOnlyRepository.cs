using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableReadOnlyRepository
	{
		public Task<bool> ExistTableWithNumber(int number);
		public Task<List<Entities.Table>> ListTables();
	}
}
