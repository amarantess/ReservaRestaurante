namespace ReservaRestaurante.Exceptions.ExceptionsBase
{
	public class ErrorOnValidationException : ReservaRestauranteException
	{
		public IList<string> ErrorMessages { get; set; }

		public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
		{
			ErrorMessages = errorMessages;
		}
	}
}
