using CommomTestUtilities.DateTimeConverter;
using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Reservation.Create;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.Reservation.Create
{
	public class CreateResevationUseCaseTest
	{

		[Fact]
		public async Task Success()
		{
			//Arrange
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var request = RequestCreateReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, table);

			//Act
			var result = await useCase.Execute(request);

			//Assert
			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Error_Table_NotFound()
		{
			//Arrange
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var request = RequestCreateReservationBuilder.Build();
			request.TableNumber = 0;
			var useCase = CreateUseCase(user, request, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<NotFoundException>())
				.Where(exception => exception.Message.Equals(ResourceMessagesException.TABLE_NOT_FOUND));
		}

		[Fact]
		public async Task Error_Capacity_Invalid()
		{
			//Arrange
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			var request = RequestCreateReservationBuilder.Build();
			request.PeopleNumber = 10;
			var useCase = CreateUseCase(user, request, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<CapacityInvalidException>())
				.Where(exception => exception.Message.Equals(ResourceMessagesException.TABLE_NOT_SUPPORT));
		}

		[Fact]
		public async Task Error_Table_Not_Available()
		{
			//Arrange
			(var user, _) = UserBuilder.Build();
			var table = TableBuilder.Build();
			table.Id = 0;
			var request = RequestCreateReservationBuilder.Build();
			var useCase = CreateUseCase(user, request, table);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<TableIsNotAvailableException>())
				.Where(exception => exception.Message.Equals(ResourceMessagesException.TABLE_NOT_AVAILABLE));
		}

		private static CreateReservationUseCase CreateUseCase(
			ReservaRestaurante.Domain.Entities.User user, 
			RequestCreateReservation request,
			ReservaRestaurante.Domain.Entities.Table table)
		{
			var loggeduser = LoggedUserBuilder.Build(user);
			var tableReadOnlyRepository = new TableReadOnlyRepositoryBuilder();
			var reservationReadOnlyRepository = new ReservationReadOnlyRepositoryBuilder();
			var dateTimeConverter = DateTimeConverterBuilder.Build();
			var reservationWriteOnlyRepository = ReservationWriteOnlyRepositoryBuilder.Build();
			var unitOfWork = UnitOfWorkBuilder.Build();

			var converted = dateTimeConverter.ConvertStringToDateTime(request.ReservationDateTime);
			tableReadOnlyRepository.GetTableByNumber(request.TableNumber, table);
			
			if(request.TableNumber != 0)
			{
				tableReadOnlyRepository.ExistTableWithNumber(request.TableNumber);

				if (request.PeopleNumber <= 8)
					tableReadOnlyRepository.IsCapacityValid(table.Id, request.PeopleNumber);

				if(table.Id != 0)
					reservationReadOnlyRepository.IsTableAvailable(table.Id, converted);
			}


			return new CreateReservationUseCase(
				loggeduser, 
				tableReadOnlyRepository.Build(), 
				reservationReadOnlyRepository.Build(), 
				dateTimeConverter, 
				reservationWriteOnlyRepository, 
				unitOfWork);
		}
	}
}
