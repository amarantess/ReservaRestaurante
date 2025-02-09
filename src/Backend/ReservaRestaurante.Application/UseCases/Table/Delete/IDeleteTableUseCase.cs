namespace ReservaRestaurante.Application.UseCases.Table.Delete
{
	public interface IDeleteTableUseCase
	{
		public Task Execute(long id);
	}
}
