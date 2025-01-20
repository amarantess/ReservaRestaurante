using System.Security.Cryptography;
using System.Text;

namespace ReservaRestaurante.Application.Services.Criptography
{
	public class PasswordEncripter
	{
		public string Encrypt(string password)
		{
			var bytes = Encoding.UTF8.GetBytes(password);
			var hashBytes = SHA512.HashData(bytes);

			return StringBytes(bytes);
		}

		private static string StringBytes(byte[] bytes)
		{
			var sb = new StringBuilder();
			foreach (byte b in bytes)
			{
				var hex = b.ToString("x2");
				sb.Append(hex);
			}

			return sb.ToString();
		}
	}
}
