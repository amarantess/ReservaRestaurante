using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.Application.UseCases.Login.DoLogin;
using ReservaRestaurante.Application.UseCases.Login.External;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using System.Security.Claims;

namespace ReservaRestaurante.API.Controllers
{
	[Route("login")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLogin request)
		{
			var response = await useCase.Execute(request);

			return Ok(response);
		}

		[HttpGet]
		[Route("google")]
		public async Task<IActionResult> LoginGoogle(string returnUrl, [FromServices]IExternalLoginUseCase useCase)
		{
			var authenticate = await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

			bool isNotAuthenticated = !authenticate.Succeeded || authenticate.Principal is null || !authenticate.Principal.Identities.Any(id => id.IsAuthenticated);

			if (isNotAuthenticated)
			{
				return Challenge(GoogleDefaults.AuthenticationScheme);
			}
			else
			{
				var claims = authenticate.Principal!.Identities.First().Claims;

				var name = claims.First(c => c.Type == ClaimTypes.Name).Value;
				var email = claims.First(c => c.Type == ClaimTypes.Email).Value;

				var token = await useCase.Execute(name, email);

				return Redirect($"{returnUrl}/{token}");
			}
		}
	}
}
