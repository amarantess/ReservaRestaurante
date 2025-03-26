using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Reservation;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class ReservationRepository : IReservationReadOnlyRepository, IReservationWriteOnlyRepository, IReservationUpdateOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public ReservationRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(Reservation reservation) => await _dbContext.Reservations.AddAsync(reservation);

		public async Task<bool> ReservationAlreadyCanceled(User user,long tableId, DateTime reservationDateTime)
		{
			return await _dbContext.Reservations
					.AsNoTracking()
					.AnyAsync(r => r.UserId == user.Id
					&& r.TableId == tableId
					&& r.ReservationDate == reservationDateTime
					&& r.Status == "Canceled");
		}

		public async Task<bool> IsTableAvailable(long tableId, DateTime reservationDateTime)
		{
			return !await _dbContext.Reservations
				.AsNoTracking()
				.AnyAsync(reservation => 
				reservation.TableId == tableId &&
				reservation.ReservationDate == reservationDateTime);
		}

		public async Task<List<Reservation>> ListReservations(long userId)
		{
			return await _dbContext.Reservations
				.AsNoTracking()
				.Where(r => r.UserId == userId)
				.OrderBy(r => r.ReservationDate)
				.ToListAsync();
		}

		public async Task<bool> ExistReservation(User user, long tableId, DateTime reservationDateTime)
		{
			return await _dbContext.Reservations
				.AsNoTracking()
				.AnyAsync(r => r.UserId == user.Id
				&& r.TableId == tableId
				&& r.ReservationDate == reservationDateTime);
		}

		public async Task<Reservation> GetReservation(Domain.Entities.User user, long tableId, DateTime dateTime)
		{
			return await _dbContext.Reservations
				.Where(r => r.UserId == user.Id
					&& r.TableId == tableId
					&& r.ReservationDate == dateTime)
				.FirstAsync();
		}

		public async Task Update(Reservation reservation)
		{
			await _dbContext.Reservations
				.Where(r => r.Id == reservation.Id)
				.ExecuteUpdateAsync(x => x
				.SetProperty(r => r.Status, "Canceled")
				);
		}
	}
}
