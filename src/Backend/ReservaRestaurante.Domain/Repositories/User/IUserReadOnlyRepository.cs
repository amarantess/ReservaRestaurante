namespace ReservaRestaurante.Domain.Repositories.User
{
	public interface IUserReadOnlyRepository
	{
		public Task<bool> ExistUserWithEmail(string email);
		public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
		public Task<bool> ExistUserWithIdentifier(Guid userIdentifier);
		public Task<bool> ExistAdminWithIdentifier(Guid userIdentifier);
		public Task<bool> UserIsAdmin(Entities.User user);
	}
}
