using Azure.Messaging.ServiceBus;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Reservation;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Domain.Repositories.Token;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Cryptography;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Domain.Services.ServiceBus;
using ReservaRestaurante.Infrastructure.DataAccess;
using ReservaRestaurante.Infrastructure.DataAccess.Repositories;
using ReservaRestaurante.Infrastructure.Extensions;
using ReservaRestaurante.Infrastructure.Security.Cryptography;
using ReservaRestaurante.Infrastructure.Security.Tokens.Access.Generator;
using ReservaRestaurante.Infrastructure.Security.Tokens.Access.Validator;
using ReservaRestaurante.Infrastructure.Security.Tokens.Refresh;
using ReservaRestaurante.Infrastructure.Services.LoggedUser;
using ReservaRestaurante.Infrastructure.Services.ServiceBus;
using System.Reflection;

namespace ReservaRestaurante.Infrastructure
{
	public static class DependencyInjectionExtensionInfra
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			AddPasswordEncripter(services);
			AddRepositories(services);
			AddLoggedUser(services);
			AddTokens(services, configuration);
			AddQueue(services, configuration);

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
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<IUserReadOnlyRepository, UserRepository>();
			services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
			services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
			services.AddScoped<IUserDeleteOnlyRepository, UserRepository>();

			services.AddScoped<ITableReadOnlyRepository, TableRepository>();
			services.AddScoped<ITableWriteOnlyRepository, TableRepository>();
			services.AddScoped<ITableUpdateOnlyRepository, TableRepository>();

			services.AddScoped<IReservationReadOnlyRepository,  ReservationRepository>();
			services.AddScoped<IReservationWriteOnlyRepository, ReservationRepository>();
			services.AddScoped<IReservationUpdateOnlyRepository, ReservationRepository>();

			services.AddScoped<ITokenRepository, TokenRepository>();
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
			services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
		}

		private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

		private static void AddPasswordEncripter(IServiceCollection services) => services.AddScoped<IPasswordEncripter, BCryptNet>();

		private static void AddQueue(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetValue<string>("Settings:ServiceBus:DeleteUserAccount")!;

			var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
			{
				TransportType = ServiceBusTransportType.AmqpWebSockets
			});

			var deleteQueue = new DeleteUserQueue(client.CreateSender("user"));

			var deleteUserProcessor = new DeleteUserProcessor(client.CreateProcessor("user", new ServiceBusProcessorOptions
			{
				MaxConcurrentCalls = 1
			}));

			services.AddSingleton(deleteUserProcessor);

			services.AddScoped<IDeleteUserQueue>(options => deleteQueue);
		}
	}
}
