using TaskManagementApi.Application.DTOs;

namespace TaskManagementApi.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponse>> GetAllAsync();

        Task<TaskResponse?> GetByIdAsync(Guid id);

        Task<TaskResponse> CreateAsync(CreateTaskRequest request);

        Task UpdateAsync(Guid id, UpdateTaskRequest request);

        Task DeleteAsync(Guid id);
    }
}
