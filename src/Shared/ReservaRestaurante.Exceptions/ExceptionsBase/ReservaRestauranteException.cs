using System.Net;

namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public abstract class ReservaRestauranteException : SystemException
	{
		public ReservaRestauranteException(string message) : base(message) { }

		public abstract IList<string> GetErrorMessages();
		public abstract HttpStatusCode GetStatusCode();
	}
}
