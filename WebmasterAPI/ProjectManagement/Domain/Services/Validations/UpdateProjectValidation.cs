using FluentValidation;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;

namespace WebmasterAPI.ProjectManagement.Domain.Services.Validations;

public class UpdateProjectValidation : AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectValidation()
    {
        RuleFor(x => x.nameProject).NotEmpty().WithMessage("project name cannot be empty");
        RuleFor(x => x.descriptionProject).NotEmpty().WithMessage("the project description cannot be empty");
        RuleFor(x => x.languages).NotEmpty().WithMessage("the languaje of project cannot be empty");
        RuleFor(x => x.frameworks).NotEmpty().WithMessage("the frameworks of the project cannot be empty");
        RuleFor(x => x.methodologies).NotEmpty().WithMessage("the methodologies of the project cannot be empty");
    }
}