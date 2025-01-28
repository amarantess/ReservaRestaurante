using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.User.Update
{
	public interface IUpdateUserUseCase
	{
		public Task Execute(RequestUpdateUser request);
	}
}
