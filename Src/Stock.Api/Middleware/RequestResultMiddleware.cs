using System.Text.Json;

namespace Stock.Api.Middleware;

public class RequestResultMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await next(context);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

        if (TryExtractStatusCode(responseBody, out var statusCode))
        {
            context.Response.StatusCode = statusCode;
        }

        if (context.Response.StatusCode != 204)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }

    private static bool TryExtractStatusCode(string responseBody, out int statusCode)
    {
        statusCode = 200;

        if (string.IsNullOrWhiteSpace(responseBody))
            return false;

        try
        {
            using var document = JsonDocument.Parse(responseBody);
            var root = document.RootElement;

            if (root.TryGetProperty("statusCode", out var statusCodeElement))
            {
                statusCode = statusCodeElement.GetInt32();
                return true;
            }
        }
        catch (JsonException)
        {
            return false;
        }

        return false;
    }
}