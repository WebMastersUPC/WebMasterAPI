using AutoMapper;
using WebmasterAPI.Projects.Domain.Repositories;
using WebmasterAPI.Projects.Domain.Services;
using WebmasterAPI.Projects.Domain.Services.Communication;
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

    public async Task UpdateDeliverableAsync(long id, DeliverableRequest updateRequest)
    {
        throw new NotImplementedException();
    }
}