using System.Net;
using Erp.Trucks.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Erp.Trucks.Infrastructure.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        switch (ex)
        {
            case TruckStatusIsNotAllowedException or TruckWithGivenUuidAlreadyExistsException:
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        error = new { message = ex.Message }
                    }));
            case TruckWithGivenUuidNotFoundException:
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        error = new { message = ex.Message }
                    }));
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                details = ex.Message
            }
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    
}