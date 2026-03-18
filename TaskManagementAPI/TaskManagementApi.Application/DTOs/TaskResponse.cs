using TaskManagementApi.Domain.Enums;
using TaskStatus = TaskManagementApi.Domain.Enums.TaskStatus;

namespace TaskManagementApi.Application.DTOs
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
