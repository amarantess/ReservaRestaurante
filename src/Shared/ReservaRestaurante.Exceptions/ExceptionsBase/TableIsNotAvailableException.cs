using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class TableIsNotAvailableException : ReservaRestauranteException
	{
		public TableIsNotAvailableException() : base(ResourceMessagesException.TABLE_NOT_AVAILABLE)
		{
		}

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
	}
}
