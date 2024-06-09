using AutoMapper;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Messaging.Resources;

namespace WebmasterAPI.Messaging.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Message, MessageResource>();
        }
    }
}
