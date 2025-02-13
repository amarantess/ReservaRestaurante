namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class TableIsNotAvailableException : ReservaRestauranteException
	{
		public TableIsNotAvailableException() : base(ResourceMessagesException.TABLE_NOT_AVAILABLE)
		{
		}
	}
}
