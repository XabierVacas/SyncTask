using SyncTask.api.DTOs;
using SyncTask.api.Models;

namespace SyncTask.api.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDto ToDto(Project project) => new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreationDate = project.CreationDate,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status.ToString(), // Convert enum to string
        };
    }
}
