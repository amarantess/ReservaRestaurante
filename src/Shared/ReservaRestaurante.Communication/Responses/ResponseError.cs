namespace ReservaRestaurante.Communication.Responses
{
	public class ResponseError
	{
		public IList<string> Errors { get; set; }

		public ResponseError(IList<string> errors) => Errors = errors;

		public ResponseError(string error)
		{
			Errors = new List<string>();
			Errors.Add(error);
		}
	}
}
