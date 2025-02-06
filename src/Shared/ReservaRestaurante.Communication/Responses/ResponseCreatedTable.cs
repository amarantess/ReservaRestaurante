namespace ReservaRestaurante.Communication.Responses
{
	public class ResponseCreatedTable
	{
		public long Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public int Number { get; set; }
		public int Capacity { get; set; }
		public string Status { get; set; } = string.Empty;
	}
}
