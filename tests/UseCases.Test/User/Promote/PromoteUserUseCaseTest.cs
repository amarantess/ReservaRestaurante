using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.Promote;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.User.Promote
{
	public class PromoteUserUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			(var user, _) = UserBuilder.Build();

			var request =  RequestPromoteBuilder.Build();
			request.Email = user.Email;

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => await useCase.Execute(request);

			await act.Should().NotThrowAsync();

			request.Email.Should().Be(user.Email);
		}

		[Fact]
		public async Task User_Already_Admin()
		{
			(var user, _) = UserBuilder.Build();
			user.Role = "Administrator";

			var request = RequestPromoteBuilder.Build();
			request.Email = user.Email;

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => await useCase.Execute(request);

			await act.Should().ThrowAsync<UserAlreadyAdmException>();
		}

		[Fact]
		public async Task User_NotFound()
		{
			(var user, _) = UserBuilder.Build();

			var request = RequestPromoteBuilder.Build();

			var useCase = CreateUseCase(user);

			Func<Task> act = async () => await useCase.Execute(request);

			await act.Should().ThrowAsync<ErrorOnValidationException>()
				.Where(e => e.ErrorMessages.Count == 1 &&
				e.ErrorMessages.Contains(ResourceMessagesException.USER_NOT_FOUND));
		}

		private static PromoteUserUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User user)
		{
			var updateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetByEmail(user).Build();
			var unitOfWork = UnitOfWorkBuilder.Build();

			var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
			if (!string.IsNullOrEmpty(user.Email))
			{
				readOnlyRepository.ExistUserWithEmail(user.Email);
			}

			if(user.Role == "Administrator")
			{
				readOnlyRepository.UserIsAdmin(user);
			}

			return new PromoteUserUseCase(readOnlyRepository.Build(), updateOnlyRepository, unitOfWork);
		}
	}
}
