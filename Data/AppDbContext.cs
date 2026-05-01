using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SyncTask.api.Models;

namespace SyncTask.api.Data
{
    public class AppDbContext:DbContext
    {
        // Representa la tabla "Tareas" en la base de datos
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Aquí defines qué base de datos usar.
            // Para cambiar a SQL Server solo cambiarías esta línea.
            optionsBuilder.UseSqlite("Data Source=tareas.db");
        }
    }
}
