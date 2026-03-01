using proyecto.Web.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace proyecto.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var routeWhereExceptionOccured = context.Request.Path.Value ?? "N/A";
                var eventId = $"{Guid.NewGuid():N}-{DateTime.Now:yyMMddHHmmss}";

                var result = new ErrorMiddlewareViewModel
                {
                    Path = routeWhereExceptionOccured,
                    IdEvent = eventId,
                    ListMessages = GetMessages(ex)
                };

                var sb = new StringBuilder();
                sb.AppendLine($"EventId    : {eventId}");
                sb.AppendLine($"Path       : {routeWhereExceptionOccured}");
                sb.AppendLine($"ErrorList  : {string.Join(" | ", result.ListMessages)}");
                sb.AppendLine($"StackTrace : {ex}");

                _logger.LogError(sb.ToString());

                var messagesJson = JsonSerializer.Serialize(result);

                var redirectUrl = QueryHelpers.AddQueryString("/Home/ErrorHandler", "messagesJson", messagesJson);

                context.Response.Redirect(redirectUrl);
            }
        }

        private static List<string> GetMessages(Exception ex)
        {
            if (ex is AggregateException ae)
                return ae.InnerExceptions.Select(e => e.Message).ToList();

            return new List<string> { ex.Message };
        }
    }
}