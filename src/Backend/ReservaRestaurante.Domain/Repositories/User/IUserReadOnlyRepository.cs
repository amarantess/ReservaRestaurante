namespace ReservaRestaurante.Domain.Repositories.User
{
	public interface IUserReadOnlyRepository
	{
		public Task<bool> ExistUserWithEmail(string email);
	}
}
