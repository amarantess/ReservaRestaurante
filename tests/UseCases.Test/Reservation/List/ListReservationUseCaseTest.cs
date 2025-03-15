using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
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
			var tables = TableBuilder.BuildList();
			var reservations = ReservationBuilder.BuildList(tables, user);
            var useCase = CreateUseCase(user, reservations, tables);

			//Act
			var result = await useCase.Execute();

			//Assert
			result.Should().NotBeNull();
			result.Select(response => response.TableNumber).Should().Equal(tables.Select(t => t.Number));
		}

		private static ListReservationUseCase CreateUseCase(
			ReservaRestaurante.Domain.Entities.User user, 
			List<ReservaRestaurante.Domain.Entities.Reservation> reservations,
			List<ReservaRestaurante.Domain.Entities.Table> tables)
		{
			var loggedUser = LoggedUserBuilder.Build(user);
			var reservationReadOnlyRepository = new ReservationReadOnlyRepositoryBuilder();
			var tableReadOnlyRepository = new TableReadOnlyRepositoryBuilder();

			reservationReadOnlyRepository.ListReservations(user.Id, reservations);
			tableReadOnlyRepository.GetTablesNumber(reservations, tables);

			return new ListReservationUseCase(loggedUser, reservationReadOnlyRepository.Build(), tableReadOnlyRepository.Build());
		}
	}
}
