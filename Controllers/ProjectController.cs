using Microsoft.AspNetCore.Mvc;
using SyncTask.api.Models;
using SyncTask.api.Repositories;
using SyncTask.api.DTOs;
using SyncTask.api.Mappers;

namespace SyncTask.api.Controllers
{
    [ApiController] // Tells ASP.NET this class is an API controller.
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;
        private readonly ILogger<ProjectController> _logger;

        // Dependency Injection — ASP.NET provides the repository and logger automatically
        public ProjectController(IProjectRepository repository, ILogger<ProjectController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/project
        [HttpGet]
        public ActionResult<List<Project>> GetAll()
        {
            _logger.LogInformation("Getting all projects");
            List<Project> projects = _repository.GetAll();
            List<ProjectDto> dtos = projects.Select(t => ProjectMapper.ToDto(t)).ToList();
            return Ok(dtos);
        }

        // GET api/project/5
        [HttpGet("{id}")]
        public ActionResult<Project> GetById(int id)
        {
            _logger.LogInformation("Getting project {Id}", id);
            Project project = _repository.GetById(id);

            if (project == null)
                return NotFound("Project not found");

            return Ok(ProjectMapper.ToDto(project));
        }

        // POST api/project
        [HttpPost]
        public ActionResult<string> Add([FromBody] CreateProjectDto dto)
        {
            _logger.LogInformation("Adding project {Name}", dto.Name);
            Project project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = ProjectStatus.Pending
            };

            string result = _repository.Add(project);
            return Ok(result);
        }

        // DELETE api/project/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            _logger.LogInformation("Deleting project {Id}", id);
            string result = _repository.Delete(id);

            if (result == "Project not found")
                return NotFound(result);

            return Ok(result);
        }

        // PUT api/project/5/status
        [HttpPut("{id}/status")]
        public ActionResult<string> UpdateStatus(int id, [FromBody] ProjectStatus status)
        {
            _logger.LogInformation("Updating status of project {Id}", id);
            bool result = _repository.UpdateStatus(id, status);

            if (!result)
                return NotFound("Project not found");

            return Ok("Status updated successfully");
        }

        // GET api/project/{id}/tasks
        [HttpGet("{id}/tasks")]
        public ActionResult<List<WorkTaskDto>> GetTasks(int id)
        {
            _logger.LogInformation("Getting tasks for project {Id}", id);
            List<WorkTask> tasks = _repository.GetTasksByProject(id);
            return Ok(tasks.Select(t => WorkTaskMapper.ToDto(t)).ToList());
        }

        // GET api/project/{id}/hours
        [HttpGet("{id}/hours")]
        public ActionResult<List<WorkTaskHour>> GetHours(int id)
        {
            _logger.LogInformation("Getting hours for project {Id}", id);
            List<WorkTaskHour> hours = _repository.GetTaskHoursByProject(id);
            return Ok(hours);
        }

    }
}
