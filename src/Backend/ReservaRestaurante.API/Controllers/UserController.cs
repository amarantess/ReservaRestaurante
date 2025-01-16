using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
	[Route("usuarios")]
	[ApiController]
	public class UserController : ControllerBase
	{
		[HttpPost("registrar")]
		[ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
		public IActionResult Register(RequestRegisterUser request)
		{
			return Created();
		}
	}
}
