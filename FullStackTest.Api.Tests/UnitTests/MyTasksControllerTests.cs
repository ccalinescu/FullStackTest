using AutoMapper;
using FullStackTest.Api.Controllers;
using FullStackTest.Api.Models;
using FullStackTest.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FullStackTest.Api.Tests.UnitTests
{
    public class MyTasksControllerTests
    {
        private readonly Mock<IMyTaskService> _myTaskServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<MyTasksController>> _loggerMock;
        private readonly MyTasksController _controller;

        public MyTasksControllerTests()
        {
            _myTaskServiceMock = new Mock<IMyTaskService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<MyTasksController>>();
            _controller = new MyTasksController(_myTaskServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithMappedTasks()
        {
            var tasks = new List<MyTask> { new MyTask { Id = 1 } };
            var dtos = new List<MyTaskDto> { new MyTaskDto { Id = 1 } };

            _myTaskServiceMock.Setup(s => s.GetTasksAsync()).ReturnsAsync(tasks);
            _mapperMock.Setup(m => m.Map<IEnumerable<MyTaskDto>>(tasks)).Returns(dtos);

            var result = await _controller.Get();

            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBe(dtos);
        }

        [Fact]
        public async Task Get_ShouldReturnInternalServerError_OnException()
        {
            _myTaskServiceMock.Setup(s => s.GetTasksAsync()).ThrowsAsync(new Exception("fail"));

            var result = await _controller.Get();

            var statusResult = result.ShouldBeOfType<ObjectResult>();
            statusResult.StatusCode.ShouldBe(500);
            statusResult.Value.ShouldBe("Internal server error");
        }

        [Fact]
        public async Task GetById_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            var result = await _controller.Get(0);

            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();
            badRequest.Value.ShouldBe("Invalid task ID.");
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(1)).ReturnsAsync((MyTask)null);

            var result = await _controller.Get(1);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetById_ShouldReturnOkWithMappedTask_WhenTaskExists()
        {
            var task = new MyTask { Id = 1 };
            var dto = new MyTaskDto { Id = 1 };

            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(1)).ReturnsAsync(task);
            _mapperMock.Setup(m => m.Map<MyTaskDto>(task)).Returns(dto);

            var result = await _controller.Get(1);

            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBe(dto);
        }

        [Fact]
        public async Task GetById_ShouldReturnInternalServerError_OnException()
        {
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("fail"));

            var result = await _controller.Get(1);

            var statusResult = result.ShouldBeOfType<ObjectResult>();
            statusResult.StatusCode.ShouldBe(500);
            statusResult.Value.ShouldBe("Internal server error");
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Post(new CreateTaskRequest());

            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();

            var errors = badRequest.Value as IDictionary<string, object>;
            errors.ShouldNotBeNull();
            errors.ShouldContainKey("Name");

            var nameErrors = errors["Name"] as string[];
            nameErrors.ShouldNotBeNull();
            nameErrors.ShouldContain("Required");
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenTaskIdIsInvalid()
        {
            var request = new CreateTaskRequest();
            var mappedTask = new MyTask();
            _mapperMock.Setup(m => m.Map<MyTask>(request)).Returns(mappedTask);
            _myTaskServiceMock.Setup(s => s.AddTaskAsync(mappedTask)).ReturnsAsync(0);

            var result = await _controller.Post(request);

            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();
            badRequest.Value.ShouldBe("Failed to create task.");
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedAtAction_WhenTaskIsCreated()
        {
            var request = new CreateTaskRequest();
            var mappedTask = new MyTask();
            _mapperMock.Setup(m => m.Map<MyTask>(request)).Returns(mappedTask);
            _myTaskServiceMock.Setup(s => s.AddTaskAsync(mappedTask)).ReturnsAsync(5);

            var result = await _controller.Post(request);

            var createdResult = result.ShouldBeOfType<CreatedAtActionResult>();
            createdResult.ActionName.ShouldBe(nameof(_controller.Get));
            createdResult.RouteValues["id"].ShouldBe(5);
            createdResult.Value.ShouldBe(5);
        }

        [Fact]
        public async Task Post_ShouldReturnInternalServerError_OnException()
        {
            var request = new CreateTaskRequest();
            _mapperMock.Setup(m => m.Map<MyTask>(request)).Throws(new Exception("fail"));

            var result = await _controller.Post(request);

            var statusResult = result.ShouldBeOfType<ObjectResult>();
            statusResult.StatusCode.ShouldBe(500);
            statusResult.Value.ShouldBe("Internal server error");
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Put(new UpdateTaskRequest());

            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();

            var errors = badRequest.Value as IDictionary<string, object>;
            errors.ShouldNotBeNull();
            errors.ShouldContainKey("Name");

            var nameErrors = errors["Name"] as string[];
            nameErrors.ShouldNotBeNull();
            nameErrors.ShouldContain("Required");
        }

        [Fact]
        public async Task Put_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            var request = new UpdateTaskRequest { Id = 1 };
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(request.Id)).ReturnsAsync((MyTask)null);

            var result = await _controller.Put(request);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Put_ShouldReturnOk_WhenTaskIsUpdated()
        {
            var request = new UpdateTaskRequest { Id = 1 };
            var existingTask = new MyTask { Id = 1 };
            var mappedTask = new MyTask { Id = 1 };

            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(request.Id)).ReturnsAsync(existingTask);
            _mapperMock.Setup(m => m.Map<MyTask>(request)).Returns(mappedTask);
            _myTaskServiceMock.Setup(s => s.UpdateTaskAsync(mappedTask)).Returns(Task.CompletedTask);

            var result = await _controller.Put(request);

            result.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public async Task Put_ShouldReturnInternalServerError_OnException()
        {
            var request = new UpdateTaskRequest { Id = 1 };
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(request.Id)).ThrowsAsync(new Exception("fail"));

            var result = await _controller.Put(request);

            var statusResult = result.ShouldBeOfType<ObjectResult>();
            statusResult.StatusCode.ShouldBe(500);
            statusResult.Value.ShouldBe("Internal server error");
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            var result = await _controller.Delete(0);

            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();
            badRequest.Value.ShouldBe("Invalid task ID.");
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(1)).ReturnsAsync((MyTask)null);

            var result = await _controller.Delete(1);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenTaskIsDeleted()
        {
            var task = new MyTask { Id = 1 };
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(1)).ReturnsAsync(task);
            _myTaskServiceMock.Setup(s => s.DeleteTaskAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            result.ShouldBeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnInternalServerError_OnException()
        {
            _myTaskServiceMock.Setup(s => s.GetTaskByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("fail"));

            var result = await _controller.Delete(1);

            var statusResult = result.ShouldBeOfType<ObjectResult>();
            statusResult.StatusCode.ShouldBe(500);
            statusResult.Value.ShouldBe("Internal server error");
        }
    }
}
