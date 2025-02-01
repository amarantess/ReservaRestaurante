namespace ReservaRestaurante.Domain.Repositories.User
{
	public interface IUserUpdateOnlyRepository
	{
		public Task<Entities.User> GetById(long id);
		public Task<Entities.User> GetByEmail(string email);
		public void Update(Entities.User user);
	}
}
