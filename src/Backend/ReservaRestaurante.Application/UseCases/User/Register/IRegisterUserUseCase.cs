using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.User.Register
{
	public interface IRegisterUserUseCase
	{
		public Task<ResponseRegisteredUser> Execute(RequestRegisterUser request);
	}
}
