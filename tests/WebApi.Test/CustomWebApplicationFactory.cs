using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Infrastructure.DataAccess;

namespace WebApi.Test
{
	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
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
				});
		}
	}
}
