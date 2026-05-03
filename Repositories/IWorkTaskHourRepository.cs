using SyncTask.api.Models;


namespace SyncTask.api.Repositories
{
    public interface IWorkTaskHourRepository
    {
        // CRUD
        string Add(WorkTaskHour worktaskhour);
        string Delete(int id);
        WorkTaskHour GetById(int id);
        List<WorkTaskHour> GetAll();
        List<WorkTaskHour> GetByTask(int id);
    }
}
