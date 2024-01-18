﻿using FinalCase.Services;
using FluentValidation;
using Newtonsoft.Json;
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
                watch.Stop();
                await HandleValidationException(context, validationEx, watch);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
            
        }

        // Validation hataları handle edilir.
        private Task HandleValidationException(HttpContext context, ValidationException validationEx, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Validasyon hataları console ekranına yazdırılır
            string message = "[Validation Error] HTTP: " + context.Request.Method + " - " + context.Response.StatusCode +
                    " Error Mesage: " + validationEx.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);


            // Validasyon hataları geri dönülür
            var validationErrors = validationEx.Errors.Select(error =>
            {
                return new
                {
                    propertyName = error.PropertyName,
                    errorMessage = error.ErrorMessage
                };
            });

            var result = JsonConvert.SerializeObject(new { validationErrors }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //  hatalar console ekranına yazdırılır
            string message = "[Error] HTTP: " + context.Request.Method + " - " + context.Response.StatusCode +
                    " Error Mesage: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);

            //  hatalar geri dönülür
            var result =JsonConvert.SerializeObject(new { error  = ex.Message },Formatting.None);
            return context.Response.WriteAsync(result);
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
