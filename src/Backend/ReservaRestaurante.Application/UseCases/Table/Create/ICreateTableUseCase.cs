using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Table.Create
{
	public interface ICreateTableUseCase
	{
		public Task<ResponseCreatedTable> Execute(RequestCreateTable request);
	}
}
