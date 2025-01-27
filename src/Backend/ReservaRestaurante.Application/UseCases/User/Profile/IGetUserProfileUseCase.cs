using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.User.Profile
{
	public interface IGetUserProfileUseCase
	{
		public Task<ResponseUserProfile> Execute();
	}
}
