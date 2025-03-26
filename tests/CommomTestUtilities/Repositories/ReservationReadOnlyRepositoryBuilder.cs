using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Reservation;

namespace CommomTestUtilities.Repositories
{
	public class ReservationReadOnlyRepositoryBuilder
	{
		private readonly Mock<IReservationReadOnlyRepository> _repository;

		public ReservationReadOnlyRepositoryBuilder() => _repository = new Mock<IReservationReadOnlyRepository>();

		public void IsTableAvailable(long tableId, DateTime reservationDateTime)
		{
			_repository.Setup(x => x.IsTableAvailable(tableId, reservationDateTime)).ReturnsAsync(true);
		}

		public void ListReservations(long userId, List<Reservation> reservation)
		{
			_repository.Setup(x => x.ListReservations(userId)).ReturnsAsync(reservation);
		}

		public void ExistReservation(User user, long tableId, DateTime reservation)
		{
			_repository.Setup(x => x.ExistReservation(user, tableId, reservation)).ReturnsAsync(true);
		}

		public void ReservationAlreadyCanceled(User user, long tableId, DateTime reservation)
		{
			_repository.Setup(x => x.ReservationAlreadyCanceled(user, tableId, reservation)).ReturnsAsync(true);
		}

		public IReservationReadOnlyRepository Build() => _repository.Object;
	}
}
