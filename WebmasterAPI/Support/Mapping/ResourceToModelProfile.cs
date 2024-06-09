using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Resources;
using AutoMapper;

namespace WebmasterAPI.Support.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveSupportRequestResource, SupportRequest>();
        }
    }
}
