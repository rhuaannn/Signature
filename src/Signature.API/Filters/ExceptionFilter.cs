using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Signature.Exception;
using Signature.Exception.ErrorJson;
using Signature.Exception.Exception;
using System.Net;

namespace Signature.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            if (context.Exception is ErroOnValidationException validationException)
            {
                HandleBadRequestException(context, validationException);
            }
            else if (context.Exception is DomainValidationException domainEx)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(ErrorMessageException.BADREQUEST));
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private void HandleBadRequestException(ExceptionContext context, ErroOnValidationException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.Errors));
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ErrorMessageException.UNKNOWERROR));
        }
    }
}