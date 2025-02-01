using Microsoft.AspNetCore.Mvc.Filters;
using ReservaRestaurante.Domain.Repositories.User;
using ReservaRestaurante.Domain.Security.Tokens;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservaRestaurante.Communication.Responses;

namespace ReservaRestaurante.API.Filters
{
	public class AuthenticatedAdminFilter : IAsyncAuthorizationFilter
	{
		private readonly IAccessTokenValidator _accessTokenValidator;
		private readonly IUserReadOnlyRepository _repository;

		public AuthenticatedAdminFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
		{
			_accessTokenValidator = accessTokenValidator;
			_repository = repository;
		}

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			try
			{
				var token = TokenOnRequest(context); // Recebo o token

				var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token); // Valido o token

				var exist = await _repository.ExistAdminWithIdentifier(userIdentifier); // Existe um administrador com esse identificador?
				if (!exist)
				{
					throw new ReservaRestauranteException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
				}
			}
			catch (SecurityTokenExpiredException) // Se o token estiver expirado
			{
				context.Result = new UnauthorizedObjectResult(new ResponseError("TokenIsExpired")
				{
					TokenIsExpired = true,
				});
			}
			catch (ReservaRestauranteException ex)
			{
				context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
			}
			catch
			{
				context.Result = new UnauthorizedObjectResult(new ResponseError(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
			}
		}

		private static string TokenOnRequest(AuthorizationFilterContext context)
		{
			var authentication = context.HttpContext.Request.Headers.Authorization.ToString(); // Buscar o token na requisição
			if (string.IsNullOrWhiteSpace(authentication))
			{
				throw new ReservaRestauranteException(ResourceMessagesException.NO_TOKEN);
			}

			return authentication["Bearer".Length..].Trim(); // Retorna apenas o token sem o Bearer
		}
	}
}
