namespace ReservaRestaurante.Communication.Requests
{
    public class RequestCancelReservation
    {
		public int TableNumber { get; set; }
		public string ReservationDateTime { get; set; } = string.Empty;
	}
}
