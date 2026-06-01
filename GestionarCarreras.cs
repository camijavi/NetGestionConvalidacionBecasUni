
using System;

namespace NetGestionConvalidacionBecasUni
{
    public static class GestionarCarreras
    {
        public static void Ejecutar(ref int nCarreras, ref string c1, ref string f1, ref string c2, ref string f2)
        {
            do
            {
                Console.WriteLine("¿Cuántas carreras cursa el estudiante (1 o 2)?");
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int result) && (result == 1 || result == 2))
                {
                    nCarreras = result;
                }
                else
                {
                    nCarreras = -1;
                }
            } while (nCarreras != 1 && nCarreras != 2);

            Console.WriteLine("--- Selección de la Primera Carrera ---");
            ElegirCarrera(ref c1, ref f1);

            if (nCarreras == 2)
            {
                Console.WriteLine("--- Selección de la Segunda Carrera ---");
                ElegirCarrera(ref c2, ref f2);
            }
        }

        public static void ElegirCarrera(ref string carrera, ref string facultad)
        {
            Console.Write("Ingrese el nombre de la carrera: ");
            carrera = Console.ReadLine() ?? "";
            Console.Write("Ingrese el nombre de la facultad: ");
            facultad = Console.ReadLine() ?? "";
        }
    }
}