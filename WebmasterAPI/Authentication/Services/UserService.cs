using AutoMapper;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Models;
using WebmasterAPI.Shared.Domain.Repositories;

namespace WebmasterAPI.Authentication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
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

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);

        if (user == null)
        {
            throw new ApplicationException("User not found");
        }
        return user;
    }

    public async Task RegisterAsync(RegisterRequest model)
    {
        if (_userRepository.ExistsByEmail(model.mail))
        {
            throw new ApplicationException($"Email '{model.mail}' is already taken");
        }

        var user = _mapper.Map<User>(model);

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new ApplicationException($"An error occurred while saving the user: {e.InnerException?.Message ?? e.Message}", e);
        }
    }

    // public Task DeleteAsync(int id)
    // {
    //     throw new NotImplementedException();
    // }
}