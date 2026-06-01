
using System;

namespace NetGestionConvalidacionBecasUni
{
    public static class GestionarCarreras
    {
        public static void Ejecutar(ref int cantidadCarreras, ref string carreraPrimera, ref string facultadPrimera, ref string carreraSegunda, ref string facultadSegunda)
        {
            do
            {
                Console.WriteLine("¿Cuántas carreras cursa el estudiante (1 o 2)?");
                string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe ingresar 1 o 2. Intente de nuevo.");
                if (int.TryParse(entrada, out int opcionSeleccionada) && (opcionSeleccionada == 1 || opcionSeleccionada == 2))
                {
                    cantidadCarreras = opcionSeleccionada;
                }
                else
                {
                    cantidadCarreras = -1;
                    Console.WriteLine("¡ERROR! Debe ingresar 1 o 2.");
                }
            } while (cantidadCarreras != 1 && cantidadCarreras != 2);

            Console.WriteLine("--- Selección de la Primera Carrera ---");
            SeleccionCatalogos.ElegirCarrera(ref carreraPrimera, ref facultadPrimera);

            if (carreraPrimera == "Medicina" || carreraPrimera == "Odontología")
            {
                if (cantidadCarreras == 2)
                {
                    Console.WriteLine($"¡AVISO! Los estudiantes de {carreraPrimera} no pueden cursar una segunda carrera. Se ajustará la cantidad de carreras a 1.");
                    cantidadCarreras = 1;
                }
                carreraSegunda = "";
                facultadSegunda = "";
            }

            if (cantidadCarreras == 2)
            {
                bool carreraSegundaValida = false;
                do
                {
                    Console.WriteLine("--- Selección de la Segunda Carrera ---");
                    SeleccionCatalogos.ElegirCarrera(ref carreraSegunda, ref facultadSegunda);

                    if (carreraSegunda == "Medicina" || carreraSegunda == "Odontología")
                    {
                        Console.WriteLine($"¡ERROR! No se permite cursar {carreraSegunda} como segunda carrera por normativa académica.");
                        carreraSegunda = "";
                        facultadSegunda = "";
                    }
                    else if (carreraSegunda == carreraPrimera)
                    {
                        Console.WriteLine("¡ERROR! La segunda carrera no puede ser igual a la primera carrera.");
                        carreraSegunda = "";
                        facultadSegunda = "";
                    }
                    else
                    {
                        carreraSegundaValida = true;
                    }
                } while (!carreraSegundaValida);
            }
        }
    }
}