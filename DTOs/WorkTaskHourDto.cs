namespace SyncTask.api.DTOs
{
    public class WorkTaskHourDto
    {
        public int Id { get; set; }
        public TimeSpan Hours { get; set; }
        public DateTime EntryDate { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
    }
}