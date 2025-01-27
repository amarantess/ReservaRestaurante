using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.User.Profile;
using ReservaRestaurante.Application.UseCases.User.Register;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
	[Route("usuarios")]
	[ApiController]
	public class UserController : ControllerBase
	{
		[HttpPost("register")]
		[ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
		public async Task<IActionResult> Register(
			[FromServices] IRegisterUserUseCase useCase,
			[FromBody] RequestRegisterUser request)
		{
			var result = await useCase.Execute(request);

			return Created(string.Empty ,result);
		}

		[HttpGet("get/profile")]
		[ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
		[AuthenticatedUser]
		public async Task<IActionResult> GetUserProfile([FromServices]IGetUserProfileUseCase useCase)
		{
			var result = await useCase.Execute();

			return Ok(result);
		}
	}
}
