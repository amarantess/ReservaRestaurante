using AutoMapper;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.Reservation;
using ReservaRestaurante.Domain.Services.LoggedUser;

namespace ReservaRestaurante.Application.UseCases.Reservation.List
{
	public class ListReservationUseCase : IListReservationUseCase
	{
		private readonly ILoggedUser _loggedUser;
		private readonly IReservationReadOnlyRepository _repository;
		public readonly IMapper _mapper;

		public ListReservationUseCase(
			ILoggedUser loggedUser,
			IReservationReadOnlyRepository reservationReadOnlyRepository,
			IMapper mapper)
		{
			_loggedUser = loggedUser;
			_repository = reservationReadOnlyRepository;;
			_mapper = mapper;
		}

		public async Task<List<ResponseListReservation>> Execute()
		{
			var user = await _loggedUser.User();

			var reservations = await _repository.ListReservations(user.Id);

			return _mapper.Map<List<ResponseListReservation>>(reservations);
		}
	}
}
