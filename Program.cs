using SyncTask.api.Data;

using (var db = new AppDbContext())
{
    db.Database.EnsureCreated();
}
//GestorTareas gestor = new GestorTareas();
//int opcion = 0;
//int idBorrar = 0;
//int idMostrar = 0;

//while (true)
//{
//    Console.WriteLine(" Menu:\n1. Agregar\n2. Borrar\n3. Mostrar todas\n4. Mostrar una\n5. Finalizar tarea\n0. Salir\n");
    
//    while (!int.TryParse(Console.ReadLine(), out opcion))
//        Console.WriteLine("Opción no válida, ingrese un número: ");

//    switch (opcion)
//    {
//        case 1:
//            Console.WriteLine("Nombre: ");
//            string nombre = Console.ReadLine();
//            Console.WriteLine("Descripcion: ");
//            string descripcion = Console.ReadLine();
//            Console.WriteLine("Fecha de vencimiento (dd/MM/yyyy): ");
//            DateTime fecha_vencimiento;
//            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha_vencimiento))
//                Console.WriteLine("Formato incorrecto, intente de nuevo (dd/MM/yyyy): ");
//            Console.WriteLine(gestor.Agregar(nombre, descripcion, fecha_vencimiento));
//            break;

//        case 2:
//            Console.WriteLine("ID a borrar: ");
//            while (!int.TryParse(Console.ReadLine(), out idBorrar))
//                Console.WriteLine("Opción no válida, ingrese un número: ");
//            Console.WriteLine(gestor.Borrar(idBorrar));
//            break;

//        case 3:
//            foreach (var t in new GestorTareas().MostrarTodas())
//                Console.WriteLine($"ID: {t.Id}, Nombre: {t.Nombre}, Estado: " + (t.Estado ? "Finalizada" : "No finalizada"));
//            break;

//        case 4:
//            Console.WriteLine("ID a mostrar: ");
//            while (!int.TryParse(Console.ReadLine(), out idMostrar))
//                Console.WriteLine("Opción no válida, ingrese un número: ");
//            Tarea tarea = new GestorTareas().MostrarUno(idMostrar);
//            if (tarea.Nombre != null)
//                Console.WriteLine($"ID: {tarea.Id}, Nombre: {tarea.Nombre}, Descripcion: {tarea.Descripcion}, Fecha de creacion: {tarea.Fecha_creacion}, Fecha de vencimiento: {tarea.Fecha_vencimiento}, Estado: " + (tarea.Estado ? "Finalizada" : "No finalizada"));
//            break;
//        case 5:
//            Console.WriteLine("ID de la tarea a finalizar: ");
//            while (!int.TryParse(Console.ReadLine(), out idMostrar))
//                Console.WriteLine("Opción no válida, ingrese un número: ");
//            Console.WriteLine(new GestorTareas().FinalizarTarea(idMostrar) ? "Tarea finalizada" : "Tarea no encontrada");
//            break;

//        case 0:
//            return;
//    }
