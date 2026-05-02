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
        public User GetById(int id)
        {
            try
            {
                return _context.Users.Include(u => u.Tasks).Include(u => u.TaskHours).FirstOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return _context.Users.ToList();
            }catch (Exception ex)
            {
                return null;
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email);
            }catch (Exception ex)
            {
                return null;
            }
        }

        // GetTasksByUser — load related project
        public List<WorkTask> GetTasksByUser(int userId)
        {
            try
            {
                return _context.Tasks.Include(t => t.Project).Where(t => t.UserId == userId).ToList();
            }catch (Exception ex)
            {
                return null;
            }
        }

        // GetTaskHoursByUser — load related task
        public List<WorkTaskHour> GetTaskHoursByUser(int userId)
        {
            try
            {
                return _context.TaskHours.Include(th => th.Task).Where(th => th.UserId == userId).ToList();
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
