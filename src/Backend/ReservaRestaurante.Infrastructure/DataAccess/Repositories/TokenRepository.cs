using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.Token;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public TokenRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task<RefreshToken?> Get(string refreshToken)
		{
			return await _dbContext.RefreshTokens
				.AsNoTracking()
				.Include(token => token.User)
				.FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
		}

		public async Task SaveNewRefreshToken(RefreshToken refreshToken)
		{
			var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

			_dbContext.RefreshTokens.RemoveRange(tokens);

			await _dbContext.RefreshTokens.AddAsync(refreshToken);
		}
	}
}
