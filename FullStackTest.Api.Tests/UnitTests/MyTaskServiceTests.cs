using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using FullStackTest.Api.Services;
using FullStackTest.Api.Repositories;
using FullStackTest.Api.Models;

namespace FullStackTest.Api.Tests.UnitTests
{
    public class MyTaskServiceTests
    {
        private readonly Mock<IMyTaskRepository> _repoMock;
        private readonly MyTaskService _service;

        public MyTaskServiceTests()
        {
            _repoMock = new Mock<IMyTaskRepository>();
            _service = new MyTaskService(_repoMock.Object);
        }

        [Fact]
        public async Task GetTasksAsync_ShouldReturnTasks()
        {
            // Arrange
            var tasks = new List<MyTask> { new MyTask { Id = 1 }, new MyTask { Id = 2 } };
            _repoMock.Setup(r => r.GetTasksAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _service.GetTasksAsync();

            // Assert
            result.ShouldBe(tasks);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            var task = new MyTask { Id = 1 };
            _repoMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync(task);

            // Act
            var result = await _service.GetTaskByIdAsync(1);

            // Assert
            result.ShouldBe(task);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            // Arrange
            _repoMock.Setup(r => r.GetTaskByIdAsync(99)).ReturnsAsync((MyTask?)null);

            // Act
            var result = await _service.GetTaskByIdAsync(99);

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task AddTaskAsync_ShouldReturnNewTaskId()
        {
            // Arrange
            var newTask = new MyTask { Id = 0 };
            _repoMock.Setup(r => r.AddTaskAsync(newTask)).ReturnsAsync(42);

            // Act
            var result = await _service.AddTaskAsync(newTask);

            // Assert
            result.ShouldBe(42);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldCallRepository()
        {
            // Arrange
            var task = new MyTask { Id = 1 };

            // Act
            await _service.UpdateTaskAsync(task);

            // Assert
            _repoMock.Verify(r => r.UpdateTaskAsync(task), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldCallRepository()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteTaskAsync(id);

            // Assert
            _repoMock.Verify(r => r.DeleteTaskAsync(id), Times.Once);
        }
    }
}