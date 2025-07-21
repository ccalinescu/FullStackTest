using FullStackTest.Api.Models;
using FullStackTest.Api.Repositories;

namespace FullStackTest.Api.Services
{
    public class MyTaskService : IMyTaskService
    {
        private readonly IMyTaskRepository _myTaskRepository;

        public MyTaskService(IMyTaskRepository myTaskRepository)
        {
            _myTaskRepository = myTaskRepository;
        }

        public async Task<IEnumerable<MyTask>?> GetTasksAsync()
        {
            //await Task.Delay(100);
            return await _myTaskRepository.GetTasksAsync();
        }

        public async Task<MyTask?> GetTaskByIdAsync(int id)
        {
            return await _myTaskRepository.GetTaskByIdAsync(id);
        }

        public async Task<int> AddTaskAsync(MyTask myTask)
        {
            return await _myTaskRepository.AddTaskAsync(myTask);
        }

        public async Task UpdateTaskAsync(MyTask myTask)
        {
            await _myTaskRepository.UpdateTaskAsync(myTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _myTaskRepository.DeleteTaskAsync(id);
        }
    }
}
