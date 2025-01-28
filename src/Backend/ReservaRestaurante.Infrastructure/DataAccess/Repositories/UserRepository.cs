using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.User;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public UserRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

		public async Task<bool> ExistUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));

		public async Task<bool> ExistUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier));

		public async Task<User?> GetByEmailAndPassword(string email, string password)
		{
			return await _dbContext
				.Users
				.AsNoTracking() // Esse método é utilizado quando não queremos atualizar alguma informação no contexto
				.FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
		}

		public async Task<User> GetById(long id)
		{
			return await _dbContext
				.Users // Não pode ter a função AsNoTracking por que vamos atualizar alguma informação
				.FirstAsync(user => user.Id == id);
		}

		public void Update(User user) => _dbContext.Users.Update(user);
	}
}
