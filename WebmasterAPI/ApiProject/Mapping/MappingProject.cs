using AutoMapper;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Services.Communication;

namespace WebmasterAPI.ApiProject.Mapping;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<InsertProjectDto, Project>();
        CreateMap<Project, ProjectDto>().ForMember(dto => dto.Id, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<UpdateProjectDto, Project>();

    }
}