namespace ReservaRestaurante.Communication.Responses
{
    public class ResponseListReservation
    {
        public int TableNumber {  get; set; }
        public string ReservationDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
	}
}
