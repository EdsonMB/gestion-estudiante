using System.Globalization;
using GestionEstudiante.util;
namespace GestionEstudiante.model;

public class Matricula : ModeloBase
{
    public int estudianteId { get; set; }
    public int aulaId { get; set; }
    public DateTime fecha { get; set; }
    public DateTime? fechaCancelacion { get; set; }
    public bool Activa => !fechaCancelacion.HasValue;
    public string Codigo => "M" + id.ToString("000000", CultureInfo.InvariantCulture);
    public bool VigenteEn(DateTime dia) => fecha.Date <= dia.Date
        && (!fechaCancelacion.HasValue || dia.Date <= fechaCancelacion.Value.Date);
    public override string[] ObtenerCampos() => new[]
    {
        id.ToString(CultureInfo.InvariantCulture), estudianteId.ToString(CultureInfo.InvariantCulture),
        aulaId.ToString(CultureInfo.InvariantCulture), fecha.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture),
        fechaCancelacion?.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture) ?? ""
    };
}
