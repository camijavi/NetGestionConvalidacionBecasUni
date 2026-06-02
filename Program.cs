using System;
using System.Collections.Generic;

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

            string idEstudiante = "";
            List<Actividad> listaActividades = new List<Actividad>();

            bool cargadoDeArchivo = false;
            bool mostrarMenuPrimero = false;

            // Preguntar si desea cargar datos al iniciar el programa
            if (GestionAlmacenamiento.ExisteArchivo())
            {
                Console.WriteLine("====================================================");
                Console.WriteLine("Se detectó un archivo de datos guardado previamente.");
                Console.WriteLine("¿Desea cargar los datos de la sesión anterior? (s/n):");
                Console.WriteLine("====================================================");
                string respuesta = Console.ReadLine() ?? "";
                if (respuesta.Trim().ToLower() == "s")
                {
                    var datos = GestionAlmacenamiento.CargarDatos();
                    if (datos != null)
                    {
                        idEstudiante = datos.IdEstudiante;
                        nombreEstudiante = datos.NombreEstudiante;
                        correoUAMV = datos.CorreoUAMV;
                        carreraPrimera = datos.CarreraPrimera;
                        carreraSegunda = datos.CarreraSegunda;
                        facultadPrimera = datos.FacultadPrimera;
                        facultadSegunda = datos.FacultadSegunda;
                        numeroCIF = datos.NumeroCIF;
                        cantidadCarreras = datos.CantidadCarreras;
                        totalActividades = datos.TotalActividades;
                        detalleReporte = datos.DetalleReporte;
                        estadoRestriccion = datos.EstadoRestriccion;
                        restriccionTipo = datos.RestriccionTipo;
                        horasAcumuladas = datos.HorasAcumuladas;
                        partidosAcumulados = datos.PartidosAcumulados;
                        listaActividades = datos.Actividades ?? new List<Actividad>();

                        cargadoDeArchivo = true;
                        if (totalActividades > 0)
                        {
                            mostrarMenuPrimero = true;
                        }

                        Console.WriteLine($"Sesión cargada para el estudiante: {nombreEstudiante} (CIF: {numeroCIF})");
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }

            while (continuarProceso.ToLower() == "s")
            {
                if (totalActividades == 0 && !cargadoDeArchivo)
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

                    idEstudiante = Guid.NewGuid().ToString();
                    listaActividades.Clear();

                    estadoRestriccion = "libre";
                    restriccionTipo = "libre";
                    horasAcumuladas = 0;
                    partidosAcumulados = 0;
                    detalleReporte = "";

                    // Auto-guardado transparente inicial tras registrar el estudiante
                    DatosPrograma datosAutoGuardados = new DatosPrograma
                    {
                        IdEstudiante = idEstudiante,
                        NombreEstudiante = nombreEstudiante,
                        CorreoUAMV = correoUAMV,
                        CarreraPrimera = carreraPrimera,
                        CarreraSegunda = carreraSegunda,
                        FacultadPrimera = facultadPrimera,
                        FacultadSegunda = facultadSegunda,
                        NumeroCIF = numeroCIF,
                        CantidadCarreras = cantidadCarreras,
                        TotalActividades = totalActividades,
                        DetalleReporte = detalleReporte,
                        EstadoRestriccion = estadoRestriccion,
                        RestriccionTipo = restriccionTipo,
                        HorasAcumuladas = horasAcumuladas,
                        PartidosAcumulados = partidosAcumulados,
                        Actividades = listaActividades
                    };
                    GestionAlmacenamiento.GuardarDatos(datosAutoGuardados);
                }
                cargadoDeArchivo = false;

                if (!mostrarMenuPrimero)
                {
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

                    Actividad nuevaActividad = new Actividad
                    {
                        Id = $"ACT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                        Nombre = nombreActividad,
                        Organizador = organizadorFinal,
                        TipoConvalidacion = tipoDeConvalidacion,
                        CantidadConvalidada = cantidadConvalidada,
                        Fecha = fechaRealizada
                    };
                    listaActividades.Add(nuevaActividad);

                    totalActividades++;
                    detalleReporte += $"ID: {nuevaActividad.Id} | Actividad: {nombreActividad} | Org: {organizadorFinal} | Tipo: {tipoDeConvalidacion} | Cant: {cantidadConvalidada} | Fecha: {fechaRealizada}\n";

                    // Auto-guardado transparente tras registrar cada actividad
                    DatosPrograma datosAutoGuardadosAct = new DatosPrograma
                    {
                        IdEstudiante = idEstudiante,
                        NombreEstudiante = nombreEstudiante,
                        CorreoUAMV = correoUAMV,
                        CarreraPrimera = carreraPrimera,
                        CarreraSegunda = carreraSegunda,
                        FacultadPrimera = facultadPrimera,
                        FacultadSegunda = facultadSegunda,
                        NumeroCIF = numeroCIF,
                        CantidadCarreras = cantidadCarreras,
                        TotalActividades = totalActividades,
                        DetalleReporte = detalleReporte,
                        EstadoRestriccion = estadoRestriccion,
                        RestriccionTipo = restriccionTipo,
                        HorasAcumuladas = horasAcumuladas,
                        PartidosAcumulados = partidosAcumulados,
                        Actividades = listaActividades
                    };
                    GestionAlmacenamiento.GuardarDatos(datosAutoGuardadosAct);
                }
                
                mostrarMenuPrimero = false;

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" REGISTRO COMPLETADO. ¿QUÉ DESEA HACER AHORA?");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Añadir otra actividad al estudiante actual");
                Console.WriteLine("2. Generar reporte PDF y finalizar");
                Console.WriteLine("3. Generar reporte e iniciar nuevo estudiante");
                Console.WriteLine("4. Salir sin guardar");
                Console.WriteLine("5. Guardar datos en archivo JSON");
                Console.WriteLine("6. Cargar datos desde archivo JSON");
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
                    case "5":
                        DatosPrograma datosAGuardar = new DatosPrograma
                        {
                            IdEstudiante = idEstudiante,
                            NombreEstudiante = nombreEstudiante,
                            CorreoUAMV = correoUAMV,
                            CarreraPrimera = carreraPrimera,
                            CarreraSegunda = carreraSegunda,
                            FacultadPrimera = facultadPrimera,
                            FacultadSegunda = facultadSegunda,
                            NumeroCIF = numeroCIF,
                            CantidadCarreras = cantidadCarreras,
                            TotalActividades = totalActividades,
                            DetalleReporte = detalleReporte,
                            EstadoRestriccion = estadoRestriccion,
                            RestriccionTipo = restriccionTipo,
                            HorasAcumuladas = horasAcumuladas,
                            PartidosAcumulados = partidosAcumulados,
                            Actividades = listaActividades
                        };
                        GestionAlmacenamiento.GuardarDatos(datosAGuardar);
                        mostrarMenuPrimero = true;
                        continuarProceso = "s";
                        break;
                    case "6":
                        var datosCargados = GestionAlmacenamiento.CargarDatos();
                        if (datosCargados != null)
                        {
                            idEstudiante = datosCargados.IdEstudiante;
                            nombreEstudiante = datosCargados.NombreEstudiante;
                            correoUAMV = datosCargados.CorreoUAMV;
                            carreraPrimera = datosCargados.CarreraPrimera;
                            carreraSegunda = datosCargados.CarreraSegunda;
                            facultadPrimera = datosCargados.FacultadPrimera;
                            facultadSegunda = datosCargados.FacultadSegunda;
                            numeroCIF = datosCargados.NumeroCIF;
                            cantidadCarreras = datosCargados.CantidadCarreras;
                            totalActividades = datosCargados.TotalActividades;
                            detalleReporte = datosCargados.DetalleReporte;
                            estadoRestriccion = datosCargados.EstadoRestriccion;
                            restriccionTipo = datosCargados.RestriccionTipo;
                            horasAcumuladas = datosCargados.HorasAcumuladas;
                            partidosAcumulados = datosCargados.PartidosAcumulados;
                            listaActividades = datosCargados.Actividades ?? new List<Actividad>();

                            cargadoDeArchivo = true;
                            if (totalActividades > 0)
                            {
                                mostrarMenuPrimero = true;
                            }
                            Console.WriteLine($"Datos cargados con éxito para: {nombreEstudiante}");
                        }
                        continuarProceso = "s";
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
