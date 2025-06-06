using ReservaRestaurante.Domain.Security.Tokens;

namespace ReservaRestaurante.Infrastructure.Security.Tokens.Refresh
{
	public class RefreshTokenGenerator : IRefreshTokenGenerator
	{
		public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
	}
}
