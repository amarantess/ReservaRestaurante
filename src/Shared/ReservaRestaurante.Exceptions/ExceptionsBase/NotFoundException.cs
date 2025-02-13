using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class NotFoundException : ReservaRestauranteException
	{
		public NotFoundException(string message) : base(message)
		{
		}

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
	}
}
