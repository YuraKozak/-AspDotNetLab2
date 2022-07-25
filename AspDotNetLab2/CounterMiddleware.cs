using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services;

namespace AspDotNetLab2
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate _next; //* Посилання на слідуючий делегат.
        private int i = 0; //* Кількість запитів.

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, CounterService counterService, ICounter counter)
        {
            if (httpContext.Request.Path.Value.ToLower() == "/services/random") {
                i++;
                httpContext.Response.ContentType = "text/html;charset=utf-8";
                await httpContext.Response.WriteAsync(
                $"Запит: {i};" +
                $"Counter: {counter.Value};" +
                $"Service: {counterService.Counter.Value}");
            }
            else {
                await _next.Invoke(httpContext);
            }
        }
    }
}