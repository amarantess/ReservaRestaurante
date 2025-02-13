using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Infrastructure.DataAccess
{
	public class ReservaRestauranteDbContext : DbContext
	{
		public ReservaRestauranteDbContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Table> Table { get; set; }
		public DbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservaRestauranteDbContext).Assembly);
		}
	}
}
