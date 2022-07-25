using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services;     //* Використовуємо простір імен.

namespace AspDotNetLab2
{
    public class Startup
    {
        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            services.AddTransient<TimerService>(); //TODO: Робить залежність і кожен раз створює новий об'єкт.
            services.AddScoped<ICounter, RandomCounter>();
            services.AddScoped<CounterService>();
            services.AddSingleton<GeneralCounterServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TimerService timeS)
        {
            app.General();
            app.Timer();
            app.RandomCounter();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseRouting();
            app.UseFileServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            //TODO: Інформація про сервіси.
            app.Map("/services/list", ap => ap.Run(async (context) =>
            {
                var sb = new StringBuilder();           //* Using.System.Text;
                sb.Append("<h1>All services</h1>");
                sb.Append("<table>");
                sb.Append("<tr><th>Type</th><th>LifeTime</th><th>Realization</th></tr>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}
