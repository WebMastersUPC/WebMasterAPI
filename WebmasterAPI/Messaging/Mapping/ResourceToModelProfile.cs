using AutoMapper;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Resources;

namespace WebmasterAPI.Messaging.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveMessageResource, Message>();
        }
    }
}
