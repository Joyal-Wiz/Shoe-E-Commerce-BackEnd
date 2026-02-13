using ECommerce.Application.Constants;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Responses;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                AlreadyExistsException => (int)HttpStatusCode.Conflict,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };


            var message = exception switch
            {
                _ => exception.Message
            };

            if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                message = ApiMessages.Error.ServerError;
            }

            var response = ApiResponse<object>
                .FailureResponse(message);

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

    }
}
