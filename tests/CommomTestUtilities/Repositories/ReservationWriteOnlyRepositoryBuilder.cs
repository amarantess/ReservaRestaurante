using Moq;
using ReservaRestaurante.Domain.Repositories.Reservation;

namespace CommomTestUtilities.Repositories
{
	public class ReservationWriteOnlyRepositoryBuilder
	{
		public static IReservationWriteOnlyRepository Build()
		{
			var mock = new Mock<IReservationWriteOnlyRepository>();

			return mock.Object;
		}
	}
}
