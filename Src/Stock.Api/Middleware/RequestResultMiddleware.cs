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

        if (TryExtractRequestResult(responseBody, out var isSuccess, out var hasNotFound))
        {
            context.Response.StatusCode = DetermineStatusCode(context.Request.Method, isSuccess, hasNotFound);
        }

        if (context.Response.StatusCode != 204)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }

    private static bool TryExtractRequestResult(string responseBody, out bool isSuccess, out bool hasNotFound)
    {
        isSuccess = false;
        hasNotFound = false;

        if (string.IsNullOrWhiteSpace(responseBody))
            return false;

        try
        {
            using var document = JsonDocument.Parse(responseBody);
            var root = document.RootElement;

            if (root.TryGetProperty("isSuccess", out var isSuccessElement))
            {
                isSuccess = isSuccessElement.GetBoolean();
                
                if (!isSuccess && root.TryGetProperty("errors", out var errorsElement))
                {
                    if (errorsElement.TryGetProperty("NotFound", out _))
                    {
                        hasNotFound = true;
                    }
                }
                
                return true;
            }
        }
        catch (JsonException)
        {
            return false;
        }

        return false;
    }

    private static int DetermineStatusCode(string httpMethod, bool isSuccess, bool hasNotFound)
    {
        if (hasNotFound)
            return 404;

        return httpMethod.ToUpperInvariant() switch
        {
            "GET" => isSuccess ? 200 : 400,
            "POST" => isSuccess ? 201 : 400,
            "PUT" => isSuccess ? 200 : 400,
            "PATCH" => isSuccess ? 200 : 400,
            "DELETE" => isSuccess ? 204 : 400,
            _ => isSuccess ? 200 : 400
        };
    }
}