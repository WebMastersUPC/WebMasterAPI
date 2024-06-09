using AutoMapper;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Mapping;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<InsertProjectDto, Project>();
        CreateMap<Project, ProjectDto>().ForMember(dto => dto.project_Id, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<UpdateProjectDto, Project>();

    }
}