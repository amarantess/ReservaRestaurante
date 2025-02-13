using System.Text.Json.Serialization;

namespace ReservaRestaurante.Communication.Requests
{
	public class RequestCreateReservation
	{
		public int TableNumber { get; set; }
		public string ReservationDateTime { get; set; } = string.Empty;
		public int PeopleNumber { get; set; }
	}
}
