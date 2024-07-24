using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using System.Net;
using System.Text.Json;

namespace EcommerceApp.ErrorHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine("-------------------------------------" + exception.ToString());
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is UnauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exception is ItemNotFoundException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }

            var errorResponse = new ApiError
            {
                Status = (int)statusCode,
                Message = message,
                Details = exception.StackTrace // Avoid exposing stack trace in production
            };

            response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(result);
        }
    }
}
