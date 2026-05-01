namespace SyncTask.api.Models
{
    public class WorkTaskHour
    {
        public int Id { get; set; }
        public TimeSpan Hours { get; set; }
        public DateTime EntryDate { get; set; }

        //Foreing keys
        public int TaskId { get; set; }
        public int UserId { get; set; }

        //Navigation
        public WorkTask Task { get; set; }
        public User User { get; set; }
    }
}
