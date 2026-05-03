using SyncTask.api.DTOs;
using SyncTask.api.Models;

namespace SyncTask.api.Mappers
{
    public static class UserMapper
    {
        // Converts a User entity to a
        // UserDto

        public static UserDto ToDto(User user) => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

    }
}
