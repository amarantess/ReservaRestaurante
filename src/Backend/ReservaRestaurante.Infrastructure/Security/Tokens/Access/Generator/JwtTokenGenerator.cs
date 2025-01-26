using Microsoft.IdentityModel.Tokens;
using ReservaRestaurante.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservaRestaurante.Infrastructure.Security.Tokens.Access.Generator
{
	public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
	{
		private readonly uint _expirationTimeMinutes;
		private readonly string _signingKey;

		public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
		{
			_expirationTimeMinutes = expirationTimeMinutes;
			_signingKey = signingKey;
		}
		public string Generate(Guid userIdentifier)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Sid, userIdentifier.ToString())
			};

			var tokenDescriptor = new SecurityTokenDescriptor // Descrevendo o token
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
				SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var securityToken = tokenHandler.CreateToken(tokenDescriptor); // Criando token com as descrições definidas

			return tokenHandler.WriteToken(securityToken); // Devolvendo o token como uma string
		}
	}
}
