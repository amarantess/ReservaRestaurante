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

		public async Task Delete(long id)
		{
			var table = await _dbContext.Table.FindAsync(id);

			_dbContext.Table.Remove(table!);
		}

		public async Task<bool> IsCapacityValid(long tableId, int capacity)
		{
			return await _dbContext.Table.
				AsNoTracking().
				AnyAsync(table => table.Id == tableId && table.Capacity >= capacity);
		}

		public async Task<Table> GetTableByNumber(int tableNumber) => await _dbContext.Table.FirstAsync(table => table.Number == tableNumber);

		public async Task<List<int>> GetTablesNumber(List<Reservation> reservations)
		{
			var tableIds = reservations.Select(r => r.TableId).Distinct().ToList();

			var tablesNumber = await _dbContext.Table
				.Where(table => tableIds.Contains(table.Id))
				.Select(table => table.Number)
				.ToListAsync();

			return tablesNumber;
		}

		public async Task<long> GetTableIdByNumber(int tableNumber)
		{
			return await _dbContext.Table
				.AsNoTracking()
				.Where(t => t.Number.Equals(tableNumber))
				.Select(t => t.Id)
				.FirstAsync();
		}
	}
}
