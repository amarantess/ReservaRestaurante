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
			if(context.Exception is ReservaRestauranteException reservaRestauranteException)
			{
				HandleProjectException(reservaRestauranteException, context);
			}
			else
			{
				ThrowUnknowException(context);
			}
		}

		private static void HandleProjectException(ReservaRestauranteException reservaRestauranteException, ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)reservaRestauranteException.GetStatusCode();
			context.Result = new ObjectResult(new ResponseError(reservaRestauranteException.GetErrorMessages()));
		}

		private static void ThrowUnknowException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Result = new ObjectResult(new ResponseError(ResourceMessagesException.UNKNOW_ERROR));
		}
	}
}
