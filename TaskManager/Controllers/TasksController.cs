using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-db")]
        public IActionResult TestDb()
        {
            var connStr = _context.Database.GetDbConnection().ConnectionString;
            return Ok(connStr);
        }

        [HttpGet("db-name")]
        public IActionResult GetDbName()
        {
            var dbName = _context.Database.GetDbConnection().Database;
            return Ok(new { ConnectedDatabase = dbName });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.ShowAllTasks());

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TaskItem task)
        {
            await _context.CreateTask(task);
            return Ok("Task created.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            task.TaskID = id;
            await _context.UpdateTask(task);
            return Ok("Task updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.DeleteTask(id);
            return Ok("Task deleted.");
        }

        [HttpPatch("{id}/status/{status}")]
        public async Task<IActionResult> ChangeStatus(int id, int status)
        {
            await _context.ChangeTaskStatus(id, status);
            return Ok("Task status updated.");
        }
    }
}
