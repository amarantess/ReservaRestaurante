namespace ReservaRestaurante.Domain.Repositories.Reservation
{
    public interface IReservationUpdateOnlyRepository
    {
        public Task<Entities.Reservation> GetReservation(Entities.User user, long tableId, DateTime dateTime);
        public Task Update(Entities.Reservation reservation);
    }
}
