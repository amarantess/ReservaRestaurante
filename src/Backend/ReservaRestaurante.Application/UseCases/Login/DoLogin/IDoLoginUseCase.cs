using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Login.DoLogin
{
	public interface IDoLoginUseCase
	{
		public Task<ResponseRegisteredUser> Execute(RequestLogin request);
	}
}
