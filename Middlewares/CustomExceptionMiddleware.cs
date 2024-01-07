﻿using MovieStoreWebApi.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace MovieStoreWebApi.MiddleWares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILoggerServices _loggerServices;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerServices loggerServices)
        {
            _next = next;
            _loggerServices = loggerServices;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
                _loggerServices.Write(message);
                await _next(context);
                watch.Stop();
                message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerServices.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
                
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error] HTTP" + context.Request.Method + " - " + context.Response.StatusCode + " Error Message" + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerServices.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }


}
