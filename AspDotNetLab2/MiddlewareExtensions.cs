using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetLab2
{
    public static class MeddlewareExtensions
    {
        public static IApplicationBuilder RandomCounter
            (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CounterMiddleware>();
        }

        public static IApplicationBuilder Timer
            (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimerMiddleware>();
        }

        public static IApplicationBuilder General
            (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GeneralCounterMiddleware>();
        }
    }
}