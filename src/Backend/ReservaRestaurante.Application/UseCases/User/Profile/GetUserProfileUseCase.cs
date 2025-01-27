using AutoMapper;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Services.LoggedUser;

namespace ReservaRestaurante.Application.UseCases.User.Profile
{
	public class GetUserProfileUseCase : IGetUserProfileUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IMapper _mapper;

		public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
		{
			_loggedUser = loggedUser;
			_mapper = mapper;
		}

		public async Task<ResponseUserProfile> Execute()
		{
			var user = await _loggedUser.User();

			return _mapper.Map<ResponseUserProfile>(user);
		}
	}
}
