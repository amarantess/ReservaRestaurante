using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Table.List_Admin
{
	public interface IListTableAdminUseCase
	{
		public Task<List<ResponseTable>> Execute();
	}
}
