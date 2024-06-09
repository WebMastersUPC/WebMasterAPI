using static System.Runtime.InteropServices.JavaScript.JSType;
using WebmasterAPI.Support.Domain.Models;
using WebMasterApiSupport.Support.Resources;
using AutoMapper;

namespace WebmasterAPI.Support.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<SupportRequest, SupportRequestResource>();
        }
    }
}
