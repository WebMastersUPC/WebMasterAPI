using WebmasterAPI.Projects.Domain.Models;
using WebmasterAPI.Projects.Domain.Services.Communication;
using WebmasterAPI.Projects.Resources;

namespace WebmasterAPI.Projects.Mapping;

public class ModelToResourceDeliverable : AutoMapper.Profile {

    public ModelToResourceDeliverable()
    {
        CreateMap<Deliverable, DeliverableResponse>();
        CreateMap<Deliverable, DeliverableResource>();
    }
}