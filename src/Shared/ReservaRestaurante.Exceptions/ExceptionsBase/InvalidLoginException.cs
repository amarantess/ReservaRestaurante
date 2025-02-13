using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class InvalidLoginException : ReservaRestauranteException
	{
		public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
		{
		}

		public override IList<string> GetErrorMessages() => [Message]; // Devolvendo uma lista da propriedade Exception.Message

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
	}
}
