using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace PetFamily.API.Middleware
{
    public class CustomExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string message = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(validationException.Message);
                    break;
            }


            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            if (message == string.Empty)
                message = JsonSerializer.Serialize(new { error = exception.Message });

            return context.Response.WriteAsync(message);
        }
    }
}
