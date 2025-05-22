using Microsoft.EntityFrameworkCore;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.User;

namespace ReservaRestaurante.Infrastructure.DataAccess.Repositories
{
	public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository, IUserDeleteOnlyRepository
	{
		private readonly ReservaRestauranteDbContext _dbContext;

		public UserRepository(ReservaRestauranteDbContext dbContext) => _dbContext = dbContext;

		public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

		public async Task DeleteAccount(Guid userIdentifier)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);
			if(user is null)
				return;

			var reservations = _dbContext.Reservations.Where(r => r.UserId == user.Id);

			_dbContext.Reservations.RemoveRange(reservations);

			_dbContext.Users.Remove(user);
		}

		public async Task<bool> ExistAdminWithIdentifier(Guid userIdentifier)
		{
			return await _dbContext.Users.
				AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Role.Equals("Administrator") && user.Active);
		}

		public async Task<bool> ExistUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

		public async Task<bool> ExistUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);

		public async Task<User> GetByEmail(string email) => await _dbContext.Users.FirstAsync(user => user.Active && user.Email == email);

		public async Task<User?> GetByEmailReadOnly(string email)
		{
			return await _dbContext
			.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
		}

		public async Task<User> GetById(long id)
		{
			return await _dbContext
				.Users // Não pode ter a função AsNoTracking por que vamos atualizar alguma informação
				.FirstAsync(user => user.Id == id);
		}

		public void Update(User user) => _dbContext.Users.Update(user);

		public async Task<bool> UserIsAdmin(User user) => await _dbContext.Users.AnyAsync(u => u.Role == "Administrator" && u.Role == user.Role);
	}
}
