using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Reservation;

namespace CommomTestUtilities.Repositories
{
    public class ReservationUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IReservationUpdateOnlyRepository> _repository;

		public ReservationUpdateOnlyRepositoryBuilder() => _repository = new Mock<IReservationUpdateOnlyRepository>();

        public ReservationUpdateOnlyRepositoryBuilder GetReservation(User user, long tableId, Reservation reservation)
        {
            _repository.Setup(x => x.GetReservation(user, tableId, reservation.ReservationDate)).ReturnsAsync(reservation);
            return this;
        }

        public IReservationUpdateOnlyRepository Build() => _repository.Object;
	}
}
