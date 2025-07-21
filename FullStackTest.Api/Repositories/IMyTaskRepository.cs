using FullStackTest.Api.Models;

namespace FullStackTest.Api.Repositories
{
    public interface IMyTaskRepository
    {
        Task<IEnumerable<MyTask>?> GetTasksAsync();
        Task<MyTask?> GetTaskByIdAsync(int id);
        Task<int> AddTaskAsync(MyTask myTask);
        Task UpdateTaskAsync(MyTask myTask);
        Task DeleteTaskAsync(int id);
    }
}
