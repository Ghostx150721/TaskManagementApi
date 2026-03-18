using FluentValidation;
using System.Security.Cryptography.X509Certificates;
using TaskManagementApi.Application.DTOs;

namespace TaskManagementApi.Application.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Descrption)
                .MaximumLength(500);

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow);
        }
    }
}
