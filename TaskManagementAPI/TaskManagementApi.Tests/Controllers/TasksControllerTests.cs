using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagementApi.API.Controllers;
using TaskManagementApi.Application.DTOs;
using TaskManagementApi.Application.Interfaces;
using Xunit;

namespace TaskManagementApi.Tests.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _serviceMock;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _serviceMock = new Mock<ITaskService>();
            _controller = new TasksController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithTasks()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<TaskResponse>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenTaskExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new TaskResponse { Id = taskId };

            _serviceMock.Setup(s => s.GetByIdAsync(taskId))
                .ReturnsAsync(task);

            // Act
            var result = await _controller.GetById(taskId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(task, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            _serviceMock.Setup(s => s.GetByIdAsync(taskId))
                .ReturnsAsync((TaskResponse?)null);

            // Act
            var result = await _controller.GetById(taskId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var request = new CreateTaskRequest { Title = "Test Task" };
            var createdTask = new TaskResponse
            {
                Id = Guid.NewGuid(),
                Title = "Test Task"
            };

            _serviceMock.Setup(s => s.CreateAsync(request))
                .ReturnsAsync(createdTask);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(TasksController.GetById), createdResult.ActionName);
            Assert.Equal(createdTask, createdResult.Value);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var request = new UpdateTaskRequest { Title = "Updated Task" };

            _serviceMock.Setup(s => s.UpdateAsync(taskId, request))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(taskId, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            _serviceMock.Setup(s => s.DeleteAsync(taskId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(taskId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
