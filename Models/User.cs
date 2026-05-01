using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncTask.api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation — one user has many tasks and many task hours
        // Its a 1-N relationships
        public ICollection<WorkTask> Tasks { get; set; }
        public ICollection<WorkTaskHour> TaskHours { get; set; }
    }
}
