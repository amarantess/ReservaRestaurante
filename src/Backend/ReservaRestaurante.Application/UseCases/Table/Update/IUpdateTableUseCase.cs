using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.Table.Update
{
	public interface IUpdateTableUseCase
	{
		public Task Execute(long tableId, RequestUpdateTable request);
	}
}
