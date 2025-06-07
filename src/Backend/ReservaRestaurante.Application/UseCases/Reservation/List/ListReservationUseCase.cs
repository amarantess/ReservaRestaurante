using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.Reservation;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Reservation.List
{
	public class ListReservationUseCase : IListReservationUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;
		private readonly ITableReadOnlyRepository _tableReadOnlyRepository;

		public ListReservationUseCase(
			ILoggedUser loggedUser,
			IReservationReadOnlyRepository reservationReadOnlyRepository,
			ITableReadOnlyRepository tableReadOnlyRepository)
		{
			_loggedUser = loggedUser;
			_reservationReadOnlyRepository = reservationReadOnlyRepository;
			_tableReadOnlyRepository = tableReadOnlyRepository;
		}

		public async Task<List<ResponseListReservation>> Execute()
		{
			var user = await _loggedUser.User();

			var reservations = await _reservationReadOnlyRepository.ListReservations(user.Id);
			if (reservations is null || reservations.Count == 0)
				throw new NotFoundException("Reservations don't exist");

			var tablesNumber = await _tableReadOnlyRepository.GetTablesNumber(reservations);
			
			return reservations.Zip(tablesNumber, (reservation, tableNumber) => new ResponseListReservation
			{
				TableNumber = tableNumber,
				ReservationDate = reservation.ReservationDate.ToString("MM/dd/yyyy HH:mm"),
				Status = reservation.Status
			}).ToList();
		}
	}
}
