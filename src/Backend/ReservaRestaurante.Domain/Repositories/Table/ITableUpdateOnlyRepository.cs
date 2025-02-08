namespace ReservaRestaurante.Domain.Repositories.Table
{
	public interface ITableUpdateOnlyRepository
	{
		public Task<Entities.Table> GetById(long id);
		public void Update(Entities.Table table);
	}
}
