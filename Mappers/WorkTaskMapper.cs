using SyncTask.api.DTOs;
using SyncTask.api.Models;

namespace SyncTask.api.Mappers
{
    public static class WorkTaskMapper
    {
        public static WorkTaskDto ToDto(WorkTask task) => new WorkTaskDto
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            CreationDate = task.CreationDate,
            DueDate = task.DueDate,
            Status = task.Status.ToString(),
            ProjectName = task.Project?.Name,
            UserName = task.User?.Name
        };
    }
}