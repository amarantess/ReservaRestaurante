using AutoMapper;
using ReservaRestaurante.Application.Services.AutoMapper;
using ReservaRestaurante.Application.Services.Criptography;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Infrastructure.DataAccess;

namespace ReservaRestaurante.Application.UseCases.User.Register
{
	public class RegisterUserUseCase : IRegisterUserUseCase
	{
		private readonly IUserWriteOnlyRepository _writeOnlyRepository;
		private readonly IUserReadOnlyRepository _readOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly PasswordEncripter _passwordEncripter;

		public RegisterUserUseCase(
			IUserWriteOnlyRepository writeOnlyRepository,
			IUserReadOnlyRepository readOnlyRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper,
			PasswordEncripter passwordEncripter)
		{
			_writeOnlyRepository = writeOnlyRepository;
			_readOnlyRepository = readOnlyRepository;
			_mapper = mapper;
			_passwordEncripter = passwordEncripter;
			_unitOfWork = unitOfWork;
		}

		public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser request)
		{
			// Validar
			await Validate(request);

			// Mapear a request em uma entidade
			var user = _mapper.Map<Domain.Entities.User>(request);

			// Criptografar senha
			user.Password = _passwordEncripter.Encrypt(request.Password);

			// Adicionar no DB
			await _writeOnlyRepository.Add(user);

			// Persistir no DB
			await _unitOfWork.Commit();

			return new ResponseRegisteredUser
			{
				Name = request.Name,
			};
		}

		private async Task Validate(RequestRegisterUser request)
		{
			var validator = new RegisterUserValidator();
			var result = validator.Validate(request);

			var emailExist = await _readOnlyRepository.ExistUserWithEmail(request.Email);
			if (emailExist)
			{
				result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
			}

			if (result.IsValid ==  false)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
