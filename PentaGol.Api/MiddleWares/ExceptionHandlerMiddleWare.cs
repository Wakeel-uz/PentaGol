using PentaGol.Service.Exceptions;

namespace PentaGol.Api.MiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;

        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Handles exceptions and logs them accordingly
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Execute the next middleware in the pipeline
                await this.next(context);
            }
            catch (PentaGolException exception)
            {
                // Handle custom exceptions
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = exception.Code,
                    Error = exception.Message
                });
            }
            catch (Exception exception)
            {
                // Handle all other exceptions and log them
                this.logger.LogError($"{exception.ToString()}\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = 500,
                    Error = exception.Message,
                });
            }
        }
    }
}
