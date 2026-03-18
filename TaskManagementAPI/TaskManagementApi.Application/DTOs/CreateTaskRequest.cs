
namespace TaskManagementApi.Application.DTOs
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Descrption { get; set; }
        public DateTime DueDate { get; set; }
    }
}
