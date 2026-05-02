using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public interface IProjectRepository
    {
        // CRUD
        string Add(Project project);
        string Delete(int id);
        Project GetById(int id);
        List<Project> GetAll();

        // Extra
        bool UpdateStatus(int id, ProjectStatus status);
        List<WorkTask> GetTasksByProject(int projectId);
        List<WorkTaskHour> GetTaskHoursByProject(int projectId);
    }
}