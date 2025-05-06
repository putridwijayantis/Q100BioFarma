using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Q100BioFarma.Infrastructur.Constants;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Infrastructur.Exceptions;

namespace Q100BioFarma.Infrastructur.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            _logger.LogError($"Unhandled exception: {error}");
            _logger.LogError($"Stack Trace: {error.StackTrace}");

            JsonSerializerOptions? options;
            string? json;
            switch (error)
            {
                case HttpResponseLibraryException e:
                    // not found error
                    _logger.LogError($"{error.Message}");
                    response.StatusCode = e.Status;
                    options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    json = JsonSerializer.Serialize(new MessageDto(error.Message), options);
                    await context.Response.WriteAsync(json);
                    break;
                default:
                    // unhandled error
                    _logger.LogInformation($"{error.Message}");
                    response.StatusCode = HttpStatusCodes.BAD_REQUEST;
                    options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    json = JsonSerializer.Serialize(new MessageDto(error.Message), options);
                    await context.Response.WriteAsync(json);
                    break;
            }
        }
    }
}