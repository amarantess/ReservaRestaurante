namespace ReservaRestaurante.Domain.Repositories.Reservation
{
	public interface IReservationReadOnlyRepository
	{
		public Task<bool> IsTableAvailable(long tableId, DateTime reservationDateTime);
		public Task<List<Entities.Reservation>> ListReservations(long userId);
	}
}
