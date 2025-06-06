using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReservaRestaurante.Infrastructure.Services.LoggedUser
{
	public class LoggedUser : ILoggedUser
	{
		private readonly ReservaRestauranteDbContext _dbContext;
		private readonly ITokenProvider _tokenProvider;

		public LoggedUser(ReservaRestauranteDbContext dbContext, ITokenProvider tokenProvider)
		{
			_dbContext = dbContext;
			_tokenProvider = tokenProvider;
		}

		public async Task<User> User()
		{
			var token = _tokenProvider.Value(); // Recebe o token

			var tokenHandler = new JwtSecurityTokenHandler();

			var jwtSecurityToken = tokenHandler.ReadJwtToken(token); // Faz a leitura

			var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value; //Faz a comparação do tipo de claim

			var userIdentifier = Guid.Parse(identifier); // Transforma o identifier em um Guid

			return await _dbContext
				.Users
				.AsNoTracking()
				.FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
		}
	}
}
