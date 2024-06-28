using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Resources;
using AutoMapper;

namespace WebmasterAPI.Support.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveSupportRequestResource, SupportRequest>()
                .ForMember(dest => dest.AttachmentPath, opt => opt.Ignore()); // Ignorar la propiedad AttachmentPath
        }
    }
}
