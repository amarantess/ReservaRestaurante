using Microsoft.Extensions.Configuration;
using ReservaRestaurante.Infrastructure.Security.Cryptography;

namespace ReservaRestaurante.Infrastructure.Security.AdminUserCreated
{
	public class AdminUserCreated
	{
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public string Role { get; set; } = "Administrator";
		public Guid UserIdentifier { get; set; } = Guid.NewGuid();
		public bool Active { get; set; } = true;

		public static string GetName(IConfiguration configuration) => configuration.GetValue<string>("Settings:User:Name")!;

		public static string GetEmail(IConfiguration configuration) => configuration.GetValue<string>("Settings:User:Email")!;

		public static string GetPassword(IConfiguration configuration)
		{
			var password = configuration.GetValue<string>("Settings:User:Password");
			var encryptedPassword = new BCryptNet().Encrypt(password!);
			return encryptedPassword;
		}
	}
}
