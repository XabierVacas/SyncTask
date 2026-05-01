using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SyncTask.api.Models
{
    
    public class Tarea
    {
        private static readonly string _rutaArchivo = "C:\\Users\\xabiv\\OneDrive\\Escritorio\\Trabajo\\Aprendizaje C#\\Test_proyect\\SyncTask.api\\Data\\Tareas.json";

        //Constructor
        public Tarea()
        {
        }
        //Constructor
        public Tarea(int id)
        {
            this.Id = id;
            RellenarTarea();
            //this.Nombre = "";
            //this.Descripcion = "";
            //this.Fecha_creacion = DateTime.Now;
            //this.Fecha_vencimiento = DateTime.Now;
            //this.Estado = false;
        }
        public Tarea(string nombre, string descripcion, DateTime fecha_creacion, DateTime fecha_vencimiento, bool estado)
        {
            this.Id = 0;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Fecha_creacion = fecha_creacion;
            this.Fecha_vencimiento = fecha_vencimiento;
            this.Estado = estado;
        }

        //Getters y Setters
        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public string Nombre { get; private set; }
        [JsonInclude]
        public string Descripcion { get; private set; }
        [JsonInclude]
        public DateTime Fecha_creacion { get; private set; }
        [JsonInclude]
        public DateTime Fecha_vencimiento { get; private set; }
        [JsonInclude]
        public bool Estado { get; private set; }

        public string GuardarEnJson()
        {
            try
            {
                // Cargar tareas existentes
                List<Tarea> tareas = CargarDesdeJson();

                // Asignar ID automático
                this.Id = tareas.Count > 0 ? tareas[^1].Id + 1 : 1;

                // Añadir y guardar
                tareas.Add(this);
                Directory.CreateDirectory("Data");
                string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivo, json);

                return "Tarea guardada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public string BorrarDeJson()
        {
            try
            {
                // Cargar tareas existentes
                List<Tarea> tareas = CargarDesdeJson();

                // Borrar ID
                tareas.RemoveAll(x => x.Id == this.Id);

                // Añadir y guardar
                Directory.CreateDirectory("Data");
                string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivo, json);

                return "Tarea borrada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

   

        /// <summary>
        /// Carga todas las tareas desde el archivo JSON.
        /// Es 'static' porque no necesita una instancia concreta de Tarea
        /// para leer el archivo — es una utilidad general de la clase.
        /// </summary>
        private static List<Tarea> CargarDesdeJson()
        {
            // Si el archivo no existe aún, devolvemos lista vacía.
            // Esto ocurre la primera vez que se ejecuta el programa.
            if (!File.Exists(_rutaArchivo))
                return new List<Tarea>();

            string json = File.ReadAllText(_rutaArchivo);
            return JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
        }
        public List<Tarea> CargarTareas()
        {
            return CargarDesdeJson();
        }

        private void RellenarTarea()
        {
           List<Tarea> tareas = CargarDesdeJson();
           Tarea tarea = tareas.Find(x => x.Id == this.Id); // Buscamos la tarea por su ID;

            // Verificamos que la tarea existe antes de usarla
            if (tarea == null)
            {
                Console.WriteLine("Tarea no encontrada");
                return;
            }
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Fecha_creacion = tarea.Fecha_creacion;
            this.Fecha_vencimiento = tarea.Fecha_vencimiento;
            this.Estado = tarea.Estado;
        }
        public bool FinalizarTarea(int id)
        {
            List<Tarea> tareas = CargarDesdeJson();
            Tarea tarea = tareas.Find(x => x.Id == id);
            if (tarea == null)
            {
                Console.WriteLine("Tarea no encontrada");
                return false;
            }
            tarea.Estado = true;
            Directory.CreateDirectory("Data");
            string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_rutaArchivo, json);
            return true;
        }
    }
}
