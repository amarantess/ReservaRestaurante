using CommomTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Infrastructure.DataAccess;

namespace WebApi.Test
{
	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
		private ReservaRestaurante.Domain.Entities.User _user = default!;
		private ReservaRestaurante.Domain.Entities.Table _table = default!;
		private string _password = string.Empty;

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Test")
				.ConfigureServices(services =>
				{
					var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ReservaRestauranteDbContext>)); // Indo até a DI ver se já existe um Db
					if (descriptor is not null)
						services.Remove(descriptor);

					var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

					services.AddDbContext<ReservaRestauranteDbContext>(options =>
					{
						options.UseInMemoryDatabase("InMemoryDbForTesting");
						options.UseInternalServiceProvider(provider);
					});

					using var scope = services.BuildServiceProvider().CreateScope();

					var dbContext = scope.ServiceProvider.GetRequiredService<ReservaRestauranteDbContext>();

					dbContext.Database.EnsureDeleted(); //Garantir que a base de dados inicializara vazia

					StartDatabase(dbContext);
				});
		}

		public string GetName() => _user.Name;
		public string GetEmail() => _user.Email;
		public string GetPassword() => _password;

		public Guid GetUserIdentifier() => _user.UserIdentifier;

		private void StartDatabase(ReservaRestauranteDbContext dbContext)
		{
			(_user, _password) = UserBuilder.Build();
			_user.Role = "Administrator";

			_table = TableBuilder.Build();

			dbContext.Users.Add(_user);
			dbContext.Table.Add(_table);

			dbContext.SaveChanges();
		}
	}
}
