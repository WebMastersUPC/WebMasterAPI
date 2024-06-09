using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Resources;

namespace WebmasterAPI.ProjectManagement.Mapping;

public class ModelToResourceDeliverable : AutoMapper.Profile {

    public ModelToResourceDeliverable()
    {
        CreateMap<Deliverable, DeliverableResponse>();
        CreateMap<Deliverable, DeliverableResource>();
    }
}