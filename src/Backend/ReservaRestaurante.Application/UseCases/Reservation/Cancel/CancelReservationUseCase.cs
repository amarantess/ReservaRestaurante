using ReservaRestaurante.Application.Services.DateTimeConverter;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Repositories.Reservation;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Reservation.Cancel
{
	public class CancelReservationUseCase : ICancelReservationUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IDateTimeConverter _dateTimeConverter;
		private readonly ITableReadOnlyRepository _tableReadOnlyRepository;
		private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;
		private readonly IReservationUpdateOnlyRepository _reservationUpdateOnlyRepository;

		public CancelReservationUseCase(
			ILoggedUser loggedUser,
			IDateTimeConverter dateTimeConverter,
			ITableReadOnlyRepository tableReadOnlyRepository,
			IReservationReadOnlyRepository reservationReadOnlyRepository,
			IReservationUpdateOnlyRepository reservationUpdateOnlyRepository)
		{
			_loggedUser = loggedUser;
			_dateTimeConverter = dateTimeConverter;
			_tableReadOnlyRepository = tableReadOnlyRepository;
			_reservationReadOnlyRepository = reservationReadOnlyRepository;
			_reservationUpdateOnlyRepository = reservationUpdateOnlyRepository;
		}

		public async Task Execute(RequestCancelReservation request)
		{
			var user = await _loggedUser.User();

			var dateTime = _dateTimeConverter.ConvertStringToDateTime(request.ReservationDateTime);
			
			await Validate(request, user, dateTime);

			var tableId = await _tableReadOnlyRepository.GetTableIdByNumber(request.TableNumber);

			var reservation = await _reservationUpdateOnlyRepository.GetReservation(user, tableId, dateTime);
			await _reservationUpdateOnlyRepository.Update(reservation);
		}

		private async Task Validate(RequestCancelReservation request, Domain.Entities.User user, DateTime dateTime)
		{
			var validator = new CancelReservationValidator();
			var result = validator.Validate(request);

			var existTable = await _tableReadOnlyRepository.ExistTableWithNumber(request.TableNumber);
			if (!existTable)
				throw new NotFoundException(ResourceMessagesException.TABLE_NOT_FOUND);

			var tableId = await _tableReadOnlyRepository.GetTableIdByNumber(request.TableNumber);

			var existReservation = await _reservationReadOnlyRepository.ExistReservation(user, tableId, dateTime);
			if (!existReservation)
				throw new NotFoundException(ResourceMessagesException.RESERVATION_NOT_FOUND);

			var IsCanceled = await _reservationReadOnlyRepository.ReservationAlreadyCanceled(user, tableId, dateTime);
			if(IsCanceled)
				result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.RESERVATION_ALREADY_CANCELED));

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
