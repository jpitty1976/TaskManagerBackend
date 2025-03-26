using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using TaskManager.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskManager.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public async Task<List<TaskItem>> ShowAllTasks() =>
            await Tasks.FromSqlRaw("EXEC ShowAllTasks").ToListAsync();

        public async Task<int> CreateTask(TaskItem task)
        {
            var parameters = new[]
            {
                new SqlParameter("@Title", task.TaskTitle),
                new SqlParameter("@Description", task.TaskDescription),
                new SqlParameter("@Status", task.TaskStatus)
            };
            return await Database.ExecuteSqlRawAsync("EXEC CreateTask @Title, @Description, @Status", parameters);
        }

        public async Task<int> UpdateTask(TaskItem task)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", task.TaskID),
                new SqlParameter("@Title", task.TaskTitle),
                new SqlParameter("@Description", task.TaskDescription),
                new SqlParameter("@Status", task.TaskStatus)
            };
            return await Database.ExecuteSqlRawAsync("EXEC UpdateTask @Id, @Title, @Description, @Status", parameters);
        }

        public async Task<int> DeleteTask(int id) =>
            await Database.ExecuteSqlRawAsync("EXEC DeleteTask @Id", new SqlParameter("@Id", id));

        public async Task<int> ChangeTaskStatus(int id, int status)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Status", status)
            };
            return await Database.ExecuteSqlRawAsync("EXEC ChangeTaskStatus @Id, @Status", parameters);
        }
    }
}
