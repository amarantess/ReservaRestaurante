namespace ReservaRestaurante.Domain.Entities
{
	public class Reservation : EntityBase
	{
		public long UserId { get; set; }
		public long TableId { get; set; }
		public DateTime ReservationDate { get; set; }
		public string Status { get; set; } = "Active"; // Active or Canceled
	}
}
