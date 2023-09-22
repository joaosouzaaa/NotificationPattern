using Domain.Errors;
using ExceptionProject.Exceptions;
using System.Net;
using System.Text.Json;

namespace ExceptionProject.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (exception is InvalidNameException invalidNameException)
            {
                context.Response.StatusCode = (int)invalidNameException.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new List<Notification>()
                {
                    new Notification()
                    {
                        Message = invalidNameException.Message,
                        Key = invalidNameException.Key
                    }
                };

                var jsonResponse = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(jsonResponse);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync("Internal Server Error - Unexpected error.");
            }
        }
    }
}
