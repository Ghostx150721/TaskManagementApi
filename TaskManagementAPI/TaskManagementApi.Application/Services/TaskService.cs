using System.Threading.Tasks;
using TaskManagementApi.Application.DTOs;
using TaskManagementApi.Application.Interfaces;
using TaskManagementApi.Domain.Entities;
using TaskManagementApi.Domain.Interfaces;

namespace TaskManagementApi.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
        {
            var task = new TaskItem(
                request.Title,
                request.Descrption,
                request.DueDate
                );

            await _repository.AddAsync( task );

            return MapToResponse( task );
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskResponse>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();

            return tasks.Select(MapToResponse);
        }

        public async Task<TaskResponse?> GetByIdAsync(Guid id)
        {
            var tasks = await _repository.GetByIdAsync(id);

            return tasks == null ? null : MapToResponse(tasks);
        }

        public async Task UpdateAsync(Guid id, UpdateTaskRequest request)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null)
                throw new Exception("Task not found");

            task.UpdateDetails(request.Title, request.Description);

            switch (request.Status)
            {
                case Domain.Enums.TaskStatus.InProgress:
                    task.MarkInProgress();
                    break;

                case Domain.Enums.TaskStatus.Completed:
                    task.MarkCompleted(); 
                    break;
            }

            await _repository.UpdateAsync(task);
        }

        private static TaskResponse MapToResponse(TaskItem task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                CreatedAt = task.CreatedAt
            };
        }
    }
}
