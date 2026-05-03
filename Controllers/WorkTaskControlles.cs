using Microsoft.AspNetCore.Mvc;
using SyncTask.api.Models;
using SyncTask.api.Repositories;
using SyncTask.api.DTOs;
using SyncTask.api.Mappers;

namespace SyncTask.api.Controllers
{
    [ApiController] // Tells ASP.NET this class is an API controller.
    [Route("api/[controller]")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskRepository _repository;
        private readonly ILogger<WorkTaskController> _logger;

        // Dependency Injection — ASP.NET provides the repository and logger automatically
        public WorkTaskController(IWorkTaskRepository repository, ILogger<WorkTaskController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/worktask
        [HttpGet]
        public ActionResult<List<WorkTask>> GetAll()
        {
            _logger.LogInformation("Getting all tasks");
            List<WorkTask> tasks = _repository.GetAll();
            List<WorkTaskDto> dtos = tasks.Select(t => WorkTaskMapper.ToDto(t)).ToList();
            return Ok(dtos);
        }

        // GET api/worktask/5
        [HttpGet("{id}")]
        public ActionResult<WorkTask> GetById(int id)
        {
            _logger.LogInformation("Getting task {Id}", id);
            WorkTask task = _repository.GetById(id);

            if (task == null)
                return NotFound("Task not found");
        
            return Ok(WorkTaskMapper.ToDto(task));
        }

        // POST api/worktask
        [HttpPost]
        public ActionResult<string> Add([FromBody] CreateWorkTaskDto dto)
        {
            _logger.LogInformation("Adding task {Name}", dto.Name);
            // Convert DTO to WorkTask entity
            WorkTask task = new WorkTask
            {
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Status = WorkTaskStatus.Pending
            };
            string result = _repository.Add(task);
            return Ok(result);
        }

        // DELETE api/worktask/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            _logger.LogInformation("Deleting task {Id}", id);
            string result = _repository.Delete(id);

            if (result == "Task not found")
                return NotFound(result);

            return Ok(result);
        }

        // PUT api/worktask/5/status
        [HttpPut("{id}/status")]
        public ActionResult<string> UpdateStatus(int id, [FromBody] WorkTaskStatus status)
        {
            _logger.LogInformation("Updating status of task {Id}", id);
            bool result = _repository.UpdateStatus(id, status);

            if (!result)
                return NotFound("Task not found");

            return Ok("Status updated successfully");
        }

        // GET api/worktask/5/hours
        [HttpGet("{id}/hours")]
        public ActionResult<List<WorkTaskHour>> GetTaskHours(int id)
        {
            _logger.LogInformation("Getting hours for task {Id}", id);
            List<WorkTaskHour> hours = _repository.GetTaskHoursByTask(id);
            return Ok(hours);
        }
    }
}