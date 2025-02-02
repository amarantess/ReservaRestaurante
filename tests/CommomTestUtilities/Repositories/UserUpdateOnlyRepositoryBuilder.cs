using Moq;
using ReservaRestaurante.Domain.Entities;
using ReservaRestaurante.Domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
	public class UserUpdateOnlyRepositoryBuilder
	{
		private readonly Mock<IUserUpdateOnlyRepository> _repository;

		public UserUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUserUpdateOnlyRepository>();

		public UserUpdateOnlyRepositoryBuilder GetById(User user)
		{
			_repository.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);
			return this;
		}

		public UserUpdateOnlyRepositoryBuilder GetByEmail(User user)
		{
			_repository.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);
			return this;
		}

		public IUserUpdateOnlyRepository Build() => _repository.Object;
	}
}
