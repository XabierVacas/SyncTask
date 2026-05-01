using Microsoft.EntityFrameworkCore;
using SyncTask.api.Models;

namespace SyncTask.api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WorkTask> Tareas { get; set; }
        public DbSet<Project> Proyectos { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<WorkTaskHour> TareasHoras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SyncTask.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WorkTask → Project 1-N
            // 1 Project -> N WorkTask
            modelBuilder.Entity<WorkTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            // WorkTask → User  1-N
            // 1 User -> N WorkTask
            modelBuilder.Entity<WorkTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            // WorkTaskHour → WorkTask 1-N
            // 1 WorkTask -> N WorkTaskHour
            modelBuilder.Entity<WorkTaskHour>()
                .HasOne(th => th.Task)
                .WithMany(t => t.TaskHours)
                .HasForeignKey(th => th.TaskId);

            // WorkTaskHour → User 1-N
            // 1 User -> N WorkTaskHour
            modelBuilder.Entity<WorkTaskHour>()
                .HasOne(th => th.User)
                .WithMany(u => u.TaskHours)
                .HasForeignKey(th => th.UserId);
        }
    }
}