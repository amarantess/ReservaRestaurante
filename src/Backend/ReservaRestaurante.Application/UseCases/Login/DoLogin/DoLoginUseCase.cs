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
			//Criptografa a senha recebida
			var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

			//Compara as informações                                                           //Se o que estiver a esquerda dos "??" for nulo, irá executar o que estiver a direita.
			var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

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
