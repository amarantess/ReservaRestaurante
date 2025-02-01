namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class UserAlreadyAdmException : ReservaRestauranteException
	{
		public UserAlreadyAdmException() : base(ResourceMessagesException.USER_ALREADY_ADM)
		{
		}
	}
}
