using System;

namespace NetGestionConvalidacionBecasUni
{
    public static class GestionConvalidacion
    {
        public static void ObtenerTipoConvalidacion(out string tipoConvalidacion, out int cantidadConvalidada, ref string restriccionTipo)
        {
            int opcionSeleccionada = 0;
            bool entradaValida;
            tipoConvalidacion = "";
            cantidadConvalidada = 0;
            
            do
            {
                entradaValida = true;
                
                if (restriccionTipo != "libre")
                {
                    Console.WriteLine($"Tipo de convalidación fijado por registros previos: {restriccionTipo}");
                    if (restriccionTipo == "Horas Laborales")
                    {
                        opcionSeleccionada = 1;
                    }
                    else
                    {
                        opcionSeleccionada = 2;
                    }
                }
                else
                {
                    Console.WriteLine("Elija qué desea convalidar:");
                    Console.WriteLine("1. Horas Laborales");
                    Console.WriteLine("2. Partidos Laborales");
                    
                    string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");
                    if (!int.TryParse(entrada, out opcionSeleccionada) || (opcionSeleccionada != 1 && opcionSeleccionada != 2))
                    {
                        Console.WriteLine("¡ERROR! Debe ingresar 1 o 2.");
                        entradaValida = false;
                    }
                }
            } while (!entradaValida);
            
            if (opcionSeleccionada == 1)
            {
                tipoConvalidacion = "Horas Laborales";
                restriccionTipo = "Horas Laborales";
                
                do
                {
                    entradaValida = true;
                    Console.WriteLine("Ingrese el número de Horas Laborales (1 - 25):");
                    string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe ingresar un número. Intente de nuevo.");
                    if (!int.TryParse(entrada, out cantidadConvalidada) || cantidadConvalidada <= 0 || cantidadConvalidada > 25)
                    {
                        Console.WriteLine("¡ERROR! Rango válido: 1 - 25.");
                        entradaValida = false;
                    }
                } while (!entradaValida);
            }
            else
            {
                tipoConvalidacion = "Partidos";
                restriccionTipo = "Partidos";
                
                do
                {
                    entradaValida = true;
                    Console.WriteLine("Ingrese el número de Partidos Laborales (1 - 7):");
                    string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe ingresar un número. Intente de nuevo.");
                    if (!int.TryParse(entrada, out cantidadConvalidada) || cantidadConvalidada <= 0 || cantidadConvalidada > 7)
                    {
                        Console.WriteLine("¡ERROR! Rango válido: 1 - 7.");
                        entradaValida = false;
                    }
                } while (!entradaValida);
            }
        }

        public static string ObtenerOrganizador(ref string estadoRestriccion)
        {
            string organizadorRetorno = "";
            string[] organizadoresOpciones = new string[5];
            organizadoresOpciones[0] = "Club";
            organizadoresOpciones[1] = "Voluntariado";
            organizadoresOpciones[2] = "Vida Estudiantil";
            organizadoresOpciones[3] = "Biblioteca";
            organizadoresOpciones[4] = "Admisión";

            bool entradaValida;
            
            do
            {
                entradaValida = true;
                Console.WriteLine();
                Console.WriteLine("Seleccione el organizador de la actividad:");
                
                if (estadoRestriccion != "libre")
                {
                    Console.WriteLine($"(AVISO: Solo se permite Vida Estudiantil debido a registro previo en {estadoRestriccion})");
                }
                
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{i + 1}. {organizadoresOpciones[i]}");
                }
                
                string entrada = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");
                if (int.TryParse(entrada, out int opcionPrincipal) && opcionPrincipal >= 1 && opcionPrincipal <= 5)
                {
                    if (estadoRestriccion != "libre" && opcionPrincipal != 3)
                    {
                        Console.WriteLine($"¡ERROR! Por normativa, no puede combinar {estadoRestriccion} con esta opción.");
                        Console.WriteLine("Solo puede convalidar con Vida Estudiantil.");
                        entradaValida = false;
                    }
                    else
                    {
                        switch (opcionPrincipal)
                        {
                            case 1:
                                string clubSeleccionado = "";
                                SeleccionCatalogos.ElegirClub(ref clubSeleccionado);
                                organizadorRetorno = "Club " + clubSeleccionado;
                                break;
                            case 2:
                                string nombreVoluntariado = "";
                                SeleccionCatalogos.ElegirVoluntariado(ref nombreVoluntariado);
                                organizadorRetorno = nombreVoluntariado;
                                break;
                            case 3:
                                organizadorRetorno = "Vida Estudiantil";
                                break;
                            case 4:
                                organizadorRetorno = "Biblioteca";
                                estadoRestriccion = "Biblioteca";
                                break;
                            case 5:
                                organizadorRetorno = "Admisión";
                                estadoRestriccion = "Admisión";
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("¡ERROR! Opción inválida.");
                    entradaValida = false;
                }
            } while (!entradaValida);
            
            return organizadorRetorno;
        }
    }
}
