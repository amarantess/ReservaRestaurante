using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Filters;

namespace ReservaRestaurante.API.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class AuthenticatedUserAttribute : TypeFilterAttribute
	{
		public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
		{
		}
	}
}
