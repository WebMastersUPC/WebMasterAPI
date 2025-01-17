using AutoMapper;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Resources;
using WebmasterAPI.Shared.Domain.Repositories;

namespace WebmasterAPI.ProjectManagement.Services;

public class DeliverableService : IDeliverableService
{
    private readonly IDeliverableRepository _deliverableRepository;
    private readonly IProjectRepository<Project> _projectRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeliverableService(IDeliverableRepository deliverableRepository, IProjectRepository<Project> projectRepository,IMapper mapper, IUnitOfWork unitOfWork)
    {
        _deliverableRepository = deliverableRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    
    public async Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception($"No se encontró un proyecto con ID {projectId}");
        }

        var developerId = project.developer_id;
        if (developerId == null)
        {
            throw new Exception($"No se encontró un desarrollador asociado al proyecto con ID {projectId}");
        }

        var highestOrderDeliverable = await _deliverableRepository.GetHighestOrderNumberByProjectIdAsync(projectId);
        var nextOrderNumber = (highestOrderDeliverable != null) ? highestOrderDeliverable.orderNumber + 1 : 1;

        var deliverable = new Deliverable
        {
            title = request.title,
            description = request.description,
            state = "En espera de entrega",
            developerDescription = "",
            deadlineDateValue = request.deadlineDateValue,
            deadlineTime = request.deadlineTime,
            file = "", // Valor predeterminado para file
            projectID = projectId,
            developer_id = developerId.Value,
            orderNumber = nextOrderNumber
        };

        try
        {
            await _deliverableRepository.AddSync(deliverable);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el deliverable: {ex.Message}");
            throw;
        }
    }
    
    public async Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, DeliverableUpdateRequest updateRequest)
    {
        var deliverable = await _deliverableRepository.FindDeliverableByIdAsync(deliverableId);
        if (deliverable == null)
        {
            throw new Exception("Entregable no encontrado.");
        }

        if (deliverable.projectID != projectId)
        {
            throw new Exception("El entregable no pertenece al proyecto especificado.");
        }

        deliverable.title = updateRequest.title;
        deliverable.description = updateRequest.description;
        deliverable.deadlineDateValue = updateRequest.deadlineDateValue;
        deliverable.deadlineTime = updateRequest.deadlineTime.ToString();
        await _deliverableRepository.UpdateAsync(deliverable);
    }

    public async Task<UploadDeliverableResponse> UploadDeliverableAsync(long projectId, long deliverableId, UploadDeliverableRequest upload)
    {
        var deliverable = await _deliverableRepository.FindDeliverableByIdAsync(deliverableId);

        if (deliverable == null)
        {
            throw new Exception("No se encontró el entregable.");
        }
        
        // verifica el entregable con el número de orden más alto que el desarrollador ha subido en el proyecto específico
        var lastUploadedDeliverable = await _deliverableRepository.GetLastUploadedDeliverableByDeveloperIdAndProjectId(projectId);

        // verifica si el entregable que el desarrollador está intentando subir sigue el orden correcto
        if (lastUploadedDeliverable != null && deliverable.orderNumber != lastUploadedDeliverable.orderNumber + 1)
        {
            throw new Exception($"El último entregable subido para el proyecto con ID {projectId} es el entregable con ID {lastUploadedDeliverable.deliverable_id}. " +
                                $"El siguiente entregable que debe subir debe tener el número de orden {lastUploadedDeliverable.orderNumber + 1}.");
        }

        // si el último entregable es nulo y el número de orden del entregable no es 1, lanza una excepción
        if (lastUploadedDeliverable == null && deliverable.orderNumber != 1)
        {
            throw new Exception($"El entregable con ID {deliverableId} no es el primer entregable. Debe comenzar con el entregable número 1.");
        }
        
        // si el último entregable subido ha sido aprobado
        if (lastUploadedDeliverable != null && lastUploadedDeliverable.state != "Aprobado")
        {
            throw new Exception("No puedes subir un nuevo entregable hasta que el último entregable subido haya sido aprobado.");
        }
        
        // si el último entregable subido ha sido aprobado
        if (deliverable.state == "Aprobado")
        {
            throw new Exception("El entregable ya ha sido aprobado. No puedes subir un nuevo entregable.");
        }

        // si la fecha límite del entregable ha pasado
        if (deliverable.deadline < DateTime.Now)
        {
            throw new Exception("La fecha límite de entrega del entregable ha pasado. No puedes subir ningún archivo.");
        }

        deliverable.developerDescription = upload.developerDescription;
        deliverable.file = upload.file;
        deliverable.state = "En espera de revisión";

        await _deliverableRepository.UpdateAsync(deliverable);

        return new UploadDeliverableResponse { developerDescription = deliverable.developerDescription, file = deliverable.file };
    }
    
    public async Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId)
    {
        var deliverables = await _deliverableRepository.ListByProjectIdAsync(projectId);
        return _mapper.Map<List<DeliverableResponse>>(deliverables);
    }

    public async Task<DeliverableResponse> DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, int orderNumber)
    {
        await _deliverableRepository.RemoveDeliverableByProjectIdandDeliverableIdAsync(projectId, orderNumber);
        await _unitOfWork.CompleteAsync();
        var deletedDeliverable = await _deliverableRepository.FindDeliverableByorderNumberAsync(orderNumber);

        return _mapper.Map<DeliverableResponse>(deletedDeliverable);
    }

    public async Task ApproveOrRejectDeliverableAsync(long deliverableId, string newState)
    {
        var deliverable = await _deliverableRepository.FindDeliverableByIdAsync(deliverableId);
        if (deliverable == null)
        {
            throw new Exception("No se encontró el entregable.");
        }

        if (deliverable.state != "En espera de revisión")
        {
            throw new Exception("El desarrollador tuvo que haber enviado un entregable anteriormente.");
        }

        deliverable.state = newState;

        await _deliverableRepository.UpdateAsync(deliverable);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task<DeliverableResponse> GetDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId)
    {
        var deliverable = await _deliverableRepository.FindDeliverableByIdAsync(deliverableId);
        return _mapper.Map<DeliverableResponse>(deliverable);
    }
    
    public async Task<UploadDeliverableResponse> GetUploadedDeliverableByProjectIdAndDeliverableIdAsync(long projectId, long deliverableId)
    {
        var deliverable = await _deliverableRepository.GetDeliverableByProjectIdAndDeliverableIdAsync(projectId, deliverableId);

        if (deliverable == null)
        {
            return null;
        }

        return new UploadDeliverableResponse
        {
            orderNumber= deliverable.orderNumber,
            developerDescription = deliverable.developerDescription,
            file = deliverable.file
        };
    }
}
