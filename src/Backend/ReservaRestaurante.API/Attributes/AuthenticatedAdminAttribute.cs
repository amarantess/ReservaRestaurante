using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Filters;

namespace ReservaRestaurante.API.Attributes
{
	public class AuthenticatedAdminAttribute : TypeFilterAttribute
	{
		public AuthenticatedAdminAttribute() : base(typeof(AuthenticatedAdminFilter))
		{
		}
	}
}
