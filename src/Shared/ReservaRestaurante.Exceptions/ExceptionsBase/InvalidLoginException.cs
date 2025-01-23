namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class InvalidLoginException : ReservaRestauranteException
	{
		public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
		{
		}
	}
}
