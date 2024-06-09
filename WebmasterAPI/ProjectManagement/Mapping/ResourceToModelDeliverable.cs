
using WebmasterAPI.Authentication.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Resources;

namespace WebmasterAPI.ProjectManagement.Mapping;

public class ResourceToModelDeliverable : AutoMapper.Profile
{
    public ResourceToModelDeliverable()
    {
        // DeliverableResource to Deliverable
        CreateMap<DeliverableResource, Deliverable>();
        CreateMap<UpdateRequest,Deliverable>().ForAllMembers(options =>options.Condition((source, target, property) =>
            {
                if (property == null) return false;
                return property.GetType() != typeof(string) || !string.IsNullOrEmpty((string)property);
            })
        );
        
        CreateMap<CreateDeliverableRequest, Deliverable>();
        
        CreateMap<CreateDeliverableRequest, DeliverableResource>();
        
        // DeliverableRequest to Deliverable
        CreateMap<DeliverableRequest, Deliverable>()
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description));

        // Deliverable to DeliverableResource
        CreateMap<Deliverable, DeliverableResource>()
            .ForMember(dest=>dest.title, opt=>opt.MapFrom(src=>src.title))
            .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.state))
            .ForMember(dest => dest.file, opt => opt.MapFrom(src => src.file))
            .ForMember(dest => dest.project_id, opt => opt.MapFrom(src => src.projectID))
            .ForMember(dest => dest.developer_id, opt => opt.MapFrom(src => src.developer_id));
        
        CreateMap<DeliverableRequest, Deliverable>()
            .ForAllMembers(options => options.Condition((source, target, sourceMember, destMember) =>
            {
                // Solo asigna el valor si el miembro de origen no es null
                return sourceMember != null;
            }));
        
    }
    
}