using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Token;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Token.RefreshToken
{
    public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
	{
		private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
		private readonly IRefreshTokenGenerator _refreshTokenGenerator;
		private readonly IAccessTokenGenerator _accessTokenGenerator;

		public UseRefreshTokenUseCase(
			ITokenRepository tokenRepository,
			IUnitOfWork unitOfWork, 
			IAccessTokenGenerator accessTokenGenerator,
			IRefreshTokenGenerator refreshTokenGenerator)
		{
			_tokenRepository = tokenRepository;
			_unitOfWork = unitOfWork;
			_accessTokenGenerator = accessTokenGenerator;
			_refreshTokenGenerator = refreshTokenGenerator;
		}

		public async Task<ResponseTokenJson> Execute(RequestNewTokenJson request)
		{
			var refreshToken = await _tokenRepository.Get(request.RefreshToken);

			if (refreshToken is null)
				throw new RefreshTokenNotFoundException();

			var refreshTokenValidUntil = refreshToken.CreatedOn.AddDays(7);
			if(DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0)
				throw new RefreshTokenExpiredException();

			var newRefreshToken = new Domain.Entities.RefreshToken
			{
				Value = _refreshTokenGenerator.Generate(),
				UserId = refreshToken.UserId
			};

			await _tokenRepository.SaveNewRefreshToken(newRefreshToken);
			await _unitOfWork.Commit();

			return new ResponseTokenJson
			{
				AccessToken = _accessTokenGenerator.Generate(refreshToken.User.UserIdentifier),
				RefreshToken = newRefreshToken.Value
			};
		}
	}
}
