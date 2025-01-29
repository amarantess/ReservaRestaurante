using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.Update;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Infrastructure.Services;
using System.Reflection;

namespace UseCases.Test.User.Update
{
	public class UpdateUserUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			(var user, _) = UserBuilder.Build();

			var request = RequestUpdateUserBuilder.Build();

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => await useCase.Execute(request);

			await act.Should().NotThrowAsync();

			user.Name.Should().Be(request.Name);
			user.Email.Should().Be(request.Email);
		}

		[Fact]
		public async Task Error_Name_Empty()
		{
			(var user, _) = UserBuilder.Build();

			var request = RequestUpdateUserBuilder.Build();
			request.Name = string.Empty;

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => { await useCase.Execute(request); };

			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.ErrorMessages.Count == 1 &&
				e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));

			user.Name.Should().NotBe(request.Name);
			user.Email.Should().NotBe(request.Email);
		}

		[Fact]
		public async Task Error_Email_Already_Registered()
		{
			(var user, _) = UserBuilder.Build();

			var request = RequestUpdateUserBuilder.Build();

			var useCase = CreateUseCase(user, request.Email);

			Func<Task> act = async () => { await useCase.Execute(request); };

			await act.Should().ThrowAsync<ErrorOnValidationException>()
				.Where(e => e.ErrorMessages.Count == 1 &&
				e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

			user.Name.Should().NotBe(request.Name);
			user.Email.Should().NotBe(request.Email);
		}

		private static UpdateUserUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User user, string? email = null)
		{
			var unitOfWork = UnitOfWorkBuilder.Build();
			var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
			var loggedUser = LoggedUserBuilder.Build(user);
			var autoMapper = MapperBuilder.Build();

			var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
			if(!string.IsNullOrEmpty(email))
				userReadOnlyRepository.ExistUserWithEmail(email!);

			return new UpdateUserUseCase(loggedUser, userUpdateRepository, userReadOnlyRepository.Build(), autoMapper, unitOfWork);
		}
	}
}
