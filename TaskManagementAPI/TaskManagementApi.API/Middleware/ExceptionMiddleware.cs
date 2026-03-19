using System.Net;
using System.Text.Json;

namespace TaskManagementApi.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message 
            };

            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
