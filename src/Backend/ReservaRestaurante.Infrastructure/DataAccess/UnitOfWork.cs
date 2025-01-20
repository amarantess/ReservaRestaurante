using ReservaRestaurante.Domain.Repositories;

namespace ReservaRestaurante.Infrastructure.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public UnitOfWork(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Commit() => await _dbContext.SaveChangesAsync();
	}
}
