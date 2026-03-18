using TaskManagementApi.Domain.Enums;
using TaskStatus = TaskManagementApi.Domain.Enums.TaskStatus;

namespace TaskManagementApi.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public TaskStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private TaskItem() { }

        public TaskItem(string title, string description, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = TaskStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkInProgress() => Status = TaskStatus.InProgress;

        public void MarkCompleted() => Status = TaskStatus.Completed;

        public void UpdateDetails(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
