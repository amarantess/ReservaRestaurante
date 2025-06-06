using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class RefreshTokenNotFoundException : ReservaRestauranteException
	{
		public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION) { }

		public override IList<string> GetErrorMessages() => [Message];
		public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
	}
}
