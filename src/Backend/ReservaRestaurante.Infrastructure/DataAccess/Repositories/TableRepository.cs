using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Table;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class TableRepository : ITableReadOnlyRepository, ITableWriteOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public TableRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(Table table) => await _dbContext.Table.AddAsync(table);

		public async Task<bool> ExistTableWithNumber(int number) => await _dbContext.Table.AnyAsync(table => table.Number.Equals(number));

		public async Task<List<Table>> ListTables() => await _dbContext.Table.ToListAsync();
	}
}
