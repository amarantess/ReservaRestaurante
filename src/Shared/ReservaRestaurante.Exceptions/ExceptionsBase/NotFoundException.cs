namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class NotFoundException : ReservaRestauranteException
	{
		public NotFoundException(string message) : base(message)
		{
		}
	}
}
