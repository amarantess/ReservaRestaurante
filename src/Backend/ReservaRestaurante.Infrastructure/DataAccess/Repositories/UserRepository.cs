using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.User;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public UserRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

		public async Task<bool> ExistUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));

		public async Task<User?> GetByEmailAndPassword(string email, string password)
		{
			return await _dbContext
				.Users
				.AsNoTracking() // Esse método é utilizado quando não queremos atualizar alguma informação no contexto
				.FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
		}
	}
}
