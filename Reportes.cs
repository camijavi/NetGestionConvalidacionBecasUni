using System;

namespace NetGestionConvalidacionBecasUni
{
    public static class Reportes
    {
        public static void GenerarPDF(string nombre, int cif, string correo, string tabla, string aprobado)
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("            REPORTE DE CONVALIDACIÓN PDF            ");
            Console.WriteLine("====================================================");
            Console.WriteLine($"ESTUDIANTE: {nombre}");
            Console.WriteLine($"CIF:        {cif}");
            Console.WriteLine($"CORREO:     {correo}");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("DETALLE DE ACTIVIDADES:");
            Console.WriteLine(tabla);
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"APROBACIÓN VIDA ESTUDIANTIL: {aprobado}");
            Console.WriteLine("====================================================");
            Console.WriteLine("Reporte exportado exitosamente.");
        }
    }
}
