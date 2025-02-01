using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.User.ChangePassword;
using ReservaRestaurante.Application.UseCases.User.Profile;
using ReservaRestaurante.Application.UseCases.User.Promote;
using ReservaRestaurante.Application.UseCases.User.Register;
using ReservaRestaurante.Application.UseCases.User.Update;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
	[Route("users")]
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

		[HttpPut("update")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
		[AuthenticatedUser]
		public async Task<IActionResult> Update(
			[FromServices] IUpdateUserUseCase useCase,
			[FromBody] RequestUpdateUser request)
		{
			await useCase.Execute(request);

			return NoContent();
		}

		[HttpPut("change-password")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
		[AuthenticatedUser]
		public async Task<IActionResult> ChangePassword(
			[FromServices]IChangePasswordUseCase useCase,
			[FromBody]RequestChangePassword request)
		{
			await useCase.Execute(request);

			return NoContent();
		}

		[HttpPut("promote")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
		[AuthenticatedUser]
		[AuthenticatedAdmin]
		public async Task<IActionResult> PromoteUser(
			[FromServices]IPromoteUserUseCase useCase,
			[FromBody]RequestPromoteUser request)
		{
			await useCase.Execute(request);

			return NoContent();
		}
	}
}
