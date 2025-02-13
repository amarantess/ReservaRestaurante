using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class UnauthorizedException : ReservaRestauranteException
	{
		public UnauthorizedException(string message) : base(message)
		{
		}

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
	}
}
