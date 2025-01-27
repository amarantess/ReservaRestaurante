using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Domain.Services.LoggedUser
{
	public interface ILoggedUser
	{
		public Task<User> User();
	}
}
