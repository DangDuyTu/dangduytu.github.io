using Api.DTO;
using OpenWorkShopAPIs.Entities;
using OpenWorkShopAPIs.Repositories.InwardEntry;

namespace OpenWorkShopAPIs.Middlewares
{
    public class ValidateUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IInwardEntryRepository<PhieuNhapKho> userRepository)
        {
            // Logic để lấy thông tin người dùng từ yêu cầu (ví dụ: từ header hoặc token)
            int userId = 1; // Lấy từ yêu cầu thực tế của bạn

            var user = new PhieuNhapKho();

            if (user == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Chuyển tiếp request đến middleware tiếp theo
            await _next(context);
        }
    }


}
