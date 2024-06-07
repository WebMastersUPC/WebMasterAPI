using AutoMapper;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.ApiProject.Domain.Services.Communication;

namespace WebmasterAPI.ApiProject.Mapping;

public class MappingProject : Profile
{
    public MappingProject()
    {
        CreateMap<ProjectDto, Project>();
    }
}