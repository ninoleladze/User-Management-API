using System.Diagnostics;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine($" Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            stopwatch.Stop();
            Console.WriteLine($" Response: {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}