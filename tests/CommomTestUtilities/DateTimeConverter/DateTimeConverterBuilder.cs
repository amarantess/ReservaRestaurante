using ReservaRestaurante.Application.Services.DateTimeConverter;

namespace CommomTestUtilities.DateTimeConverter
{
	public class DateTimeConverterBuilder
	{
		public static IDateTimeConverter Build() => new Converter();
	}
}
