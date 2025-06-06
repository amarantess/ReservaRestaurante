using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Infrastructure.Security.Tokens.Refresh;

namespace CommomTestUtilities.Tokens
{
    public class RefreshTokenGeneratorBuilder
    {
        public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
    }
}
