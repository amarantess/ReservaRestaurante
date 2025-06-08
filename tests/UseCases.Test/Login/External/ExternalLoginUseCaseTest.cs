using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Login.External;
using ReservaRestaurante.Domain.Entities;

namespace UseCases.Test.Login.External
{
	public class ExternalLoginUseCaseTest
	{
		[Fact]
		public async Task Success_User_Dont_Exist()
		{
			(var user, _) = UserBuilder.Build();

			var useCase = CreateUseCase();

			var result = await useCase.Execute(user.Name, user.Email);

			result.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task Success_User_Exist()
		{
			(var user, _) = UserBuilder.Build();

			var useCase = CreateUseCase(user);

			var result = await useCase.Execute(user.Name, user.Email);

			result.Should().NotBeNullOrEmpty();
		}

		private static ExternalLoginUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User? user = null)
		{
			var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
			var writeRepositoryBuilder = UserWriteOnlyRepositoryBuilder.Build();
			var unitOfWork = UnitOfWorkBuilder.Build();
			var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

			if (user is not null)
				readRepositoryBuilder.GetByEmailReadOnly(user);

			return new ExternalLoginUseCase(readRepositoryBuilder.Build(), writeRepositoryBuilder, unitOfWork, accessTokenGenerator);
		}
	}
}
