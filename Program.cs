using System;

namespace NetGestionConvalidacionBecasUni
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nombreEstudiante = "", correoUAMV = "", carreraPrimera = "", carreraSegunda = "", facultadPrimera = "", facultadSegunda = "";
            int numeroCIF = 0, cantidadCarreras = 0, totalActividades = 0, cantidadConvalidada = 0;
            string nombreActividad = "", fechaRealizada = "", organizadorFinal = "", tipoDeConvalidacion = "";
            string continuarProceso = "s", detalleReporte = "", estadoBeca = "";
            
            string estadoRestriccion = "libre", restriccionTipo = "libre";
            int horasAcumuladas = 0, partidosAcumulados = 0;

            while (continuarProceso.ToLower() == "s")
            {
                if (totalActividades == 0)
                {
                    Console.Clear();
                    Console.WriteLine("====================================================");
                    Console.WriteLine("       SISTEMA DE CONVALIDACIÓN DE BECAS UAM        ");
                    Console.WriteLine("====================================================");
                    Console.WriteLine();
                    Console.WriteLine("--- IDENTIFICACIÓN DEL ESTUDIANTE ---");
                    
                    numeroCIF = EntradaDatos.ObtenerCIF();
                    nombreEstudiante = EntradaDatos.ObtenerNombre();
                    correoUAMV = EntradaDatos.ObtenerCorreoUAMV();
                    GestionarCarreras.Ejecutar(ref cantidadCarreras, ref carreraPrimera, ref facultadPrimera, ref carreraSegunda, ref facultadSegunda);

                    estadoRestriccion = "libre";
                    restriccionTipo = "libre";
                    horasAcumuladas = 0;
                    partidosAcumulados = 0;
                    detalleReporte = "";
                }

                Console.WriteLine();
                Console.WriteLine($"--- REGISTRO DE ACTIVIDAD #{totalActividades + 1} ---");
                nombreActividad = EntradaDatos.ObtenerNombreActividad();
                fechaRealizada = EntradaDatos.ObtenerFechaActividad();

                GestionConvalidacion.ObtenerTipoConvalidacion(out tipoDeConvalidacion, out cantidadConvalidada, ref restriccionTipo);

                if (tipoDeConvalidacion == "Horas Laborales")
                {
                    horasAcumuladas += cantidadConvalidada;
                }
                else
                {
                    partidosAcumulados += cantidadConvalidada;
                }

                organizadorFinal = GestionConvalidacion.ObtenerOrganizador(ref estadoRestriccion);

                totalActividades++;
                detalleReporte += $"Actividad: {nombreActividad} | Org: {organizadorFinal} | Tipo: {tipoDeConvalidacion} | Cant: {cantidadConvalidada} | Fecha: {fechaRealizada}\n";

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" REGISTRO COMPLETADO. ¿QUÉ DESEA HACER AHORA?");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Añadir otra actividad al estudiante actual");
                Console.WriteLine("2. Generar reporte PDF y finalizar");
                Console.WriteLine("3. Generar reporte e iniciar nuevo estudiante");
                Console.WriteLine("4. Salir sin guardar");
                Console.WriteLine("----------------------------------------------------");
                
                string opcionSalida = EntradaDatos.LeerEntradaObligatoria("¡ERROR! Debe seleccionar una opción. Intente de nuevo.");

                switch (opcionSalida)
                {
                    case "1":
                        continuarProceso = "s";
                        Console.WriteLine($"Continuando con: {nombreEstudiante}");
                        break;
                    case "2":
                    case "3":
                        if (restriccionTipo == "Horas Laborales")
                        {
                            if (horasAcumuladas >= 25)
                            {
                                estadoBeca = $"SÍ (Completó {horasAcumuladas}/25 hrs)";
                            }
                            else
                            {
                                estadoBeca = $"NO (Incompleto: {horasAcumuladas}/25 hrs)";
                            }
                        }
                        else
                        {
                            if (partidosAcumulados >= 7)
                            {
                                estadoBeca = $"SÍ (Completó {partidosAcumulados}/7 partidos)";
                            }
                            else
                            {
                                estadoBeca = $"NO (Incompleto: {partidosAcumulados}/7 partidos)";
                            }
                        }

                        Reportes.GenerarPDF(nombreEstudiante, numeroCIF, correoUAMV, detalleReporte, estadoBeca);

                        if (opcionSalida == "2")
                        {
                            continuarProceso = "n";
                        }
                        else
                        {
                            totalActividades = 0;
                            continuarProceso = "s";
                        }
                        break;
                    case "4":
                        continuarProceso = "n";
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        continuarProceso = "n";
                        break;
                }
            }
        }
    }
}
