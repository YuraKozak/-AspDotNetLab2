using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services;

namespace AspDotNetLab2
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        TimerService _timerS;

        public TimerMiddleware(RequestDelegate next, TimerService timerService)
        {
            _next = next;
            _timerS = timerService;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            if (httpContext.Request.Path.Value.ToLower() == "/services/timer") {
                httpContext.Response.ContentType = "text/html;charset=utf-8";
                await httpContext.Response.WriteAsync($"Час зараз: {_timerS.GetTime()}");
            }
            else {
                await _next.Invoke(httpContext);
            }
        }
    }
}