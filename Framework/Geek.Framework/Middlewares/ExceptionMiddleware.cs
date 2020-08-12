using System;
using System.Net;
using System.Threading.Tasks;
using Geek.Framework.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Geek.Framework.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;
        private IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            this.logger = logger;
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            bool isCatched = false;
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                await ExceptionHandlerAsync(context, e);
                isCatched = true;
            }
            finally
            {
                if (!isCatched && context.Response.StatusCode != 200)
                {
                    var msg = "";
                    switch (context.Response.StatusCode)
                    {
                        case 401:
                            msg = "该服务未授权";
                            break;
                        case 403:
                            msg = "请求错误或未授权";
                            break;
                        case 404:
                            msg = "未找到服务";
                            break;
                        case 500:
                            msg = "运行错误";
                            break;
                        case 502:
                            msg = "请求错误";
                            break;
                        default:
                            msg = "未知错误";
                            break;
                    }
                    await ExceptionHandlerAsync(context, context.Response.StatusCode, msg);
                }
            }
        }
        private async Task ExceptionHandlerAsync(HttpContext context, int statusCode, string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8;";
            var operateStatus = new
            {
                success = false,
                msg,
                statusCode
            };
            var sta = context.Response.HasStarted;
            if (sta)
            {
                await Task.FromResult(operateStatus);
            }
            var result = JsonConvert.SerializeObject(operateStatus);
            await context.Response.WriteAsync(result);
        }


        private async Task ExceptionHandlerAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json;charset=utf-8;";

            LoggerHelper.ErrorLog.Error(e.Message, e);
            LoggerHelper.WriteFileLog(NLog.LogLevel.Error, LogType.Api, e.Message, e);
            if (environment.IsDevelopment())
            {
                var operateDevStatus = new
                {
                    success = false,
                    msg = e.Message,
                    statusCode = context.Response.StatusCode
                };
                var result = JsonConvert.SerializeObject(operateDevStatus);
                await context.Response.WriteAsync(result);
            }
            else
            {
                var operateProStatus = new
                {
                    success = false,
                    msg = "服务器错误，请联系管理员",
                    statusCode = context.Response.StatusCode
                };
                var result = JsonConvert.SerializeObject(operateProStatus);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
