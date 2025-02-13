namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class CapacityInvalidException : ReservaRestauranteException
	{
		public CapacityInvalidException() : base(ResourceMessagesException.TABLE_NOT_SUPPORT)
		{
		}
	}
}
