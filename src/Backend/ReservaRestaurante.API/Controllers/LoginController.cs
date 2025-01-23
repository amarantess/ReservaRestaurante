using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.Application.UseCases.Login.DoLogin;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

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
	}
}
