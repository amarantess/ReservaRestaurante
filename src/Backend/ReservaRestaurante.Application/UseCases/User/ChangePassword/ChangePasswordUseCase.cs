using FluentValidation.Results;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Cryptography;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.User.ChangePassword
{
	public class ChangePasswordUseCase : IChangePasswordUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IUserUpdateOnlyRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPasswordEncripter _passwordEncripter;

		public ChangePasswordUseCase(
			ILoggedUser loggedUser,
			IUserUpdateOnlyRepository repository,
			IUnitOfWork unitOfWork,
			IPasswordEncripter passwordEncripter)
		{
			_loggedUser = loggedUser;
			_repository = repository;
			_unitOfWork = unitOfWork;
			_passwordEncripter = passwordEncripter;
		}

		public async Task Execute(RequestChangePassword request)
		{
			var loggedUser = await _loggedUser.User(); // Recupera o usuário logado

			Validate(request, loggedUser); // Faz a validação

			var user = await _repository.GetById(loggedUser.Id); // Recupera o usuário no banco de dados para poder fazer alterações

			user.Password = _passwordEncripter.Encrypt(request.NewPassword); // Recebe a nova senha criptografada

			_repository.Update(user);

			await _unitOfWork.Commit();
		}

		private void Validate(RequestChangePassword request, Domain.Entities.User loggedUser)
		{
			var result = new ChangePasswordValidator().Validate(request);

			if (!_passwordEncripter.IsValid(request.Password, loggedUser.Password)) // Se a senha atual for diferente da passada na request
			{
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
			}

			if (!result.IsValid)
			{
				throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
			}
		}
	}
}
