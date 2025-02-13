using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class UserAlreadyAdmException : ReservaRestauranteException
	{
		public UserAlreadyAdmException() : base(ResourceMessagesException.USER_ALREADY_ADM)
		{
		}

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
	}
}
