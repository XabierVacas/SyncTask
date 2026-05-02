using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public interface IUserRepository
    {
        // CRUD
        string Add(User user);
        string Delete(int id);
        User GetById(int id);
        List<User> GetAll();

        // Extra
        User GetByEmail(string email);
        List<WorkTask> GetTasksByUser(int userId);
        List<WorkTaskHour> GetTaskHoursByUser(int userId);
    }
}