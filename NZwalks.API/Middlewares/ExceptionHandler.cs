using System.Linq.Expressions;
using System.Net;

namespace NZwalks.API.Middlewares
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
            
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex) {

                var errid = Guid.NewGuid();
            //log this exception
             logger.LogError(ex,$"{errid} : {ex.Message}");

            //return custom error response

                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType="application/json";
                 

            var error=new{
                Id=errid,
                Message="We are working on the error"
            };
                await httpContext.Response.WriteAsJsonAsync(error);



            }
        }
    }
}
