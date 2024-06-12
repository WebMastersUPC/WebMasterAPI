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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public DeliverableService(IDeliverableRepository deliverableRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _deliverableRepository = deliverableRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<DeliverableResponse>> ListDeliverablesAsync()
    {
        var deliverables = await _deliverableRepository.ListAsync();
        var response = _mapper.Map<List<DeliverableResponse>>(deliverables);
        return response;
    }

    public async Task<DeliverableResponse> GetDeliverableByIdAsync(long id)
    {
        var deliverable = await _deliverableRepository.FindDeliverableIdAsync(id);
        var response = _mapper.Map<DeliverableResponse>(deliverable);
        return response;
    }


    public async Task<DeliverableResponse> DeleteDeliverableByIdAsync(long id)
    {
        await _deliverableRepository.RemoveByIdAsync(id);
        await _unitOfWork.CompleteAsync();
        return new DeliverableResponse();
    }

    public async Task UpdateDeliverableAsync(long id, DeliverableUpdateRequest updateRequest)
    {
        var deliverable = await _deliverableRepository.FindDeliverableIdAsync(id);
        if (deliverable == null)
        {
            throw new Exception("Entregable no encontrado.");
        }
        
        deliverable.title=updateRequest.title;
        deliverable.description=updateRequest.description;
        deliverable.state=updateRequest.state;
        await _deliverableRepository.UpdateAsync(deliverable);
        
    }
    
    public async Task UpdateDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId, DeliverableUpdateRequest updateRequest)
    {
        var deliverable = await _deliverableRepository.FindDeliverableIdAsync(deliverableId);
        if (deliverable == null)
        {
            throw new Exception("Deliverable not found");
        }
        
        deliverable.title=updateRequest.title;
        deliverable.description=updateRequest.description;
        deliverable.state=updateRequest.state;
        await _deliverableRepository.UpdateDeliverableByProjectIdandDeliverableIdAsync(projectId, deliverableId, deliverable);
        
    }

    public async Task AddDeliverableAsync(CreateDeliverableRequest request)
    {
        /*// Verificar existencia del proyecto
        var projectExists = await _deliverableRepository.ProjectExistsAsync(request.project_id);
        if (!projectExists)
        {
            throw new Exception($"No se encontró un proyecto con ID {request.project_id}");
        }*/

        // Verificar existencia del desarrollador
        var developerExists = await _deliverableRepository.DeveloperExistsAsync(request.developer_id);
        if (!developerExists)
        {
            throw new Exception($"No se encontró un desarrollador con ID {request.developer_id}");
        }

        var deliverable = new Deliverable
        {
            title = request.title,
            description = request.description,
            state = request.state,
            file = "", // Valor predeterminado para file
            developer_id = request.developer_id
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
    
    public async Task AddDeliverableToProjectAsync(long projectId, CreateDeliverableByProjectIdRequest request)
    {
        // Verificar existencia del proyecto
        var projectExists = await _deliverableRepository.ProjectExistsAsync(projectId);
        if (!projectExists)
        {
            throw new Exception($"No se encontró un proyecto con ID {projectId}");
        }

    

        var deliverable = new Deliverable
        {
            title = request.title,
            description = request.description,
            state = "En espera de entrega",
            developerDescription = "",
            deadline = request.deadline,
            file = "", // Valor predeterminado para file
            projectID = projectId,
            developer_id = request.developer_id
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

    public async Task<UploadDeliverableResponse> UploadDeliverableAsync(long deliverableId, long developerId, UploadDeliverableRequest upload)
    {
        // Obtén el Deliverable de la base de datos
        var deliverable = await _deliverableRepository.FindDeliverableIdAsync(deliverableId);

        // Verifica si el Deliverable existe
        if (deliverable == null)
        {
            return new UploadDeliverableResponse { developerDescription = "No se encontró el entregable." };
        }

        // Verifica si el Developer asociado con el Deliverable es el mismo que el Developer que está intentando subir el archivo
        if (deliverable.developer_id != developerId)
        {
            return new UploadDeliverableResponse { developerDescription = "No está autorizado para subir entregables en este proyecto." };
        }
        
        // Obtén el último entregable subido por el desarrollador
        var lastDeliverable = await _deliverableRepository.GetLastDeliverableByDeveloperIdAsync(developerId);

      
        // Si el último Deliverable existe y no ha sido entregado, no permitas agregar un nuevo Deliverable
        if (lastDeliverable != null && lastDeliverable.state != "Aprobado")
        {
            return new UploadDeliverableResponse 
            { 
                developerDescription = $"No puedes subir un nuevo entregable hasta que el último entregable sea aprobado. " +
                                       $"El último entregable que entregaste tiene el ID: {lastDeliverable.deliverable_id} y su estado es: {lastDeliverable.state}." 
            };
        }
        //
        // // Verifica si el estado del Deliverable es "En espera de entrega"
        // if (deliverable.state != "En espera de entrega")
        // {
        //     return new UploadDeliverableResponse { developerDescription = "The deliverable is not in the 'En espera de entrega' state. You cannot upload files." };
        // }

        // Verifica si la fecha límite del Deliverable ha pasado
        if (deliverable.deadline < DateTime.Now)
        {
            return new UploadDeliverableResponse { developerDescription = "La fecha límite de entrega del entregable ha pasado. No puedes subir ningún archivo." };
        }

        // Actualiza la descripción, el archivo, el estado y la fecha de creación del Deliverable
        deliverable.developerDescription = upload.developerDescription;
        deliverable.file = upload.file;
        deliverable.state = "En espera de revisión";
        deliverable.createdAt = DateTime.UtcNow; // Aquí se establece la fecha de creación

        // Guarda los cambios en la base de datos
        await _deliverableRepository.UpdateAsync(deliverable);

        // Devuelve el Deliverable actualizado
        return new UploadDeliverableResponse { developerDescription = deliverable.developerDescription, file = deliverable.file };
    }

    public async Task<List<DeliverableResponse>> GetDeliverableByProjectIdAsync(long projectId)
    {
        var deliverables = await _deliverableRepository.ListByProjectIdAsync(projectId);
        var response = _mapper.Map<List<DeliverableResponse>>(deliverables);
        return response;
    }
    
    public async Task<DeliverableResponse> DeleteDeliverableByProjectIdandDeliverableIdAsync(long projectId, long deliverableId)
    {
        await _deliverableRepository.RemoveDeliverableByProjectIdandDeliverableIdAsync(projectId, deliverableId);
        await _unitOfWork.CompleteAsync();
        return new DeliverableResponse();
    }
}