namespace ReservaRestaurante.Domain.Repositories.User
{
	public interface IUserReadOnlyRepository
	{
		public Task<bool> ExistUserWithEmail(string email);
		public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
		public Task<bool> ExistUserWithIdentifier(Guid userIdentifier);
	}
}
