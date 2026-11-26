using System.Globalization;
using GestionEstudiante.util;

namespace GestionEstudiante.model;

public class Asistencia : ModeloBase
{
    public const string SinRegistro = "Sin registro";
    public const string Presente = "Presente";
    public const string Falta = "Falta";
    public const string Tardanza = "Tardanza";

    public string codigoEstudiante { get; set; } = string.Empty;
    public DateTime fecha { get; set; }
    public string estado { get; set; } = SinRegistro;

    public static bool EsEstadoValido(string estado)
    {
        return estado is Presente or Falta or Tardanza;
    }

    public override string[] ObtenerCampos()
    {
        return new string[]
        {
            id.ToString(CultureInfo.InvariantCulture),
            codigoEstudiante,
            fecha.ToString(Constantes.FormatoFechaAsistencia, CultureInfo.InvariantCulture),
            estado
        };
    }
}
