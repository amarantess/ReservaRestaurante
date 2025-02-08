using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Application.UseCases.Table.List;
using ReservaRestaurante.Application.UseCases.Table.List_Admin;
using ReservaRestaurante.Application.UseCases.Table.Update;
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
		[ProducesResponseType(typeof(ResponseTable), StatusCodes.Status201Created)]
		[AuthenticatedAdmin]
		public async Task<IActionResult> Create(
			[FromServices]ICreateTableUseCase useCase,
			[FromBody]RequestCreateTable request)
		{
			var result = await useCase.Execute(request);

			return Created(string.Empty, result);
		}

		[HttpGet]
		[ProducesResponseType(typeof(ResponseListTable), StatusCodes.Status200OK)]
		public async Task<IActionResult> List(
			[FromServices]IListTableUseCase useCase)
		{
			var result = await useCase.Execute();

			return Ok(result);
		}

		[HttpGet("admin")]
		[ProducesResponseType(typeof(ResponseTable), StatusCodes.Status200OK)]
		[AuthenticatedAdmin]
		public async Task<IActionResult> ListTablesAdmin(
			[FromServices] IListTableAdminUseCase useCase)
		{
			var result = await useCase.Execute();

			return Ok(result);
		}

		[HttpPut]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
		[AuthenticatedAdmin]
		public async Task<IActionResult> Update(
			[FromServices]IUpdateTableUseCase useCase,
			[FromRoute]long id,
			[FromBody]RequestUpdateTable request)
		{
			await useCase.Execute(id, request);

			return NoContent();
		}
	}
}
