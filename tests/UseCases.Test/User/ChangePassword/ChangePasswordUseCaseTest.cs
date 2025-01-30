using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.ChangePassword;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.User.ChangePassword
{
	public class ChangePasswordUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			(var user, var password) = UserBuilder.Build(); // Crio um usuário e uma senha antes da criptografia
			
			var request = RequestChangePasswordBuilder.Build();
			request.Password = password; // Altero a senha que o usuário me passou pela senha ja criada antes da criptografia

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => await useCase.Execute(request);

			await act.Should().NotThrowAsync();

			var passwordEncripter = PasswordEncripterBuilder.Build();

			user.Password.Should().Be(passwordEncripter.Encrypt(request.NewPassword)); // A senha do usuário deveria ser igual a nova senha criptografada
		}

		[Fact]
		public async Task Error_NewPassword_Empty()
		{
			(var user, var password) = UserBuilder.Build();

			var request = new RequestChangePassword
			{
				Password = password,
				NewPassword = string.Empty
			};

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => { await useCase.Execute(request); };

			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.ErrorMessages.Count() == 1 &&
				e.ErrorMessages.Contains(ResourceMessagesException.PASSWORD_EMPTY));

			var passwordEncripter = PasswordEncripterBuilder.Build();

			user.Password.Should().Be(passwordEncripter.Encrypt(password)); // Garantir que a senha é a mesma
		}

		[Fact]
		public async Task Error_CurrentPassword_Different()
		{
			(var user, var password) = UserBuilder.Build();

			var request = RequestChangePasswordBuilder.Build();

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => { await useCase.Execute(request); };

			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.ErrorMessages.Count() == 1 &&
				e.ErrorMessages.Contains(ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

			var passwordEncripter = PasswordEncripterBuilder.Build();

			user.Password.Should().Be(passwordEncripter.Encrypt(password)); // Garantir que a senha é a mesma
		}

		private static ChangePasswordUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User user)
		{
			var loggedUser = LoggedUserBuilder.Build(user);
			var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
			var unitOfWork = UnitOfWorkBuilder.Build();
			var passwordEncripter = PasswordEncripterBuilder.Build();

			return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwordEncripter);
		}
	}
}
