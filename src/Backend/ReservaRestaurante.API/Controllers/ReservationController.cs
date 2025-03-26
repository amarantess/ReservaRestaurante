using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.Reservation.Cancel;
using ReservaRestaurante.Application.UseCases.Reservation.Create;
using ReservaRestaurante.Application.UseCases.Reservation.List;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
	[AuthenticatedUser]
	[Route("reservation")]
	[ApiController]
	public class ReservationController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreatedReservation), StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(
			[FromServices]ICreateReservationUseCase useCase,
			[FromBody]RequestCreateReservation request)
		{
			var result = await useCase.Execute(request);

			return Created(string.Empty, result);
		}

		[HttpGet]
		[ProducesResponseType(typeof(ResponseListReservation), StatusCodes.Status200OK)]
		public async Task<IActionResult> List([FromServices]IListReservationUseCase useCase)
		{
			var result = await useCase.Execute();

			return Ok(result);
		}

		[HttpPatch]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Cancel(
			[FromServices]ICancelReservationUseCase useCase,
			[FromBody]RequestCancelReservation request)
		{
			await useCase.Execute(request);

			return NoContent();
		}
	}
}
