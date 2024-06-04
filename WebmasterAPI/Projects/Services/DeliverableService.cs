using AutoMapper;
using WebmasterAPI.Projects.Domain.Models;
using WebmasterAPI.Projects.Domain.Repositories;
using WebmasterAPI.Projects.Domain.Services;
using WebmasterAPI.Projects.Domain.Services.Communication;
using WebmasterAPI.Projects.Resources;
using WebmasterAPI.Shared.Domain.Repositories;

namespace WebmasterAPI.Projects.Services;

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

    public async Task AddDeliverableAsync(CreateDeliverableRequest request)
    {
        // Verificar existencia del proyecto
        var projectExists = await _deliverableRepository.ProjectExistsAsync(request.project_id);
        if (!projectExists)
        {
            throw new Exception($"No se encontró un proyecto con ID {request.project_id}");
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
            project_id = request.project_id,
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
    
}