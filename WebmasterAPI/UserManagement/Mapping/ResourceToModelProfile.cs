using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Authentication.Resources;
using WebmasterAPI.Models;
using WebmasterAPI.UserManagement.Domain.Models;

namespace WebmasterAPI.Authentication.Mapping;

public class ResourceToModelProfile : AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        // User mapping
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>().ForAllMembers(options => options.Condition((source, target, property) =>
            {
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
        CreateMap<DeveloperResponse, DevApplicant>();
        
        CreateMap<EnterpriseResource, Enterprise>();

        CreateMap<EnterpriseUpdateRequest, Enterprise>()
            .ForAllMembers(options => options.Condition((source, target, sourceMember, destMember) =>
            {
                // Solo asigna el valor si el miembro de origen no es null
                return sourceMember != null;
            }));
    }
}