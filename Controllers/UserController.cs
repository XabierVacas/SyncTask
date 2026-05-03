using Microsoft.AspNetCore.Mvc;
using SyncTask.api.Models;
using SyncTask.api.Repositories;
using SyncTask.api.DTOs;
using SyncTask.api.Mappers;


namespace SyncTask.api.Controllers
{
    [ApiController] // Tells ASP.NET this class is an API controller.
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            _logger.LogInformation("Getting all users");
            List<User> users = _repository.GetAll();
            List<UserDto> dtos = users.Select(t => UserMapper.ToDto(t)).ToList();
            return Ok(dtos);
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            _logger.LogInformation("Getting user {Id}", id);
            User user = _repository.GetById(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(UserMapper.ToDto(user));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<string> Add([FromBody] CreateUserDto dto)
        {
            _logger.LogInformation("Adding user {Name}", dto.Name);
            // Convert DTO to WorkTask entity
            User user= new User
            {
                Name = dto.Name,
                Email = dto.Email
            };
            string result = _repository.Add(user);
            return Ok(result);
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            _logger.LogInformation("Deleting user {Id}", id);
            string result = _repository.Delete(id);

            if (result == "User not found")
                return NotFound(result);

            return Ok(result);
        }

        // GET api/user/email/{email}
        [HttpGet("email/{email}")]
        public ActionResult<UserDto> GetByEmail(string email)
        {
            _logger.LogInformation("Getting user by email {Email}", email);
            User user = _repository.GetByEmail(email);

            if (user == null)
                return NotFound("User not found");

            return Ok(UserMapper.ToDto(user));
        }

        // GET api/user/{id}/tasks
        [HttpGet("{id}/tasks")]
        public ActionResult<List<WorkTaskDto>> GetTasks(int id)
        {
            _logger.LogInformation("Getting tasks for user {Id}", id);
            List<WorkTask> tasks = _repository.GetTasksByUser(id);
            return Ok(tasks.Select(t => WorkTaskMapper.ToDto(t)).ToList());
        }

        // GET api/user/{id}/hours
        [HttpGet("{id}/hours")]
        public ActionResult<List<WorkTaskHour>> GetHours(int id)
        {
            _logger.LogInformation("Getting hours for user {Id}", id);
            List<WorkTaskHour> hours = _repository.GetTaskHoursByUser(id);
            return Ok(hours);
        }
    }
}
