using Microsoft.AspNetCore.Mvc;
using SyncTask.api.Models;
using SyncTask.api.Repositories;
using SyncTask.api.DTOs;
using SyncTask.api.Mappers;

namespace SyncTask.api.Controllers
{
    [ApiController] // Tells ASP.NET this class is an API controller.
    [Route("api/[controller]")]
    public class WorkTaskHourController : ControllerBase
    {
        private readonly IWorkTaskHourRepository _repository;
        private readonly ILogger<WorkTaskHourController> _logger;

        // Dependency Injection — ASP.NET provides the repository and logger automatically
        public WorkTaskHourController(IWorkTaskHourRepository repository, ILogger<WorkTaskHourController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/worktaskhour
        [HttpGet]
        public ActionResult<List<WorkTaskHour>> GetAll()
        {
            _logger.LogInformation("Getting all hours");
            List<WorkTaskHour> workTaskHours = _repository.GetAll();
            List<WorkTaskHourDto> dtos = workTaskHours.Select(t => WorkTaskHourMapper.ToDto(t)).ToList();
            return Ok(dtos);
        }

        // GET api/worktaskhour/5
        [HttpGet("{id}")]
        public ActionResult<WorkTaskHour> GetById(int id)
        {
            _logger.LogInformation("Getting Task Hour {Id}", id);
            WorkTaskHour workTaskHour = _repository.GetById(id);

            if (workTaskHour == null)
                return NotFound("Task Hour not found");

            return Ok(WorkTaskHourMapper.ToDto(workTaskHour));
        }

        // POST api/worktaskhour
        [HttpPost]
        public ActionResult<string> Add([FromBody] CreateWorkTaskHourDto dto)
        {
            _logger.LogInformation("Adding hours to task {TaskId}", dto.TaskId);
            WorkTaskHour WorkTaskHour = new WorkTaskHour
            {
                Hours = dto.Hours,
                TaskId = dto.TaskId,
                UserId = dto.UserId,
            };

            string result = _repository.Add(WorkTaskHour);
            return Ok(result);
        }

        // DELETE api/worktaskhour/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            _logger.LogInformation("Deleting Task Hour {Id}", id);
            string result = _repository.Delete(id);

            if (result == "Task Hour not found")
                return NotFound(result);

            return Ok(result);
        }

        // GET api/worktaskhour/task/{taskId}
        [HttpGet("task/{taskId}")]
        public ActionResult<List<WorkTaskHourDto>> GetByTask(int taskId)
        {
            _logger.LogInformation("Getting hours for task {TaskId}", taskId);
            List<WorkTaskHour> hours = _repository.// GET api/worktaskhour/task/{taskId}
[HttpGet("task/{taskId}")]
public ActionResult<List<WorkTaskHourDto>> GetByTask(int taskId)
{
    _logger.LogInformation("Getting hours for task {TaskId}", taskId);
    List<WorkTaskHour> hours = _repository.GetByTask(taskId);
    return Ok(hours.Select(h => WorkTaskHourMapper.ToDto(h)).ToList());
}(taskId);
            return Ok(hours.Select(h => WorkTaskHourMapper.ToDto(h)).ToList());
        }

    }
}
