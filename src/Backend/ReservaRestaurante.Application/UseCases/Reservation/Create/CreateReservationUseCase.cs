using FluentValidation.Results;
using ReservaRestaurante.Application.Services.DateTimeConverter;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Reservation;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Reservation.Create
{
	public class CreateReservationUseCase : ICreateReservationUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly ITableReadOnlyRepository _tableReadOnlyRepository;
		private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;
		private readonly IDateTimeConverter _dateTimeConverter;
		private readonly IReservationWriteOnlyRepository _reservationWriteOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateReservationUseCase(
			ILoggedUser loggedUser,
			ITableReadOnlyRepository tableReadOnlyRepository,
			IReservationReadOnlyRepository reservationReadOnlyRepository,
			IDateTimeConverter dateTimeConverter,
			IReservationWriteOnlyRepository writeOnlyRepository,
			IUnitOfWork unitOfWork)
		{
			_loggedUser = loggedUser;
			_tableReadOnlyRepository = tableReadOnlyRepository;
			_reservationReadOnlyRepository = reservationReadOnlyRepository;
			_dateTimeConverter = dateTimeConverter;
			_reservationWriteOnlyRepository = writeOnlyRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<ResponseCreatedReservation> Execute(RequestCreateReservation request)
		{
			(var table, var dateConverted) = await Validate(request);

			var loggedUser = await _loggedUser.User();

			var reservation = new Domain.Entities.Reservation
			{
				UserId = loggedUser.Id,
				TableId = table.Id,
				ReservationDate = dateConverted
			};

			await _reservationWriteOnlyRepository.Add(reservation);
			await _unitOfWork.Commit();

			return new ResponseCreatedReservation
			{
				ReservationDateTime = reservation.ReservationDate
			};
		}

		private async Task<(Domain.Entities.Table, DateTime dateConverted)> Validate(RequestCreateReservation request)
		{
			var validator = new CreateReservationValidator();
			var result = validator.Validate(request);

			var existTable = await _tableReadOnlyRepository.ExistTableWithNumber(request.TableNumber);
			if (!existTable)
				throw new NotFoundException(ResourceMessagesException.TABLE_NOT_FOUND);

			var isValid = await _tableReadOnlyRepository.IsCapacityValid(request.TableNumber, request.PeopleNumber);
			if (!isValid)
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.TABLE_NOT_SUPPORT));

			var table = await _tableReadOnlyRepository.GetTableByNumber(request.TableNumber);
			var dateConverted = _dateTimeConverter.ConvertStringToDateTime(request.ReservationDateTime);

			var isAvailable = await _reservationReadOnlyRepository.IsTableAvailable(table.Id, dateConverted);
			if (!isAvailable)
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.TABLE_NOT_AVAILABLE));

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}

			return (table, dateConverted);
		}
	}
}
