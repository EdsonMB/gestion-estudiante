using System.Globalization;
using GestionEstudiante.util;
namespace GestionEstudiante.model;

public enum EstadoAsistencia { Presente, Falta, Tardanza }

public class Asistencia : ModeloBase
{
    public int matriculaId { get; set; }
    public DateTime fecha { get; set; }
    public EstadoAsistencia estado { get; set; }
    public string observacion { get; set; } = "";
    public override string[] ObtenerCampos() => new[]
    {
        id.ToString(CultureInfo.InvariantCulture), matriculaId.ToString(CultureInfo.InvariantCulture),
        fecha.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture), estado.ToString(), observacion
    };
}
