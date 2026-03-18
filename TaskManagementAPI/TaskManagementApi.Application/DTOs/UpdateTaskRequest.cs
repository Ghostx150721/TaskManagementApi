using TaskManagementApi.Domain.Enums;
using TaskStatus = TaskManagementApi.Domain.Enums.TaskStatus;

namespace TaskManagementApi.Application.DTOs
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}
