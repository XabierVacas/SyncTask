namespace SyncTask.api.DTOs
{
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        // Instead of the full Project object, just the name
        public string ProjectName { get; set; }

        // Instead of the full User object, just the name
        public string UserName { get; set; }
    }
}