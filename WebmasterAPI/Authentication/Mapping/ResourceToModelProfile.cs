using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Mapping;

public class ResourceToModelProfile : AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        // User mapping
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>().ForAllMembers(options => options.Condition((source, target, property) => {
            if (property == null) return false;
            return property.GetType() != typeof(string) || !string.IsNullOrEmpty((string)property);
        })
        );
        
        // Mapping for Developer and Enterprise
        CreateMap<RegisterDeveloperRequest, User>()
            .ForMember(dest => dest.user_type, opt => opt.MapFrom(src => "D"));
        CreateMap<RegisterEnterpriseRequest, User>()
            .ForMember(dest => dest.user_type, opt => opt.MapFrom(src => "E"));
        
        CreateMap<RegisterDeveloperRequest, Developer>();
        CreateMap<RegisterEnterpriseRequest, Enterprise>();
        
        CreateMap<RegisterDeveloperRequest, DeveloperResource>();
        CreateMap<RegisterEnterpriseRequest, EnterpriseResource>();
        
        CreateMap<DeveloperResource, Developer>();
        CreateMap<EnterpriseResource, Enterprise>();
    }
}