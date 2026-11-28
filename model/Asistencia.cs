using System.Globalization;
using GestionEstudiante.util;
namespace GestionEstudiante.model;

public enum EstadoAsistencia { Presente, Falta, Tardanza }

public class Asistencia : ModeloBase
{
    public int matriculaId { get; set; }
    public string codigoEstudiante { get; set; } = "";
    public DateTime fecha { get; set; }
    public EstadoAsistencia estado { get; set; }
    public string observacion { get; set; } = "";
    public override string[] ObtenerCampos()
    {
        string identificador = id.ToString(CultureInfo.InvariantCulture);
        if (matriculaId == 0)
        {
            return new[] { identificador, codigoEstudiante,
                fecha.ToString(Constantes.FormatoFechaAsistencia, CultureInfo.InvariantCulture), estado.ToString() };
        }

        return new[] { identificador, matriculaId.ToString(CultureInfo.InvariantCulture),
            fecha.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture), estado.ToString(), observacion };
    }
}
