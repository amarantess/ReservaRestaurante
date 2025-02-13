using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class CapacityInvalidException : ReservaRestauranteException
	{
		public CapacityInvalidException() : base(ResourceMessagesException.TABLE_NOT_SUPPORT)
		{
		}

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
	}
}
