using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Reservation.List;

namespace UseCases.Test.Reservation.List
{
    public class ListReservationUseCaseTest
    {
        [Fact]
        public async Task Succcess()
        {
			//Arrange
			(var user, _) = UserBuilder.Build();
			var reservation = ReservationBuilder.Build();
            var useCase = CreateUseCase(user);

			//Act
			var result = await useCase.Execute();

			//Assert
			result.Should().NotBeNull();
		}

		private static ListReservationUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.User user)
		{
			var loggedUser = LoggedUserBuilder.Build(user);
			var repository = new ReservationReadOnlyRepositoryBuilder();
			var mapper = MapperBuilder.Build();

			return new ListReservationUseCase(loggedUser, repository.Build(), mapper);
		}
	}
}
