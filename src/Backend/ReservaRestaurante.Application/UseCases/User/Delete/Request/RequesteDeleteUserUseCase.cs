using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Services.LoggedUser;
using ReservaRestaurante.Domain.Services.ServiceBus;

namespace ReservaRestaurante.Application.UseCases.User.Delete.Request
{
    public class RequesteDeleteUserUseCase : IRequesteDeleteUserUseCase
	{
		private readonly IDeleteUserQueue _deleteUserQueue;
		private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

		public RequesteDeleteUserUseCase(
			IDeleteUserQueue deleteUserQueue,
			IUserUpdateOnlyRepository userUpdateOnlyRepository,
			ILoggedUser loggedUser,
			IUnitOfWork unitOfWork)
		{
			_deleteUserQueue = deleteUserQueue;
			_userUpdateOnlyRepository = userUpdateOnlyRepository;
			_loggedUser = loggedUser;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute()
		{
			var loggedUser = await _loggedUser.User();

			var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);

			user.Active = false;
			_userUpdateOnlyRepository.Update(user);
			await _unitOfWork.Commit();

			await _deleteUserQueue.SendMessage(user);
		}
	}
}
