using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Tokens;

namespace ReservaRestaurante.Application.UseCases.Login.External
{
	public class ExternalLoginUseCase : IExternalLoginUseCase
	{
		private readonly IUserReadOnlyRepository _repositoryRead;
		private readonly IUserWriteOnlyRepository _repositoryWrite;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAccessTokenGenerator _accessTokenGenerator;

		public ExternalLoginUseCase(
			IUserReadOnlyRepository repositoryRead,
			IUserWriteOnlyRepository repositoryWrite,
			IUnitOfWork unitOfWork,
			IAccessTokenGenerator accessTokenGenerator)
		{
			_repositoryRead = repositoryRead;
			_repositoryWrite = repositoryWrite;
			_unitOfWork = unitOfWork;
			_accessTokenGenerator = accessTokenGenerator;
		}

		public async Task<string> Execute(string name, string email)
		{
			var user = await _repositoryRead.GetByEmailReadOnly(email);

			if(user is null)
			{
				user = new Domain.Entities.User
				{
					Name = name,
					Email = email,
					UserIdentifier = Guid.NewGuid(),
					Password = "-"
				};

				await _repositoryWrite.Add(user);
				await _unitOfWork.Commit();
			}

			return _accessTokenGenerator.Generate(user.UserIdentifier);
		}
	}
}
