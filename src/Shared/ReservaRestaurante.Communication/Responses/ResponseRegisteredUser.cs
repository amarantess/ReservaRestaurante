namespace ReservaRestaurante.Communication.Responses
{
	public class ResponseRegisteredUser
	{
		public string Name { get; set; } = string.Empty;
		public ResponseTokenJson Tokens { get; set; } = default!;
	}
}
