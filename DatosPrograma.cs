using System.Collections.Generic;

namespace NetGestionConvalidacionBecasUni
{
    public class DatosPrograma
    {
        public string IdEstudiante { get; set; } = "";
        public string NombreEstudiante { get; set; } = "";
        public string CorreoUAMV { get; set; } = "";
        public string CarreraPrimera { get; set; } = "";
        public string CarreraSegunda { get; set; } = "";
        public string FacultadPrimera { get; set; } = "";
        public string FacultadSegunda { get; set; } = "";
        public int NumeroCIF { get; set; }
        public int CantidadCarreras { get; set; }
        public int TotalActividades { get; set; }
        public string DetalleReporte { get; set; } = "";
        public string EstadoRestriccion { get; set; } = "libre";
        public string RestriccionTipo { get; set; } = "libre";
        public int HorasAcumuladas { get; set; }
        public int PartidosAcumulados { get; set; }
        public List<Actividad> Actividades { get; set; } = new List<Actividad>();
    }
}
