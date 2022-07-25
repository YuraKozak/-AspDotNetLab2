using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services;

namespace AspDotNetLab2
{
    class GeneralCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralCounterMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, GeneralCounterServices general) {
            if (context.Request.Path.Value.ToLower() == "/services/general-counter") {
                context.Response.ContentType = "text/html;charset=utf-8";
                general.PlusGeneral();
                await context.Response.WriteAsync($"Всього запитів: {general.GetGeneral()}");
            }
            else {
                general.PlusGeneral();
                await _next.Invoke(context);
            }
        }
    }
}