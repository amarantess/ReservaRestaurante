using ReservaRestaurante.Domain.Security.Tokens;

namespace ReservaRestaurante.API.Token
{
	public class HttpContextTokenValue : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor; // Esta interface é acessada somente pelo projeto de API

		public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public string Value()
		{
			var authentication = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

			return authentication["Bearer".Length..].Trim(); // Retorna apenas o token sem o Bearer
		}
	}
}
