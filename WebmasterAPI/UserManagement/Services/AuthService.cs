using AutoMapper;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;
using WebmasterAPI.Shared.Domain.Repositories;

namespace WebmasterAPI.Authentication.Services;

public class AuthService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IDeveloperRepository _developerRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUserRepository userRepository, IDeveloperRepository developerRepository, IEnterpriseRepository enterpriseRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _developerRepository = developerRepository;
        _enterpriseRepository = enterpriseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
        var user = await _userRepository.FindByEmailAsync(model.Mail);

        if (user == null || user.password!= model.Password)
        {
            throw new ApplicationException("User not found");
        }

        var response = _mapper.Map<AuthenticateResponse>(user);
        return response;
    }
    public async Task RegisterDeveloperAsync(RegisterDeveloperRequest model)
    {
        if (await _userRepository.ExistsByEmailAsync(model.mail))
        {
            throw new ApplicationException($"Email '{model.mail}' is already taken");
        }
    
        var user = _mapper.Map<User>(model);
        // Agregar Hash de Password

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
        // Agregar Hash de Password

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        var resource = _mapper.Map<EnterpriseResource>(model);
        var enterprise = _mapper.Map<Enterprise>(resource);
        enterprise.user_id = user.user_id;

        await _enterpriseRepository.AddAsync(enterprise);
        await _unitOfWork.CompleteAsync();
    }
    
}