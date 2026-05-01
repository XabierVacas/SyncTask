using System;
using System.Collections.Generic;

namespace SyncTask.api.Models
{
    public class WorkTask
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        
        // Foreign keys
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Project Project { get; set; }
        public User User { get; set; }
        public ICollection<WorkTaskHour> TaskHours { get; set; }
    }
}