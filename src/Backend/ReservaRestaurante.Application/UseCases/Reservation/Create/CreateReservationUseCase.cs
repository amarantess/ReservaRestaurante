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
			//Validar
			await Validate(request);

			//Recuperar usuário
			var loggedUser = await _loggedUser.User();

			// Formatar a data da request em BR
			var dateConverted = _dateTimeConverter.ConvertStringToDateTime(request.ReservationDateTime);

			//Recuperando a mesa
			var table = await _tableReadOnlyRepository.GetTableByNumber(request.TableNumber);

			//Verificar se a capacidade da mesa corresponde
			var isValid = await _tableReadOnlyRepository.IsCapacityValid(table.Id, request.PeopleNumber);
			if (!isValid)
				throw new CapacityInvalidException();

			//Verificar se a mesa está disponivel no horario desejado
			var isAvailable = await _reservationReadOnlyRepository.IsTableAvailable(table.Id, dateConverted);
			if (!isAvailable)
				throw new TableIsNotAvailableException();

			//Mapear
			var reservation = new Domain.Entities.Reservation
			{
				UserId = loggedUser.Id,
				TableId = table.Id,
				ReservationDate = dateConverted
			};

			//Adicionar
			await _reservationWriteOnlyRepository.Add(reservation);

			//Persistir
			await _unitOfWork.Commit();

			//Response
			return new ResponseCreatedReservation
			{
				ReservationDateTime = reservation.ReservationDate
			};
		}

		private async Task Validate(RequestCreateReservation request)
		{
			var validator = new CreateReservationValidator();
			var result = validator.Validate(request);

			//Verificar se existe uma mesa com o mesmo número da request
			var existTable = await _tableReadOnlyRepository.ExistTableWithNumber(request.TableNumber);
			if (!existTable)
				throw new NotFoundException(ResourceMessagesException.TABLE_NOT_FOUND);

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
