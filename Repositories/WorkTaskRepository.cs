using Microsoft.EntityFrameworkCore;
using SyncTask.api.Data;
using SyncTask.api.Models;

namespace SyncTask.api.Repositories
{
    public class WorkTaskRepository : IWorkTaskRepository
    {
        //Database connection
        private readonly AppDbContext _context;

        public WorkTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public string Add(WorkTask workTask)
        {
            try
            {
                workTask.CreationDate = DateTime.Now;
                _context.Tasks.Add(workTask);
                _context.SaveChanges();
                return "Task added";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            try {
                //find task in db
                WorkTask task = _context.Tasks.Find(id);

                //task not found
                if (task == null) return "Task not found";

                //delete task
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return $"Task {task.Id} deleted";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public WorkTask GetById(int id)
        {
            try
            {
                return _context.Tasks
                .Include(t => t.Project)   // Also load the related Project
                .Include(t => t.User)      // Also load the related User
                .FirstOrDefault(t => t.Id == id);  // Get first match or null
            }catch (Exception ex)
            {
                return null;
            }
        }

        public List<WorkTask> GetAll()
        {
            try
            {
                return _context.Tasks.Include(t => t.Project).Include(t => t.User).ToList();  // Execute the query and return a List
            }catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateStatus(int id, WorkTaskStatus status)
        {
            try
            {
                WorkTask task = this.GetById(id);

                if (task == null) return false;
                task.Status = status;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<WorkTaskHour> GetTaskHoursByTask(int taskId)
        {
            try
            {
                return _context.TaskHours.Where(th => th.TaskId == taskId).ToList();
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
