using AutoMapper;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;
using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.UserManagement.Authorization.Handlers.Interface;
using WebmasterAPI.UserManagement.Domain.Services;

namespace WebmasterAPI.Authentication.Services;

public class AuthService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IDeveloperRepository _developerRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    

    public AuthService(IUserRepository userRepository, IDeveloperRepository developerRepository, IEnterpriseRepository enterpriseRepository, IPasswordHashingService passwordHashingService, IJwtHandler jwtHandler ,IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _developerRepository = developerRepository;
        _enterpriseRepository = enterpriseRepository;
        _passwordHashingService = passwordHashingService;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
        var user = await _userRepository.FindByEmailAsync(model.Mail);

        if (user == null || !_passwordHashingService.VerifyPassword(model.Password, user.passwordHashed))
        {
            throw new ApplicationException("User not found");
        }

        var response = _mapper.Map<AuthenticateResponse>(user);
        response.token = _jwtHandler.GenerateToken(user);
        return response;
    }
    public async Task RegisterDeveloperAsync(RegisterDeveloperRequest model)
    {
        if (await _userRepository.ExistsByEmailAsync(model.mail))
        {
            throw new ApplicationException($"Email '{model.mail}' is already taken");
        }
    
        var user = _mapper.Map<User>(model);
        user.passwordHashed = _passwordHashingService.GetHash(model.password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        var resource = _mapper.Map<DeveloperResource>(model);
        var developer = _mapper.Map<Developer>(resource);
        developer.user_id = user.user_id;

        await _developerRepository.AddAsync(developer);
        await _unitOfWork.CompleteAsync();
    }
    public async Task RegisterEnterpriseAsync(RegisterEnterpriseRequest model)
    {
        var user = _mapper.Map<User>(model);
        user.passwordHashed = _passwordHashingService.GetHash(model.password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        var resource = _mapper.Map<EnterpriseResource>(model);
        var enterprise = _mapper.Map<Enterprise>(resource);
        enterprise.user_id = user.user_id;

        await _enterpriseRepository.AddAsync(enterprise);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
    
        if (user == null) 
            throw new ApplicationException("User not found");
    
        return user;
    }
}