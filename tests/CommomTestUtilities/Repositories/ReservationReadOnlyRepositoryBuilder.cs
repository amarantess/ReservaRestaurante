using Moq;
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

		public IReservationReadOnlyRepository Build() => _repository.Object;
	}
}
