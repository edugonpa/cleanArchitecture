using Serilog.Context;

namespace CleanArchitecture.Api.Middleware;

public class RequestContextLoggingMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public RequestContextLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        using(LogContext.PushProperty("CorrelationId", GetCorrelationId(httpContext)))
        {
            return _next(httpContext);
        }
    }

    private string GetCorrelationId(HttpContext httpcontext)
    {
        httpcontext.Request.Headers.TryGetValue(
            CorrelationIdHeaderName, 
            out var correlationId
        );

        return correlationId.FirstOrDefault() ?? httpcontext.TraceIdentifier;
    }
}