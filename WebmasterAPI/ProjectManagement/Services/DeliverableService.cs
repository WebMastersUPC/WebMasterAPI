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
            throw new Exception("Deliverable not found");
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