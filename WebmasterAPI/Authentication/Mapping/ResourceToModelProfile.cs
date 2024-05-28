using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.Models;

namespace WebmasterAPI.Authentication.Mapping;

public class ResourceToModelProfile : AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>().ForAllMembers(options => options.Condition((source, target, property) => {
            if (property == null) return false;
            return property.GetType() != typeof(string) || !string.IsNullOrEmpty((string)property);
        })
        );
    }
}