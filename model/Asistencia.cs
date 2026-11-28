using System.Globalization;
using GestionEstudiante.util;

namespace GestionEstudiante.model;

public class Asistencia : ModeloBase
{
    public int matriculaId { get; set; }
    public DateTime fecha { get; set; }

    public string estado { get; set; }
    public string observacion { get; set; }

    public Asistencia()
    {
        id = 0;
        matriculaId = 0;
        fecha = DateTime.MinValue;
        estado = Constantes.AsistenciaSinRegistro;
        observacion = "";
    }

    public static bool EsEstadoValido(string? estado)
    {
        return estado is Constantes.AsistenciaPresente or Constantes.AsistenciaFalta or Constantes.AsistenciaTardanza;
    }

    public override string[] ObtenerCampos()
    {
        return new string[]
        {
            id.ToString(CultureInfo.InvariantCulture),
            matriculaId.ToString(CultureInfo.InvariantCulture),
            fecha.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture),
            estado,
            observacion
        };
    }
}
