using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ReservaRestaurante.Infrastructure.Security.Tokens.Access
{
	public abstract class JwtTokenHandler
	{
		protected static SymmetricSecurityKey SecurityKey(string signingKey) // Transformando a assinatura em uma securityKey
		{
			var bytes = Encoding.UTF8.GetBytes(signingKey);

			return new SymmetricSecurityKey(bytes);
		}
	}
}
