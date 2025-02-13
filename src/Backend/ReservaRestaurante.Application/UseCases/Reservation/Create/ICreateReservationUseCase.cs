using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Reservation.Create
{
	public interface ICreateReservationUseCase
	{
		public Task<ResponseCreatedReservation> Execute(RequestCreateReservation request);
	}
}
