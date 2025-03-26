using CommomTestUtilities.DateTimeConverter;
using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Reservation.Cancel;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Exceptions;

namespace UseCases.Test.Reservation.Cancel
{
    public class CancelReservationUseCaseTest
    {
		[Fact]
		public async Task Succcess()
		{
			//Arrange
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var reservation = ReservationBuilder.Build(table, user);
			var request = RequestCancelReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, reservation, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			await act.Should().NotThrowAsync();
		}

		[Fact]
		public async Task Error_Table_NotFound()
		{
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var reservation = ReservationBuilder.Build(table, user);
			var request = RequestCancelReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, reservation, table);
			request.TableNumber = 0;

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<NotFoundException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.TABLE_NOT_FOUND));
		}

		[Fact]
		public async Task Error_Reservation_NotFound()
		{
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var reservation = ReservationBuilder.Build(table, user);
			reservation.Id = 0;
			var request = RequestCancelReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, reservation, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<NotFoundException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.RESERVATION_NOT_FOUND));
		}

		[Fact]
		public async Task Error_Reservation_Already_Canceled()
		{
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var reservation = ReservationBuilder.Build(table, user);
			reservation.Status = "Canceled";
			var request = RequestCancelReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, reservation, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.RESERVATION_ALREADY_CANCELED));
		}

		private static CancelReservationUseCase CreateUseCase(
			ReservaRestaurante.Domain.Entities.User user,
			RequestCancelReservation request,
			ReservaRestaurante.Domain.Entities.Reservation reservation,
			ReservaRestaurante.Domain.Entities.Table table)
		{
			var loggedUser = LoggedUserBuilder.Build(user);
			var dateTimeConverter = DateTimeConverterBuilder.Build();
			var tableReadOnlyRepository = new TableReadOnlyRepositoryBuilder();
			var reservationReadOnlyRepository = new ReservationReadOnlyRepositoryBuilder();
			var reservationUpdateOnlyRepository = new ReservationUpdateOnlyRepositoryBuilder();

			var dateTime = dateTimeConverter.ConvertStringToDateTime(request.ReservationDateTime);

			tableReadOnlyRepository.GetTableIdByNumber(request.TableNumber, table);
			tableReadOnlyRepository.ExistTableWithNumber(request.TableNumber);
			reservationUpdateOnlyRepository.GetReservation(user, table.Id, reservation);

			if(reservation.Id > 0)
				reservationReadOnlyRepository.ExistReservation(user, table.Id, dateTime);

			if(reservation.Status == "Canceled")
				reservationReadOnlyRepository.ReservationAlreadyCanceled(user, table.Id, dateTime);

			return new CancelReservationUseCase(
				loggedUser, 
				dateTimeConverter, 
				tableReadOnlyRepository.Build(), 
				reservationReadOnlyRepository.Build(),
				reservationUpdateOnlyRepository.Build()
				);
		}
	}
}
