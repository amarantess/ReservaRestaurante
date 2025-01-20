namespace ReservaRestaurante.Domain.Entities
{
	public class EntityBase
	{
		public long Id { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
	}
}
