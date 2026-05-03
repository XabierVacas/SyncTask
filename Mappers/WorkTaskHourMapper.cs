using SyncTask.api.DTOs;
using SyncTask.api.Models;

namespace SyncTask.api.Mappers
{
    public static class WorkTaskHourMapper
    {
        public static WorkTaskHourDto ToDto(WorkTaskHour hour) => new WorkTaskHourDto
        {
            Id = hour.Id,
            Hours = hour.Hours,
            EntryDate = hour.EntryDate,
            TaskName = hour.Task?.Name,
            UserName = hour.User?.Name
        };
    }
}