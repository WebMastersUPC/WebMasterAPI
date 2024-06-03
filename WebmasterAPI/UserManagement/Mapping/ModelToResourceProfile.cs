using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Mapping;

public class ModelToResourceProfile : AutoMapper.Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();

        
        // Map for Developer and Enterprise
        CreateMap<Developer, DeveloperResource>();
        CreateMap<Enterprise, EnterpriseResource>();
        
        CreateMap<Developer, DeveloperResponse>();
        CreateMap<Enterprise, EnterpriseResponse>();
        
        CreateMap<Enterprise, EnterpriseUpdateRequest>();
    }
}