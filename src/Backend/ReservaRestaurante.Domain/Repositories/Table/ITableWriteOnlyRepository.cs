namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableWriteOnlyRepository
	{
		public Task Add(Entities.Table table);
	}
}
