using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.Support.Domain.Models;
using WebmasterAPI.Support.Domain.Repositories;
using WebmasterAPI.Support.Domain.Services;

namespace WebmasterAPI.Support.Services
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupportRequestService(ISupportRequestRepository supportRequestRepository, IUnitOfWork unitOfWork)
        {
            _supportRequestRepository = supportRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SupportRequest>> ListAsync()
        {
            return await _supportRequestRepository.ListAsync();
        }

        public async Task<SupportRequest> FindByIdAsync(int id)
        {
            return await _supportRequestRepository.FindByIdAsync(id);
        }

        public async Task<SupportRequest> SaveAsync(SupportRequest supportRequest)
        {
            try
            {
                await _supportRequestRepository.AddAsync(supportRequest);
                await _unitOfWork.CompleteAsync();

                return supportRequest;
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw new Exception("An error occurred while saving the support request.", ex);
            }
        }

        public async Task<SupportRequest> UpdateAsync(int id, SupportRequest supportRequest)
        {
            var existingRequest = await _supportRequestRepository.FindByIdAsync(id);

            if (existingRequest == null)
                throw new Exception("Support request not found.");

            existingRequest.Title = supportRequest.Title;
            existingRequest.Description = supportRequest.Description;
            existingRequest.UpdatedAt = DateTime.UtcNow;
            existingRequest.Status = supportRequest.Status;

            try
            {
                _supportRequestRepository.Update(existingRequest);
                await _unitOfWork.CompleteAsync();

                return existingRequest;
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw new Exception("An error occurred while updating the support request.", ex);
            }
        }

        public async Task<SupportRequest> DeleteAsync(int id)
        {
            var existingRequest = await _supportRequestRepository.FindByIdAsync(id);

            if (existingRequest == null)
                throw new Exception("Support request not found.");

            try
            {
                _supportRequestRepository.Remove(existingRequest);
                await _unitOfWork.CompleteAsync();

                return existingRequest;
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw new Exception("An error occurred while deleting the support request.", ex);
            }
        }
    }
}
