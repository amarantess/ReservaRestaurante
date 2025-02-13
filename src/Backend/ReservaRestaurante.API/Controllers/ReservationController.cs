using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.API.Attributes;
using ReservaRestaurante.Application.UseCases.Reservation.Create;
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
	}
}
