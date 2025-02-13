namespace ReservaRestaurante.Domain.Repositories.Reservation
{
	public interface IReservationWriteOnlyRepository
	{
		public Task Add(Entities.Reservation reservation);
	}
}
