using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
	[AuthenticatedUser]
	[Route("tables")]
	[ApiController]
	public class TableController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreatedTable), StatusCodes.Status201Created)]
		[AuthenticatedAdmin]
		public async Task<IActionResult> Create(
			[FromServices]ICreateTableUseCase useCase,
			[FromBody]RequestCreateTable request)
		{
			var result = await useCase.Execute(request);

			return Created(string.Empty, result);
		}
	}
}
