using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Login.DoLogin;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.Login.DoLogin
{
	public class DoLoginUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			(var user, var password) = UserBuilder.Build();

			var useCase = CreateUseCase(user);

			//Act
			var result = await useCase.Execute(new RequestLogin
			{
				Email = user.Email,
				Password = password
			});

			//Assert
			result.Should().NotBeNull();
			result.Tokens.Should().NotBeNull();
			result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
			result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task Error_Invalid_User()
		{
			//Arrange
			var request = RequestLoginBuilder.Build();
			var useCase = CreateUseCase();

			//Act
			Func<Task> act = async () => { await useCase.Execute(request); };

			//Assert
			await act.Should().ThrowAsync<InvalidLoginException>()
				.Where(error => error.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
		}

		private static DoLoginUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User? user = null)
		{
			var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
			var passwordEncripter = PasswordEncripterBuilder.Build();
			var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

			if (user is not null)
				readRepositoryBuilder.GetByEmailAndPassword(user);

			return new DoLoginUseCase(readRepositoryBuilder.Build(), accessTokenGenerator, passwordEncripter);
		}
	}
}
