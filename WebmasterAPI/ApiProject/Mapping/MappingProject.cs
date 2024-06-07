using AutoMapper;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Services.Communication;

namespace WebmasterAPI.ApiProject.Mapping;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<Project, ProjectDto>().ForMember(dto => dto.Id, 
            m => m.MapFrom(p => p.ProjectID));
    }
}