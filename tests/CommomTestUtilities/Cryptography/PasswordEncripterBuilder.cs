using ReservaRestaurante.Domain.Security.Cryptography;
using ReservaRestaurante.Infrastructure.Security.Cryptography;

namespace CommomTestUtilities.Cryptography
{
	public class PasswordEncripterBuilder
	{
		public static IPasswordEncripter Build() => new BCryptNet();
	}
}
