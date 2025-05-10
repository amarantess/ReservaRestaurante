using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace ReservaRestaurante.Application.UseCases.User.Promote
{
	public class PromoteUserUseCase : IPromoteUserUseCase
	{
		private readonly IUserReadOnlyRepository _userReadOnlyRepository;
		private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PromoteUserUseCase(
			IUserReadOnlyRepository userReadOnlyRepository,
			IUserUpdateOnlyRepository userUpdateOnlyRepository,
			IUnitOfWork unitOfWork)
		{
			_userReadOnlyRepository = userReadOnlyRepository;
			_userUpdateOnlyRepository = userUpdateOnlyRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(RequestPromoteUser request)
		{
			await Validate(request);

			var user  = await _userUpdateOnlyRepository.GetByEmail(request.Email);
			user.Role = "Administrator";

			_userUpdateOnlyRepository.Update(user);
			await _unitOfWork.Commit();
		}

		private async Task Validate(RequestPromoteUser request)
		{
			var validator = new PromoteUserValidator();

			var result = validator.Validate(request);

			var userExist = await _userReadOnlyRepository.ExistUserWithEmail(request.Email);
			if (!userExist)
			{
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.USER_NOT_FOUND));
			}
			else
			{
				var user = await _userUpdateOnlyRepository.GetByEmail(request.Email);
				var isAdmin = await _userReadOnlyRepository.UserIsAdmin(user);
				if (isAdmin)
					result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.USER_ALREADY_ADM));
			}

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
