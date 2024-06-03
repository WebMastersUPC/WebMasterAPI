using Microsoft.Extensions.Options;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.UserManagement.Authorization.Handlers.Interface;
using WebmasterAPI.UserManagement.Authorization.Settings;

namespace WebmasterAPI.UserManagement.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
  
    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();

        var userId = handler.ValidateToken(token);

        if (userId != null)
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);

        await _next(context);
    }
}