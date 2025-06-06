using Microsoft.AspNetCore.Mvc;
using ReservaRestaurante.Application.UseCases.Token.RefreshToken;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUseRefreshTokenUseCase _service;

		public TokenController(IUseRefreshTokenUseCase service)
		{
			_service = service;
		}

		[HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromServices] IUseRefreshTokenUseCase useCase, [FromBody] RequestNewTokenJson request)
        {
            var response = await _service.Execute(request);

            return Ok(response);
        }
    }
}
