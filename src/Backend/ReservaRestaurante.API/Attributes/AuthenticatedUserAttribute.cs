using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Filters;

namespace ReservaRestaurante.API.Attributes
{
	public class AuthenticatedUserAttribute : TypeFilterAttribute
	{
		public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
		{
		}
	}
}
