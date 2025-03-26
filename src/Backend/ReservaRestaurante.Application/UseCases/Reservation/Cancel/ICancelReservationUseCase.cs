using ReservaRestaurante.Communication.Requests;

namespace ReservaRestaurante.Application.UseCases.Reservation.Cancel
{
    public interface ICancelReservationUseCase
    {
        public Task Execute(RequestCancelReservation request);
    }
}
