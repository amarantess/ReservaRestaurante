using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Reservation;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class ReservationRepository : IReservationReadOnlyRepository, IReservationWriteOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public ReservationRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(Reservation reservation) => await _dbContext.Reservations.AddAsync(reservation);

		public async Task<bool> IsTableAvailable(long tableId, DateTime reservationDateTime)
		{
			return !await _dbContext.Reservations
				.AsNoTracking()
				.AnyAsync(reservation => 
				reservation.TableId == tableId &&
				reservation.ReservationDate == reservationDateTime);
		}
	}
}
