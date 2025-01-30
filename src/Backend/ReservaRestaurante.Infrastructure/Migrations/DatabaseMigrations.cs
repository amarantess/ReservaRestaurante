using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using ReservaRestaurante.Infrastructure.Security.AdminUserCreated;

namespace ReservaRestaurante.Infrastructure.Migration
{
	public static class DatabaseMigrations
	{
		public static void Migrate(string connectionString, IServiceProvider serviceProvider, IConfiguration configuration)
		{
			EnsureDatabaseCreated(connectionString);
			MigrationDatabase(serviceProvider);
			EnsureAdminUserExists(connectionString, configuration);
		}

		private static void EnsureDatabaseCreated(string connectionString)
		{
			var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

			var databaseName = connectionStringBuilder.Database;

			connectionStringBuilder.Remove("Database");

			using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

			var parameters = new DynamicParameters();
			parameters.Add("name", databaseName);

			var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

			if (!records.Any())
			{
				dbConnection.Execute($"CREATE DATABASE {databaseName}");
			}
		}

		private static void EnsureAdminUserExists(string connectionString, IConfiguration configuration)
		{
			var dbConnection = new MySqlConnection(connectionString);

			var adminExists = dbConnection.ExecuteScalar<bool>(
				"SELECT * FROM reservarestaurante.users WHERE Role = @role",
				new { role = "Administrator" });

			var user = new AdminUserCreated();

			if (!adminExists)
			{
				dbConnection.Execute(
					@"INSERT INTO reservarestaurante.users (CreatedOn, Name, Email, Password, Role, UserIdentifier)
                    VALUES (@createdOn, @name, @email, @password, @role, @userIdentifier)",
					new
					{
						createdOn = user.CreatedOn,
						name = AdminUserCreated.GetName(configuration),
						email = AdminUserCreated.GetEmail(configuration),
						password = AdminUserCreated.GetPassword(configuration),
						role = user.Role,
						userIdentifier = user.UserIdentifier
					});
			}
		}

		private static void MigrationDatabase(IServiceProvider serviceProvider)
		{
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

			runner.ListMigrations();

			runner.MigrateUp();
		}
	}
}
