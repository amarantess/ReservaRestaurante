namespace ReservaRestaurante.Domain.Entities
{
	public class User : EntityBase
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Role { get; set; } = "Client"; // Client or Administrator
		public Guid UserIdentifier { get; set; }
		public bool Active { get; set; } = true;
	}
}
