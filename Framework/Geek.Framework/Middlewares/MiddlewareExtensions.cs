using System;
using Microsoft.AspNetCore.Builder;

namespace Geek.Framework.Middlewares
{
    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
