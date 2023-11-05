using BetterMomshWebAPI.Models.Database_Repository;

namespace BetterMomshWebAPI.Utils
{
    public class JwtTokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenBlacklistService tokenBlacklistService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null && await tokenBlacklistService.IsTokenBlacklistedAsync(token))
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            await _next(context);
        }
    }
}
