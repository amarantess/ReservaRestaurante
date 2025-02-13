using System.Globalization;

namespace ReservaRestaurante.Application.Services.DateTimeConverter
{
	public interface IDateTimeConverter
	{
		public DateTime ConvertStringToDateTime(string date);
	}
}
