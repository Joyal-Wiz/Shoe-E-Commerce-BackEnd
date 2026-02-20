using ECommerce.Application.Resources;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Responses;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
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
                _logger.LogError(ex, ex.Message); // ✅ Logging added
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                UnauthorizedException => HttpStatusCode.Unauthorized,
                AlreadyExistsException => HttpStatusCode.Conflict,
                BadRequestException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var message = statusCode == HttpStatusCode.InternalServerError
                ? ErrorMessages.ServerError
                : exception.Message;

            var response = ApiResponse<object>.FailureResponse(
                message: message,
                statusCode: (int)statusCode,
                errors: null
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
