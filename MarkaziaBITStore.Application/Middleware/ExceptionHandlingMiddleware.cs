using MarkaziaBITStore.Application.CustomeValidations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");

            var (statusCode, message) = exception switch
            {
                BusinessRuleException => (StatusCodes.Status400BadRequest, exception.Message),
                ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                InvalidOperationException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "Something went wrong!")
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            object response;

            if (_environment.IsDevelopment())
            {
                response = new DevelopmentErrorResponse
                {
                    Error = message,
                    StatusCode = statusCode,
                    Timestamp = DateTime.UtcNow,
                    Details = exception.StackTrace
                };
            }
            else
            {
                response = new ErrorResponse
                {
                    Error = message,
                    StatusCode = statusCode,
                    Timestamp = DateTime.UtcNow
                };
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }



    public class ErrorResponse
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class DevelopmentErrorResponse : ErrorResponse
    {
        public string Details { get; set; }
    }

}
