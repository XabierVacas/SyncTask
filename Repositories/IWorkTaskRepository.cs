using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public interface IWorkTaskRepository
    {
        // CRUD
        string Add(WorkTask task);
        string Delete(int id);
        WorkTask GetById(int id);
        List<WorkTask> GetAll();

        // Extra
        bool UpdateStatus(int id, WorkTaskStatus status);

        List<WorkTaskHour> GetTaskHoursByTask(int taskId);
    }
}
