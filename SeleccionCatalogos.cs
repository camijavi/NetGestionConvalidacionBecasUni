using System;

namespace NetGestionConvalidacionBecasUni
{
    public static class SeleccionCatalogos
    {
        public static void ElegirCarrera(ref string nombreCarrera, ref string nombreFacultad)
        {
            string[] listaCarreras = new string[18];
            listaCarreras[0] = "Ing. de Sistemas";
            listaCarreras[1] = "Ing. Industrial";
            listaCarreras[2] = "Ing. Civil";
            listaCarreras[3] = "Arquitectura";
            listaCarreras[4] = "Medicina";
            listaCarreras[5] = "Psicología";
            listaCarreras[6] = "Nutrición";
            listaCarreras[7] = "Odontología";
            listaCarreras[8] = "Derecho";
            listaCarreras[9] = "Diplomacia y RRII";
            listaCarreras[10] = "Administración de Empresas";
            listaCarreras[11] = "Contabilidad y Finanzas";
            listaCarreras[12] = "Economía Empresarial";
            listaCarreras[13] = "Negocios Internacionales";
            listaCarreras[14] = "Marketing y Publicidad";
            listaCarreras[15] = "Diseño y Comunicación Visual";
            listaCarreras[16] = "Comunicación y RR.PP.";
            listaCarreras[17] = "UAM College";

            bool esValida;
            do
            {
                esValida = false;
                Console.WriteLine("--- SELECCIONE SU CARRERA ---");
                for (int i = 0; i < 18; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaCarreras[i]}");
                }
                
                string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");
                if (int.TryParse(entrada, out int opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= 18)
                {
                    esValida = true;
                    nombreCarrera = listaCarreras[opcionSeleccionada - 1];
                    
                    int indiceCarrera = opcionSeleccionada - 1;
                    if (indiceCarrera >= 0 && indiceCarrera <= 3)
                    {
                        nombreFacultad = "Facultad de Ingeniería y Arquitectura";
                    }
                    else if (indiceCarrera >= 4 && indiceCarrera <= 6)
                    {
                        nombreFacultad = "Facultad de Ciencias Médicas";
                    }
                    else if (indiceCarrera == 7)
                    {
                        nombreFacultad = "Facultad de Odontología";
                    }
                    else if (indiceCarrera >= 8 && indiceCarrera <= 9)
                    {
                        nombreFacultad = "Facultad de Ciencias Jurídicas, Humanidades y RRII";
                    }
                    else if (indiceCarrera >= 10 && indiceCarrera <= 13)
                    {
                        nombreFacultad = "Facultad de Ciencias Administrativas y Económicas";
                    }
                    else if (indiceCarrera >= 14 && indiceCarrera <= 16)
                    {
                        nombreFacultad = "Facultad de Marketing, Diseño y Comunicación";
                    }
                    else
                    {
                        nombreFacultad = "UAM College";
                    }
                }
                else
                {
                    Console.WriteLine($"¡ERROR! La opción ingresada no es válida.");
                }
            } while (!esValida);
            
            Console.WriteLine();
            Console.WriteLine($"Selección exitosa: {nombreCarrera}");
            Console.WriteLine($"Facultad: {nombreFacultad}");
            Console.WriteLine();
        }

        public static void ElegirClub(ref string nombreClub)
        {
            string[] listaClubes = new string[17];
            listaClubes[0] = "Pintura";
            listaClubes[1] = "Teatro";
            listaClubes[2] = "Música";
            listaClubes[3] = "Coro";
            listaClubes[4] = "Fotografía";
            listaClubes[5] = "Modelo";
            listaClubes[6] = "Debate";
            listaClubes[7] = "Mandalas";
            listaClubes[8] = "Intérpretes y traductores";
            listaClubes[9] = "Danza";
            listaClubes[10] = "Jagua Tech";
            listaClubes[11] = "Señas";
            listaClubes[12] = "Jaguar eSports";
            listaClubes[13] = "Madrinas y padrinos";
            listaClubes[14] = "Ambiental";
            listaClubes[15] = "Empresarial";
            listaClubes[16] = "CONANCA";

            bool entradaValida = false;
            do
            {
                Console.WriteLine("Seleccione el club:");
                for (int i = 0; i < 17; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaClubes[i]}");
                }
                
                string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");
                if (int.TryParse(entrada, out int opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= 17)
                {
                    nombreClub = listaClubes[opcionSeleccionada - 1];
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("¡ERROR! La opción no es válida. Intente de nuevo.");
                    Console.WriteLine();
                }
            } while (!entradaValida);
            
            Console.WriteLine($"Club seleccionado: {nombreClub}");
        }

        public static void ElegirVoluntariado(ref string nombreVoluntariado)
        {
            string[] listaVoluntariados = new string[5];
            listaVoluntariados[0] = "Voluntariado por la humanidad";
            listaVoluntariados[1] = "Voluntariado por la niñez";
            listaVoluntariados[2] = "Voluntariado aldeas S.O.S";
            listaVoluntariados[3] = "Voluntariado por el autismo";
            listaVoluntariados[4] = "Voluntariado por el adulto mayor";

            bool entradaValida = false;
            do
            {
                Console.WriteLine("Seleccione el voluntariado:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaVoluntariados[i]}");
                }
                
                string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");
                if (int.TryParse(entrada, out int opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= 5)
                {
                    nombreVoluntariado = listaVoluntariados[opcionSeleccionada - 1];
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("¡ERROR! La opción no es válida. Intente de nuevo.");
                    Console.WriteLine();
                }
            } while (!entradaValida);
            
            Console.WriteLine($"Voluntariado seleccionado: {nombreVoluntariado}");
        }
    }
}
