using AutoMapper;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Mapping;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<InsertProjectDto, Project>();
        CreateMap<Project, ProjectDto>().ForMember(dto => dto.project_ID, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<UpdateProjectDto, Project>();
        CreateMap<InsertDeveloperProjectDto, Project>();
        CreateMap<Project, HomeDeveloperProjectDto>().ForMember(dto => dto.project_ID, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<Project, HomeEnterpriseProjectDto>().ForMember(dto => dto.project_ID, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<Project, PostulateDeveloperProjectDto>().ForMember(dto => dto.project_ID, 
            m => m.MapFrom(p => p.projectID));
        CreateMap<Project, AvailableProjectDto>().ForMember(dto => dto.project_ID, 
            m => m.MapFrom(p => p.projectID));

    }
}