public class CustomLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomLoggingMiddleware> _logger;

    public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Handling request: " + context.Request.Path);

        if (context.User.Identity.IsAuthenticated)
        {
            foreach (var claim in context.User.Claims)
            {
                _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
            }
        }
        else
        {
            _logger.LogWarning("User is not authenticated");
        }

        await _next(context);

        _logger.LogInformation("Finished handling request.");
    }
}
