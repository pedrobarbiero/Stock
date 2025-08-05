using Framework.Application.Repositories;

namespace Stock.Api.Middleware;

public class AutoSaveChangesMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        await next(context);

        if (ShouldSaveChanges(context))
        {
            await unitOfWork.SaveChangesAsync(context.RequestAborted);
        }
    }

    private static bool ShouldSaveChanges(HttpContext context)
    {
        return IsModifyingRequest(context.Request.Method) && IsSuccessStatusCode(context.Response.StatusCode);
    }

    private static bool IsModifyingRequest(string method)
    {
        return method is "POST" or "PUT" or "PATCH" or "DELETE";
    }

    private static bool IsSuccessStatusCode(int statusCode)
    {
        return statusCode is >= 200 and < 300;
    }
}