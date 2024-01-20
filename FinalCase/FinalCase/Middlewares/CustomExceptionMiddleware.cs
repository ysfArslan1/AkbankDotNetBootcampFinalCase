using FinalCase.Services;
using FluentValidation;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using System.Net;

namespace FinalCase.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                // Request degerleri console ekranına yazdırırlır.
                string message = "[Request] HTTP: " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);

                await _next.Invoke(context);
                watch.Stop();

                // Response degerleri console ekranına yazdırırlır.
                message = "[Response] HTTP: " + context.Request.Method + " - " + context.Request.Path +
                    " - " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);
            }
            catch (ValidationException validationEx)
            {

                // SeliLog ile validation hatalarınıın kayıt edilmesi
                Log.Error(validationEx, "ValidationError");
                Log.Error(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={validationEx.Message}"
                );

            }
            catch (Exception ex)
            {

                // SeliLog ile hataların kayıt edilmesi
                Log.Error(ex, "UnexpectedError");
                Log.Fatal(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={ex.Message}"
                );

            }

        }

    }
    static public class CustomExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
   
}
