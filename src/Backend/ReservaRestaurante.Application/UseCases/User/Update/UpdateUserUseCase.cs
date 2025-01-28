using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Services.LoggedUser;
using FluentValidation.Results;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using AutoMapper;

namespace ReservaRestaurante.Application.UseCases.User.Update
{
	public class UpdateUserUseCase : IUpdateUserUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
		private readonly IUserReadOnlyRepository _userReadOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateUserUseCase(
			ILoggedUser loggedUser,
			IUserUpdateOnlyRepository userUpdateOnlyRepository,
			IUserReadOnlyRepository userReadOnlyRepository,
			IUnitOfWork unitOfWork)
		{
			_loggedUser = loggedUser;
			_userUpdateOnlyRepository = userUpdateOnlyRepository;
			_userReadOnlyRepository = userReadOnlyRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(RequestUpdateUser request)
		{
			var loggedUser = await _loggedUser.User(); // Recuperando usuário logado

			await Validate(request, loggedUser.Email); // Faz a validação

			var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id); // Busco o usuário no banco de dados

			user.Name = request.Name;
			user.Email = request.Email;

			_userUpdateOnlyRepository.Update(user);

			await _unitOfWork.Commit();
		}

		private async Task Validate(RequestUpdateUser request, string currentEmail)
		{
			var validator = new UpdateUserValidator();

			var result = validator.Validate(request);

			if (!currentEmail.Equals(request.Email)) // Se o email atual for diferente da requisição
			{
				var userExist = await _userReadOnlyRepository.ExistUserWithEmail(request.Email); // Verifica se já existe um usuário com o email da requisição
				if (userExist)
					result.Errors.Add(new ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED)); // Se existir lança um erro
			}

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
