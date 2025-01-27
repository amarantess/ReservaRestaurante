using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Services.LoggedUser;

namespace CommomTestUtilities.LoggedUser
{
	public class LoggedUserBuilder
	{
		public static ILoggedUser Build(User user)
		{
			var mock = new Mock<ILoggedUser>();

			mock.Setup(x => x.User()).ReturnsAsync(user);

			return mock.Object;
		}
	}
}
