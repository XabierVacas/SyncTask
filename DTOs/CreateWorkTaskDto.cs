namespace SyncTask.api.DTOs
{
    public class CreateWorkTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}