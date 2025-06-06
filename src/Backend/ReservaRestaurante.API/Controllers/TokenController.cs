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

		[HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromServices] IUseRefreshTokenUseCase useCase, [FromBody] RequestNewTokenJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
