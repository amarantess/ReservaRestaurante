using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Domain.Repositories.Reservation
{
	public interface IReservationReadOnlyRepository
	{
		public Task<bool> IsTableAvailable(long tableId, DateTime reservationDateTime);
		public Task<List<Entities.Reservation>> ListReservations(long userId);
		public Task<bool> ExistReservation(Entities.User user, long tableId, DateTime reservationDateTime);
		public Task<bool> ReservationAlreadyCanceled(Entities.User user,long tableId, DateTime reservationDateTime);
	}
}
