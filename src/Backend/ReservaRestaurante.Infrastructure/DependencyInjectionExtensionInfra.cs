using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Infrastructure.DataAccess;
using ReservaRestaurante.Infrastructure.DataAccess.Repositories;
using ReservaRestaurante.Infrastructure.Extensions;
using ReservaRestaurante.Infrastructure.Security.Tokens.Access.Generator;
using ReservaRestaurante.Infrastructure.Security.Tokens.Access.Validator;
using ReservaRestaurante.Infrastructure.Services;
using System.Reflection;

namespace ReservaRestaurante.Infrastructure
{
	public static class DependencyInjectionExtensionInfra
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			AddRepositories(services);
			AddLoggedUser(services);
			AddTokens(services, configuration);

			if (configuration.IsUnitTestEnviroment())
			{
				return;
			}

			AddDbContext(services, configuration);
			AddFluentMigrator(services, configuration);
		}

		private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.ConnectionString();
			var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

			services.AddDbContext<ReservaRestauranteDbContext>(dbContext =>
			{
				dbContext.UseMySql(connectionString, serverVersion);
			});
		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IUserReadOnlyRepository, UserRepository>();
			services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
			services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
		{
			services.AddFluentMigratorCore().ConfigureRunner(options  =>
			{
				var connectionString = configuration.ConnectionString();

				options.AddMySql5()
				.WithGlobalConnectionString(connectionString)
				.ScanIn(Assembly.Load("ReservaRestaurante.Infrastructure")).For.All();
			});
		}

		private static void AddTokens(IServiceCollection services, IConfiguration configuration)
		{
			var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationMinutes");
			var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

			services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
			services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
		}

		private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
	}
}
