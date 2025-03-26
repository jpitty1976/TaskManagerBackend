using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.ShowAllTasks());

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            await _context.CreateTask(task);
            return Ok("Task created.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem task)
        {
            task.Id = id;
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
