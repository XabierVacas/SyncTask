using Microsoft.EntityFrameworkCore;
using SyncTask.api.Models;

namespace SyncTask.api.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor receives the options from Program.cs via Dependency Injection
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkTaskHour> TaskHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WorkTask → Project 1-N
            modelBuilder.Entity<WorkTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            // WorkTask → User 1-N
            modelBuilder.Entity<WorkTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            // WorkTaskHour → WorkTask 1-N
            modelBuilder.Entity<WorkTaskHour>()
                .HasOne(th => th.Task)
                .WithMany(t => t.TaskHours)
                .HasForeignKey(th => th.TaskId);

            // WorkTaskHour → User 1-N
            modelBuilder.Entity<WorkTaskHour>()
                .HasOne(th => th.User)
                .WithMany(u => u.TaskHours)
                .HasForeignKey(th => th.UserId);
        }
    }
}