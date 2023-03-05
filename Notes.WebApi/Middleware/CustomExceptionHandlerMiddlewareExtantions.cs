namespace Notes.WebApi.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtantions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this
           IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
