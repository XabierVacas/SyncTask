using System;
using System.Collections.Generic;
using SyncTask.api.Models;

public class GestorTareas
{
    public string Agregar(string nombre, string descripcion, DateTime fecha_vencimiento)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(nombre))
            return "Error: El nombre no puede estar vacío";

        if (string.IsNullOrWhiteSpace(descripcion))
            return "Error: La descripcion no puede estar vacía";

        if (fecha_vencimiento < DateTime.Now)
            return "Error: La fecha de vencimiento no puede ser anterior a hoy";

        Tarea tarea = new Tarea(nombre, descripcion, DateTime.Now, fecha_vencimiento, false);
        return tarea.GuardarEnJson();
    }

    public string Borrar(int id)
    {
        if (id <= 0)
            return "Error: El ID no puede ser menor o igual a 0";

        Tarea tarea = new Tarea(id);
        if (tarea.Nombre == null)
            return "Tarea no encontrada";

        return tarea.BorrarDeJson();
    }

    public List<Tarea> MostrarTodas() => new Tarea().CargarTareas();
    public Tarea MostrarUno(int id) => id <= 0 ? new Tarea(): new Tarea(id);
    public bool FinalizarTarea(int id) => new Tarea().FinalizarTarea(id);
}