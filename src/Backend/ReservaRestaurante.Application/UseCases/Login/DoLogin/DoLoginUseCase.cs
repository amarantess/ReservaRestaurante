using ReservaRestaurante.Application.Services.Criptography;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Login.DoLogin
{
	public class DoLoginUseCase : IDoLoginUseCase
	{
		private readonly IUserReadOnlyRepository _repository;
		private readonly PasswordEncripter _passwordEncripter;

		public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter)
		{
			_repository = repository;
			_passwordEncripter = passwordEncripter;
		}

		public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
		{
			//Criptografa a senha recebida
			var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

			//Compara as informações                                                           //Se o que estiver a esquerda dos "??" for nulo, irá executar o que estiver a direita.
			var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

			return new ResponseRegisteredUser
			{
				Name = user.Name
			};
		}
	}
}
