using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.Register;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.User.Register
{
	public class RegisterUserUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			var request = RequestRegisterUserBuilder.Build();

			var useCase = CreateUseCase();
			
			//Act
			var result = await useCase.Execute(request);

			//Assert
			result.Should().NotBeNull();
			result.Tokens.Should().NotBeNull();
			result.Name.Should().Be(request.Name);
			result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task Error_Email_Already_Registered()
		{
			//Arrange
			var request = RequestRegisterUserBuilder.Build();

			var useCase = CreateUseCase(request.Email);

			//Act | Salvando a função dentro da var act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(error => error.ErrorMessages.Count == 1 && error.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
		}

		[Fact]
		public async Task Error_Name_Empty()
		{
			//Arrange
			var request = RequestRegisterUserBuilder.Build();
			request.Name = string.Empty;

			var useCase = CreateUseCase();

			//Act | Salvando a função dentro da var act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(error => error.ErrorMessages.Count == 1 && error.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));
		}

		private static RegisterUserUseCase CreateUseCase(string? email = null)
		{
			var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
			var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
			var unitOfWork = UnitOfWorkBuilder.Build();
			var mapper = MapperBuilder.Build();
			var passwordEncripter = PasswordEncripterBuilder.Build();
			var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

			if (string.IsNullOrEmpty(email) == false)
				readRepositoryBuilder.ExistUserWithEmail(email);

			return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), unitOfWork, mapper, accessTokenGenerator, passwordEncripter);
		}
	}
}
