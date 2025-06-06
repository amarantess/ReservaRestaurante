using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.Application.UseCases.Token.RefreshToken
{
    public interface IUseRefreshTokenUseCase
    {
        public Task<ResponseTokenJson> Execute(RequestNewTokenJson request);
    }
}
