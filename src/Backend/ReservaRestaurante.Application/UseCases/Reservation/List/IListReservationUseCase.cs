using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Reservation.List
{
    public interface IListReservationUseCase
    {
        public Task<List<ResponseListReservation>> Execute();
    }
}
