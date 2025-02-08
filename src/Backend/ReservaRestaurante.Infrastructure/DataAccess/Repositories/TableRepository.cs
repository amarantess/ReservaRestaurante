using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Table;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class TableRepository : ITableReadOnlyRepository, ITableWriteOnlyRepository, ITableUpdateOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public TableRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(Table table) => await _dbContext.Table.AddAsync(table);

		public async Task<bool> ExistTableWithId(long id) => await _dbContext.Table.AnyAsync(table => table.Id.Equals(id));

		public async Task<bool> ExistTableWithNumber(int number) => await _dbContext.Table.AnyAsync(table => table.Number.Equals(number));

		public async Task<Table> GetById(long id) => await _dbContext.Table.FirstAsync(table => table.Id.Equals(id));

		public async Task<List<Table>> ListTables() => await _dbContext.Table.ToListAsync();

		public void Update(Table table) => _dbContext.Table.Update(table);
	}
}
