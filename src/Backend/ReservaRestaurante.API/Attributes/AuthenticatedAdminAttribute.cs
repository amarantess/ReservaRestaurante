using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Filters;

namespace ReservaRestaurante.API.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthenticatedAdminAttribute : TypeFilterAttribute
	{
		public AuthenticatedAdminAttribute() : base(typeof(AuthenticatedAdminFilter))
		{
		}
	}
}
