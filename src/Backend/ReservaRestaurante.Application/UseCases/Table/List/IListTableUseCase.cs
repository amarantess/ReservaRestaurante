using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Table.List
{
	public interface IListTableUseCase
	{
		public Task<List<ResponseListTable>> Execute();
	}
}
