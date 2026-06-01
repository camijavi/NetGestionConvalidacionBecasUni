using System;
using System.Globalization;

namespace NetGestionConvalidacionBecasUni
{
    public static class EntradaDatos
    {
        public static string LeerEntradaObligatoria(string mensajeError = "¡ERROR! El campo no puede estar vacío. Intente de nuevo.")
        {
            while (true)
            {
                string entrada = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada.Trim();
                }
                Console.WriteLine(mensajeError);
            }
        }

        public static int ObtenerCIF()
        {
            int cifRetorno = 0;
            bool cifValido;
            do
            {
                cifValido = false;
                Console.WriteLine("Ingrese el CIF del estudiante (8 dígitos):");
                string textoCIF = LeerEntradaObligatoria("Error: El CIF no puede estar vacío. Intente de nuevo.");
                
                if (textoCIF.Length == 8)
                {
                    if (int.TryParse(textoCIF, out cifRetorno) && cifRetorno > 0)
                    {
                        cifValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: El CIF debe ser un número positivo.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: El CIF debe tener exactamente 8 caracteres.");
                }
            } while (!cifValido);
            
            return cifRetorno;
        }

        public static string ObtenerNombre()
        {
            Console.WriteLine("Ingrese el nombre completo del estudiante:");
            return LeerEntradaObligatoria("Error: El nombre no puede estar vacío. Intente de nuevo.");
        }

        public static string ObtenerCorreoUAMV()
        {
            string correo = "";
            bool correoValido;
            do
            {
                correoValido = false;
                Console.WriteLine("Ingrese el correo UAMV del estudiante (@uamv.edu.ni):");
                correo = LeerEntradaObligatoria("Error: El correo no puede estar vacío. Intente de nuevo.");
                
                if (correo.Length >= 12)
                {
                    if (correo.EndsWith("@uamv.edu.ni"))
                    {
                        correoValido = true;
                    }
                }
                
                if (!correoValido)
                {
                    Console.WriteLine("Error: Dominio incorrecto o formato inválido. Use @uamv.edu.ni");
                }
            } while (!correoValido);
            
            return correo;
        }

        public static string ObtenerNombreActividad()
        {
            Console.WriteLine("Ingrese el nombre de la actividad:");
            return LeerEntradaObligatoria("Error: El nombre de la actividad no puede estar vacío. Intente de nuevo.");
        }

        public static string ObtenerFechaActividad()
        {
            string fecha = "";
            bool fechaValida = false;
            do
            {
                Console.WriteLine("Ingrese la fecha en la que se realizó la actividad (DD/MM/AAAA):");
                fecha = LeerEntradaObligatoria("Error: La fecha no puede estar vacía. Intente de nuevo.");
                
                if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (parsedDate.Year == 2026)
                    {
                        fechaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: El año de la fecha debe ser obligatoriamente 2026.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Formato de fecha inválido. Asegúrese de ingresar en formato DD/MM/AAAA (ej. 15/05/2026).");
                }
            } while (!fechaValida);
            
            return fecha;
        }
    }
}
