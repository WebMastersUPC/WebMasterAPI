using AutoMapper;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.UserManagement.Domain.Models;

namespace WebmasterAPI.Authentication.Services;

public class ProfileService : IProfileService
{
    private readonly IDeveloperRepository _developerRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IDeveloperRepository developerRepository, IEnterpriseRepository enterpriseRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _developerRepository = developerRepository;
        _enterpriseRepository = enterpriseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DeveloperResponse>> ListDevelopersAsync()
    {
        var developers = await _developerRepository.ListAsync();
        var response = _mapper.Map<List<DeveloperResponse>>(developers);
        return response;
    }

    public async Task<DeveloperResponse> GetDeveloperByIdAsync(long id)
    {
        var developer = await _developerRepository.FindByIdAsync(id);
        var response = _mapper.Map<DeveloperResponse>(developer);
        return response;
    }
    
    public async Task<DeveloperResponse> GetDeveloperByDevIdAsync(long id)
    {
        var developer = await _developerRepository.FindByDevIdAsync(id);
        var response = _mapper.Map<DeveloperResponse>(developer);
        return response;
    }

    public async Task<EnterpriseResponse> GetEnterpriseByEnterpriseIdAsync(long enterprise_id)
    {
        var enterprise = await _enterpriseRepository.FindByEnterpriseIdAsync(enterprise_id);
        var response = _mapper.Map<EnterpriseResponse>(enterprise);
        return response;
    }

    public async Task UpdateDeveloperProfileImgAsync(long id, DevImgUpdateRequest updateRequest)
    {
        var developer = await _developerRepository.FindByIdAsync(id);
        if (developer == null)
        {
            throw new Exception("Developer not found");
        }
        
        developer.profile_img_url = updateRequest.profile_img_url;
        
        await _developerRepository.UpdateAsync(developer);
    }

    public async Task UpdateEnterpriseProfileImgAsync(long id, EnterpriseImgUpdateRequest updateRequest)
    {
        var enterprise = await _enterpriseRepository.FindByIdAsync(id);
        if (enterprise == null)
        {
            throw new Exception("Enterprise not found");
        }
        
        enterprise.profile_img_url = updateRequest.profile_img_url;
        
        await _enterpriseRepository.UpdateAsync(enterprise);
    }


    public async Task<EnterpriseResponse> GetEnterpriseByIdAsync(long id)
    {
        var enterprise = await _enterpriseRepository.FindByIdAsync(id);
        var response = _mapper.Map<EnterpriseResponse>(enterprise);
        return response;
    }
    
    

    public async Task UpdateEnterpriseAsync(long id, EnterpriseUpdateRequest updateRequest)
    {
        var enterprise = await _enterpriseRepository.FindByIdAsync(id);
        if (enterprise == null)
        {
            throw new Exception("Enterprise not found");
        }
        enterprise.description = updateRequest.description;
        enterprise.country = updateRequest.country;
        enterprise.RUC = updateRequest.RUC;
        enterprise.phone = updateRequest.phone;
        enterprise.website = updateRequest.website;
        enterprise.sector = updateRequest.sector;
        await _enterpriseRepository.UpdateAsync(enterprise);
    }
    
    public async Task UpdateDeveloperAsync(long id, DeveloperUpdateRequest updateRequest)
    {
        var developer = await _developerRepository.FindByIdAsync(id);
        if (developer == null)
        {
            throw new Exception("Developer not found");
        }
        developer.description = updateRequest.description;
        developer.country = updateRequest.country;
        developer.phone = updateRequest.phone;
        developer.specialties = updateRequest.specialties;
        await _developerRepository.UpdateAsync(developer);
    }
}