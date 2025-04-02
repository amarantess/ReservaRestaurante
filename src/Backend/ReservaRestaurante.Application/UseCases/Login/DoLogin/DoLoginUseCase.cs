using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Cryptography;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Login.DoLogin
{
	public class DoLoginUseCase : IDoLoginUseCase
	{
		private readonly IUserReadOnlyRepository _repository;
		private readonly IPasswordEncripter _passwordEncripter;
		private readonly IAccessTokenGenerator _accessTokenGenerator;

		public DoLoginUseCase(
			IUserReadOnlyRepository repository, 
			IAccessTokenGenerator accessTokenGenerator,
			IPasswordEncripter passwordEncripter)
		{
			_repository = repository;
			_passwordEncripter = passwordEncripter;
			_accessTokenGenerator = accessTokenGenerator;
		}

		public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
		{
			var user = await _repository.GetByEmailReadOnly(request.Email);

			if(user is null || !_passwordEncripter.IsValid(request.Password, user.Password))
				throw new InvalidLoginException();

			return new ResponseRegisteredUser
			{
				Name = user.Name,
				Tokens = new ResponseToken
				{
					AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
				}
			};
		}
	}
}
