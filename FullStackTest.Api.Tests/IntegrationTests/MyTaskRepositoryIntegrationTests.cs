using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FullStackTest.Api.Models;
using FullStackTest.Api.Repositories;
using Microsoft.Data.Sqlite;
using Shouldly;
using Xunit;

namespace FullStackTest.Api.Tests.IntegrationTests
{
    public class MyTaskRepositoryIntegrationTests
    {
        private IDbConnection CreateInMemoryConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            connection.Execute(@"
                CREATE TABLE Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Completed INTEGER NOT NULL
                );
            ");
            return connection;
        }

        private async Task<int> SeedTaskAsync(IDbConnection connection, string name, bool completed)
        {
            var sql = "INSERT INTO Tasks (Name, Completed) VALUES (@Name, @Completed); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, new { Name = name, Completed = completed ? 1 : 0 });
        }

        [Fact]
        public async Task GetTasksAsync_ReturnsTasks()
        {
            using var connection = CreateInMemoryConnection();
            var id1 = await SeedTaskAsync(connection, "Task 1", false);
            var id2 = await SeedTaskAsync(connection, "Task 2", true);

            var repository = new MyTaskRepository(connection);

            var result = await repository.GetTasksAsync();

            result.ShouldNotBeNull();
            result.ShouldContain(t => t.Id == id1 && t.Name == "Task 1" && !t.Completed);
            result.ShouldContain(t => t.Id == id2 && t.Name == "Task 2" && t.Completed);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsTask_WhenFound()
        {
            using var connection = CreateInMemoryConnection();
            var id = await SeedTaskAsync(connection, "Task 1", false);

            var repository = new MyTaskRepository(connection);

            var result = await repository.GetTaskByIdAsync(id);

            result.ShouldNotBeNull();
            result!.Id.ShouldBe(id);
            result.Name.ShouldBe("Task 1");
            result.Completed.ShouldBeFalse();
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsNull_WhenNotFound()
        {
            using var connection = CreateInMemoryConnection();
            var repository = new MyTaskRepository(connection);

            var result = await repository.GetTaskByIdAsync(999);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task AddTaskAsync_ReturnsNewTaskId()
        {
            using var connection = CreateInMemoryConnection();
            var repository = new MyTaskRepository(connection);

            var newTask = new MyTask { Name = "New Task", Completed = false };
            var newId = await repository.AddTaskAsync(newTask);

            newId.ShouldBeGreaterThan(0);

            var inserted = await repository.GetTaskByIdAsync(newId);
            inserted.ShouldNotBeNull();
            inserted!.Name.ShouldBe("New Task");
            inserted.Completed.ShouldBeFalse();
        }

        [Fact]
        public async Task UpdateTaskAsync_UpdatesTask()
        {
            using var connection = CreateInMemoryConnection();
            var repository = new MyTaskRepository(connection);

            var id = await SeedTaskAsync(connection, "Old Name", false);
            var updatedTask = new MyTask { Id = id, Name = "Updated Name", Completed = true };

            await repository.UpdateTaskAsync(updatedTask);

            var result = await repository.GetTaskByIdAsync(id);
            result.ShouldNotBeNull();
            result!.Name.ShouldBe("Updated Name");
            result.Completed.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteTaskAsync_DeletesTask()
        {
            using var connection = CreateInMemoryConnection();
            var repository = new MyTaskRepository(connection);

            var id = await SeedTaskAsync(connection, "To Delete", false);

            await repository.DeleteTaskAsync(id);

            var result = await repository.GetTaskByIdAsync(id);
            result.ShouldBeNull();
        }
    }
}