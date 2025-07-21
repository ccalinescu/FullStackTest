using FullStackTest.Api.Models;

namespace FullStackTest.Api.Services
{
    public interface IMyTaskService
    {
        Task<IEnumerable<MyTask>?> GetTasksAsync();
        Task<MyTask?> GetTaskByIdAsync(int id);
        Task<int> AddTaskAsync(MyTask myTask);
        Task UpdateTaskAsync(MyTask myTask);
        Task DeleteTaskAsync(int id);
    }
}