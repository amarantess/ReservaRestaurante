using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using System.Net;

namespace ReservaRestaurante.API.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if(context.Exception is ReservaRestauranteException)
			{
				HandleProjectException(context);
			}
			else
			{
				ThrowUnknowException(context);
			}
		}

		private static void HandleProjectException(ExceptionContext context)
		{
			if (context.Exception is InvalidLoginException)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				context.Result = new UnauthorizedObjectResult(new ResponseError(context.Exception.Message));
			}
			else if (context.Exception is ErrorOnValidationException exception)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Result = new BadRequestObjectResult(new ResponseError(exception.ErrorMessages));
			}
		}

		private static void ThrowUnknowException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Result = new ObjectResult(new ResponseError(ResourceMessagesException.UNKNOW_ERROR));
		}
	}
}
