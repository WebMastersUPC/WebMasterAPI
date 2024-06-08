using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebmasterAPI.Models;
using WebmasterAPI.UserManagement.Authorization.Handlers.Interface;
using WebmasterAPI.UserManagement.Authorization.Settings;

namespace WebmasterAPI.UserManagement.Authorization.Handlers.Implementations;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;

    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var secret = _appSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Sid, user.user_id.ToString()),
                    new Claim(ClaimTypes.Name, user.mail),
                }),
            Expires = DateTime.UtcNow.AddDays(31),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);
      
            var jwtToken = (JwtSecurityToken) validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
      
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}

/*
 Authenticate Response agregar Token
 *     private readonly IJwtHandler _jwtHandler;

    public AuthService(IUserRepository userRepository, IDeveloperRepository developerRepository, IEnterpriseRepository enterpriseRepository, IPasswordHashingService passwordHashingService, IMapper mapper, IUnitOfWork unitOfWork, IJwtHandler jwtHandler)
    {
        // ... otros inicializadores

        _jwtHandler = jwtHandler;
    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
        var user = await _userRepository.FindByEmailAsync(model.Mail);

        if (user == null || !_passwordHashingService.VerifyPassword(model.Password, user.passwordHashed))
        {
            throw new ApplicationException("User not found");
        }

        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user.Email);
        return response;
    }
 */