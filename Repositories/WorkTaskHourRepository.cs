using Microsoft.EntityFrameworkCore;
using SyncTask.api.Data;
using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public class WorkTaskHourRepository :IWorkTaskHourRepository
    {
        //Database connection
        private readonly AppDbContext _context;

        public WorkTaskHourRepository(AppDbContext context)
        {
            _context = context;
        }

        public string Add(WorkTaskHour workTaskHour)
        {
            try
            {
                workTaskHour.EntryDate = DateTime.Now;
                _context.TaskHours.Add(workTaskHour);
                _context.SaveChanges();
                return "Task hour added";
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
                WorkTaskHour taskHour = _context.TaskHours.Find(id);
                if (taskHour == null) return "Task hour not found";
                _context.TaskHours.Remove(taskHour);
                _context.SaveChanges();
                return "Task hour deleted";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public WorkTaskHour GetById(int id)
        {
            try
            {
                return _context.TaskHours.Include(th => th.Task).Include(th => th.User).FirstOrDefault(th => th.Id == id); // Get first match or null
            }
            catch (Exception ex)
            {
                return new WorkTaskHour();
            }
        }

        public List<WorkTaskHour> GetByTask(int taskId)
        {
            try
            {
                return _context.TaskHours.Include(th => th.Task).Include(th => th.User).Where(th => th.TaskId == taskId).ToList(); // Get first match or null
            }
            catch (Exception ex)
            {
                return new List<WorkTaskHour>();
            }
        }

        public List<WorkTaskHour> GetAll()
        {
            try
            {
                return _context.TaskHours.Include(th => th.Task).Include(th => th.User).ToList(); // Get first match or null
            }
            catch (Exception ex)
            {
                return new List<WorkTaskHour>();
            }
        }
    }
}
