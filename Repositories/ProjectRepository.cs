using SyncTask.api.Models;
using SyncTask.api.Data;
using Microsoft.EntityFrameworkCore;

namespace SyncTask.api.Repositories
{
    public class ProjectRepository  : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) {
            _context = context;
        }

        public string Add(Project project)
        {
            try
            {
                project.CreationDate = DateTime.Now;
                _context.Projects.Add(project);
                _context.SaveChanges();
                return "Project added";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            try
            {
                Project project = _context.Projects.Find(id);

                if (project == null) return "Project not found";

                _context.Projects.Remove(project);
                _context.SaveChanges();

                return $"Project {project.Id} deleted";
            }catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        public Project GetById(int id) => _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id); // Get first match or null

        public List<Project> GetAll() => _context.Projects.ToList(); // Execute the query and return a List even if it is empty

        public bool UpdateStatus(int id, ProjectStatus status)
        {
            Project project = this.GetById(id);

            if (project == null) return false;
            project.Status = status;
            _context.SaveChanges();
            return true;
        }

        public List<WorkTask> GetTasksByProject(int projectId) => _context.Tasks.Include(t => t.User).Where(t => t.ProjectId == projectId).ToList(); // Get first match or null

        public List<WorkTaskHour> GetTaskHoursByProject(int projectId) => _context.TaskHours.Include(th => th.Task).Include(th => th.User)
            .Where(th => th.Task.ProjectId == projectId)
            .ToList();


    }

}
