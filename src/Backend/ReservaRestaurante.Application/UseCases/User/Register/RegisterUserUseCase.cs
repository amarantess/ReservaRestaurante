using AutoMapper;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Token;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Cryptography;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.User.Register
{
	public class RegisterUserUseCase : IRegisterUserUseCase
	{
		private readonly IUserWriteOnlyRepository _writeOnlyRepository;
		private readonly IUserReadOnlyRepository _readOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IAccessTokenGenerator _accessTokenGenerator;
		private readonly IPasswordEncripter _passwordEncripter;
		private readonly IRefreshTokenGenerator _refreshTokenGenerator;
		private readonly ITokenRepository _tokenRepository;

		public RegisterUserUseCase(
			IUserWriteOnlyRepository writeOnlyRepository,
			IUserReadOnlyRepository readOnlyRepository,
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IAccessTokenGenerator accessTokenGenerator,
			IPasswordEncripter passwordEncripter,
			IRefreshTokenGenerator refreshTokenGenerator,
			ITokenRepository tokenRepository)
		{
			_writeOnlyRepository = writeOnlyRepository;
			_readOnlyRepository = readOnlyRepository;
			_mapper = mapper;
			_accessTokenGenerator = accessTokenGenerator;
			_passwordEncripter = passwordEncripter;
			_unitOfWork = unitOfWork;
			_refreshTokenGenerator = refreshTokenGenerator;
			_tokenRepository = tokenRepository;
		}

		public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser request)
		{
			await Validate(request);

			var user = _mapper.Map<Domain.Entities.User>(request);
			user.Password = _passwordEncripter.Encrypt(request.Password);
			user.UserIdentifier = Guid.NewGuid();

			await _writeOnlyRepository.Add(user);
			await _unitOfWork.Commit();

			var refreshToken = await CreateAndSaveRefreshToken(user);

			return new ResponseRegisteredUser
			{
				Name = user.Name,
				Tokens = new ResponseTokenJson
				{
					AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
					RefreshToken = refreshToken
				}
			};
		}

		private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
		{
			var refreshToken = new Domain.Entities.RefreshToken
			{
				Value = _refreshTokenGenerator.Generate(),
				UserId = user.Id
			};

			await _tokenRepository.SaveNewRefreshToken(refreshToken);
			await _unitOfWork.Commit();

			return refreshToken.Value;
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

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
