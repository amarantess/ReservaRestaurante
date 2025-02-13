using System.Globalization;

namespace ReservaRestaurante.Application.Services.DateTimeConverter
{
	public class Converter : IDateTimeConverter
	{
		public DateTime ConvertStringToDateTime(string date)
		{
			DateTime.TryParse(date, new CultureInfo("en-US"), out var dateTime);

			return dateTime;
		}
	}
}
