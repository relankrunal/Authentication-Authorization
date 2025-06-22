using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Custom JWT validation logic can go here
            await _next(context);
        }
    }
}
