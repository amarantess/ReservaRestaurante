namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableReadOnlyRepository
	{
		public Task<bool> ExistTableWithNumber(int number);
	}
}
