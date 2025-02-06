namespace ReservaRestaurante.Domain.Entities
{
	public class Table : EntityBase
	{
		public int Number {  get; set; }
		public int Capacity { get; set; }
		public string Status { get; set; } = "Available"; // Available, Reserved, Inactive
	}
}
