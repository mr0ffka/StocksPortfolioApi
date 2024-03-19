
using StocksPortfolio.Domain.Exceptions;
using StocksPortfolio.Models;
using System.Net;
using System.Net.Mime;

namespace StocksPortfolio.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"Something Went wrong while processing {context.Request.Path}");
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            CustomValidationProblemDetails error = ex switch
            {
                BadRequestException badRequestException => HandleBadRequestException(badRequestException, ref statusCode),
                NotFoundException notFoundException => HandleResourceNotFoundException(notFoundException, ref statusCode),
                _ => HandleUnhandledExceptions(ex, ref statusCode)
            };

            if (!context.Response.HasStarted)
            {
                context.Response.Clear();
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(error);
            }
        }

        private CustomValidationProblemDetails HandleUnhandledExceptions(Exception ex, ref HttpStatusCode statusCode)
        {
            statusCode = HttpStatusCode.InternalServerError;
            var error = new CustomValidationProblemDetails
            {
                Title = "An unhandled error occurred while processing this request",
                Status = (int)statusCode,
                Type = nameof(Exception),
                Detail = ex.InnerException?.Message
            };

            return error;
        }

        private CustomValidationProblemDetails HandleResourceNotFoundException(NotFoundException notFoundException, ref HttpStatusCode statusCode)
        {
            statusCode = HttpStatusCode.NotFound;
            var error = new CustomValidationProblemDetails
            {
                Title = notFoundException.Message,
                Status = (int)statusCode,
                Type = nameof(NotFoundException),
                Detail = notFoundException.InnerException?.Message
            };

            return error;
        }

        private CustomValidationProblemDetails HandleBadRequestException(BadRequestException badRequestException, ref HttpStatusCode statusCode)
        {
            statusCode = HttpStatusCode.BadRequest;
            var error = new CustomValidationProblemDetails
            {
                Title = badRequestException.Message,
                Status = (int)statusCode,
                Detail = badRequestException.InnerException?.Message,
                Type = nameof(BadRequestException),
                Errors = badRequestException.Errors
            };

            return error;
        }
    }
}
