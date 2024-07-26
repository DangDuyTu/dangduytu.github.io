
using System.Data.Common;
using Microsoft.Extensions.Options;

//using OpenWorkShopAPIs.Repositories.Role;
using Newtonsoft.Json;

namespace RoadTrafficManagement.Middlewares
{
    public class DbContextMiddleware
    {
        private readonly RequestDelegate _next;

        public DbContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            uint userId = 0;
            string userName = "";

            if (context.User.Identity.IsAuthenticated)
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                var userNameClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

                if (userIdClaim != null)
                {
                    userId = (uint)Convert.ToInt32(userIdClaim);
                }

                if (userNameClaim != null)
                {
                    userName = userNameClaim;
                }
            }

            // Lưu trữ thông tin vào context.Items để sử dụng sau này nếu cần
            context.Items["UserId"] = userId;
            context.Items["UserName"] = userName;

            await _next(context);
        }
    }

}
