namespace SyncTask.api.DTOs
{
    public class CreateWorkTaskHourDto
    {
        public TimeSpan Hours { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}