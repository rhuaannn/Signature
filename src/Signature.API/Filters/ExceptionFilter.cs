using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Signature.Exception;
using Signature.Exception.ErrorJson;
using Signature.Exception.Exception;
using System.Net;
using System.Text.Json;

namespace Signature.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception,
                "Exceção capturada: {ExceptionType} - {Message}",
                context.Exception.GetType().Name,
                context.Exception.Message);

            context.ExceptionHandled = true;

            switch (context.Exception)
            {
                case ErroOnValidationException validationException:
                    HandleValidationException(context, validationException);
                    break;

                case DomainValidationException domainEx:
                    HandleDomainException(context, domainEx);
                    break;

                case ArgumentException argEx:
                    HandleArgumentException(context, argEx);
                    break;

                case InvalidOperationException invalidOpEx:
                    HandleInvalidOperationException(context, invalidOpEx);
                    break;

                // Tratar exceções específicas de JSON/Model Binding
                case JsonException jsonEx:
                    HandleJsonException(context, jsonEx);
                    break;

                // Tratar exceções de conversão (incluindo enum)
                case InvalidCastException castEx:
                    HandleInvalidCastException(context, castEx);
                    break;

                case FormatException formatEx:
                    HandleFormatException(context, formatEx);
                    break;

                default:
                    HandleUnknownException(context);
                    break;
            }
        }

        private void HandleValidationException(ExceptionContext context, ErroOnValidationException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.Errors));
        }

        private void HandleDomainException(ExceptionContext context, DomainValidationException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.Message));
        }

        private void HandleArgumentException(ExceptionContext context, ArgumentException exception)
        {
            var friendlyMessage = GetFriendlyArgumentMessage(exception);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(friendlyMessage));
        }

        private void HandleArgumentNullException(ExceptionContext context, ArgumentNullException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(
                new ResponseErrorJson($"O campo '{exception.ParamName}' é obrigatório."));
        }

        private void HandleInvalidOperationException(ExceptionContext context, InvalidOperationException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.Message));
        }

        private void HandleJsonException(ExceptionContext context, JsonException exception)
        {
            var message = "Formato JSON inválido. Verifique a estrutura dos dados enviados.";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(message));
        }

        private void HandleInvalidCastException(ExceptionContext context, InvalidCastException exception)
        {
            var message = GetFriendlyCastMessage(exception);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(message));
        }

        private void HandleFormatException(ExceptionContext context, FormatException exception)
        {
            var message = "Formato de dados inválido. Verifique os valores enviados.";
            if (exception.Message.Contains("enum") || exception.Message.Contains("Enum"))
            {
                message = "Valor inválido para situação. Use: 0 (Ativo), 1 (Inativo) ou 2 (Cancelado).";
            }
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(message));
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            _logger.LogError("Exceção não tratada: {ExceptionType} - {Message}",
                context.Exception.GetType().Name,
                context.Exception.Message);

            var message = "Ocorreu um erro interno. Tente novamente mais tarde.";

            // Em desenvolvimento, mostrar mais detalhes
#if DEBUG
            message = $"Erro não tratado: {context.Exception.GetType().Name} - {context.Exception.Message}";
#endif

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(message));
        }

        private string GetFriendlyArgumentMessage(ArgumentException exception)
        {
            var message = exception.Message.ToLower();

            if (message.Contains("description"))
                return "A descrição deve ter entre 1 e 50 caracteres.";

            if (message.Contains("enum") || message.Contains("situation"))
                return "Valor inválido para situação. Use: 0 (Ativo), 1 (Inativo) ou 2 (Cancelado).";

            return "Dados inválidos fornecidos.";
        }

        private string GetFriendlyCastMessage(InvalidCastException exception)
        {
            var message = exception.Message.ToLower();

            if (message.Contains("enum"))
                return "Valor inválido para situação. Use: 0 (Ativo), 1 (Inativo) ou 2 (Cancelado).";

            return "Erro de conversão de dados. Verifique os tipos de dados enviados.";
        }
    }
}