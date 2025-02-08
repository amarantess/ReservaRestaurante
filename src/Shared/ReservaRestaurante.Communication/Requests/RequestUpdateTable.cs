namespace ReservaRestaurante.Communication.Requests
{
	public class RequestUpdateTable
	{
		public int Capacity { get; set; }
		public string Status { get; set; } = string.Empty;
	}
}
