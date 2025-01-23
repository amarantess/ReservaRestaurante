using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
	public class UserReadOnlyRepositoryBuilder
	{
		private readonly Mock<IUserReadOnlyRepository> _repository;

		public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

		public void ExistUserWithEmail(string email)
		{
			_repository.Setup(repository => repository.ExistUserWithEmail(email)).ReturnsAsync(true);
		}

		public void GetByEmailAndPassword(User user)
		{
			_repository.Setup(repository => repository.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
		}

		public IUserReadOnlyRepository Build() => _repository.Object;
	}
}
