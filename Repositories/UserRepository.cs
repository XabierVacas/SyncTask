using Microsoft.EntityFrameworkCore;
using SyncTask.api.Data;
using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public string Add(User user)
        {
            try {
                _context.Users.Add(user);
                _context.SaveChanges();
                return "User added";
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
                User user = _context.Users.Find(id);
                if (user == null) return "User not found";
                _context.Users.Remove(user);
                _context.SaveChanges();
                return "User deleted";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        // GetById — load related tasks and hours
        public User GetById(int id) => _context.Users.Include(u => u.Tasks).Include(u => u.TaskHours).FirstOrDefault(u => u.Id == id);

        public List<User> GetAll() => _context.Users.ToList();
        public User GetByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);

        // GetTasksByUser — load related project
        public List<WorkTask> GetTasksByUser(int userId) => _context.Tasks.Include(t => t.Project).Where(t => t.UserId == userId).ToList();
       
        // GetTaskHoursByUser — load related task
        public List<WorkTaskHour> GetTaskHoursByUser(int userId) => _context.TaskHours.Include(th => th.Task).Where(th => th.UserId == userId).ToList();


    }
}
