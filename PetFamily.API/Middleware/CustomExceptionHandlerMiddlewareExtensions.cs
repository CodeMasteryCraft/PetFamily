namespace PetFamily.API.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionsHandlerMiddleware>();
            return app;
        }
    }
}
