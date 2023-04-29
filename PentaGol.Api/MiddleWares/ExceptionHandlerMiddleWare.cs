using PentaGol.Service.Exceptions;

namespace PentaGol.Api.MiddleWares;

public class ExceptionHandlerMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleWare> logger;

    public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (PentaGolException exception)
        {
            context.Response.StatusCode = exception.Code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = exception.Code,
                Error = exception.Message
            });
        }
        catch (Exception exception)
        {
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
