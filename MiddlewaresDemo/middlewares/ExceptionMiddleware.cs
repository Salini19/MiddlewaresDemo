using MiddlewaresDemo.Exceptions;
using System.Runtime.CompilerServices;

namespace MiddlewaresDemo.middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;       
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
               
            }
        }
        private static Task HandleException(HttpContext context,Exception ex)
        {
            int statuscode = StatusCodes.Status500InternalServerError;
            switch (ex)
            {
                case NotFoundException :
                    statuscode = StatusCodes.Status404NotFound;
                    break;
                case BadRequestException :
                    statuscode = StatusCodes.Status400BadRequest;
                    break;
                case DivideByZeroException:
                    statuscode = StatusCodes.Status400BadRequest;   
                    break;
                default:
                    break;
            }

            var errroresponse = new ErrorResponse
            {
                StatusCode = statuscode,
                Message = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statuscode;

            return context.Response.WriteAsync(errroresponse.ToString());
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ExceptionMiddlewareExtension
    //{
    //    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    //    {
    //        app.UseMiddleware<ExceptionMiddleware>();
    //    } 
    //}
}
