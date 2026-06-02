using System;
using System.IO;
using System.Text.Json;

namespace NetGestionConvalidacionBecasUni
{
    public static class GestionAlmacenamiento
    {
        private static readonly string NombreArchivo = "datos_programa.json";

        public static void GuardarDatos(DatosPrograma datos)
        {
            try
            {
                var opciones = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(datos, opciones);
                File.WriteAllText(NombreArchivo, jsonString);
                Console.WriteLine();
                Console.WriteLine("====================================================");
                Console.WriteLine($"Datos guardados con éxito en: {Path.GetFullPath(NombreArchivo)}");
                Console.WriteLine("====================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡ERROR al guardar los datos! Detalle: {ex.Message}");
            }
        }

        public static DatosPrograma? CargarDatos()
        {
            try
            {
                if (File.Exists(NombreArchivo))
                {
                    string jsonString = File.ReadAllText(NombreArchivo);
                    var datos = JsonSerializer.Deserialize<DatosPrograma>(jsonString);
                    if (datos != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("====================================================");
                        Console.WriteLine("Datos cargados con éxito desde el archivo JSON.");
                        Console.WriteLine("====================================================");
                        return datos;
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró ningún archivo de guardado previo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡ERROR al cargar los datos! Detalle: {ex.Message}");
            }
            return null;
        }

        public static bool ExisteArchivo()
        {
            return File.Exists(NombreArchivo);
        }
    }
}
