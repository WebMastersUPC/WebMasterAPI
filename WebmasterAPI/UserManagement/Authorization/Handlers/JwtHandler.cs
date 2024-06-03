namespace WebmasterAPI.UserManagement.Authorization.Handlers;

public class JwtHandler
{
    private readonly AppSettings _appSettings;

    public JwtHandler(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public string GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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