using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.User.ChangePassword
{
	public interface IChangePasswordUseCase
	{
		public Task Execute(RequestChangePassword request);
	}
}
