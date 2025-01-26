using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Infrastructure.Security.Tokens.Access.Generator;

namespace CommomTestUtilities.Tokens
{
	public class JwtTokenGeneratorBuilder
	{
		public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "tttttttttttttttttttttttttttttttt");
	}
}
