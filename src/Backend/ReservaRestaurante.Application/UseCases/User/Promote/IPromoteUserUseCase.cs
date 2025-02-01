using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.User.Promote
{
	public interface IPromoteUserUseCase
	{
		public Task Execute(RequestPromoteUser request);
	}
}
