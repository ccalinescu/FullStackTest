using Dapper;
using EzraTest.Api.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace EzraTest.Api.Repositories
{
    public class MyTaskRepository : IMyTaskRepository
    {

        private readonly IDbConnection _dbConnection;

        public MyTaskRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<MyTask>?> GetTasksAsync()
        {
            const string sql = "SELECT Id, Name, Completed FROM Tasks";

            var result = await _dbConnection.QueryAsync<MyTask>(sql);
            return result;
        }

        public async Task<MyTask?> GetTaskByIdAsync(int id)
        {
            const string sql = "SELECT Id, Name, Completed FROM Tasks WHERE Id = @Id";

            var result = await _dbConnection.QuerySingleOrDefaultAsync<MyTask>(sql, new { Id = id });
            return result;
        }
        
        public async Task<int> AddTaskAsync(MyTask myTask)
        {
            const string sql = "INSERT INTO Tasks (Name, Completed) VALUES (@name, 0) RETURNING Id";

            var result = await _dbConnection.ExecuteScalarAsync<int>(sql, new { name = myTask.Name });
            return result;
        }
        
        public async Task UpdateTaskAsync(MyTask myTask)
        {
            const string sql = "UPDATE Tasks SET Name = @Name, Completed = @Completed WHERE Id = @Id";

            await _dbConnection.ExecuteAsync(sql, new { myTask.Id, myTask.Name, myTask.Completed });
        }
        
        public async Task DeleteTaskAsync(int id)
        {
            const string sql = "DELETE FROM Tasks WHERE Id = @id";

            await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}
