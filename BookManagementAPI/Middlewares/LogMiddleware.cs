namespace BookManagementAPI.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************************** Middleware log ****************************************");
            Console.WriteLine($"Request method: {ctx.Request.Method}");
            Console.WriteLine($"Request path: {ctx.Request.Path}");
            Console.WriteLine($"Request query string: {ctx.Request.QueryString}");
            Console.WriteLine("****************************************** End of log ********************************************");
            Console.ResetColor();
            await _next.Invoke(ctx);
        }
    }
}
