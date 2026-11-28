using System.Globalization;
using GestionEstudiante.util;

namespace GestionEstudiante.model;

public class Aula : ModeloBase
{
    public string nivel { get; set; }
    public int grado { get; set; }
    public string seccion { get; set; }
    public int anio { get; set; }

    public int capacidad { get; set; }
    public bool activo { get; set; }

    public string Descripcion => nivel == Constantes.NivelInicial
        ? $"{nivel} · {grado} años {seccion} ({anio})"
        : $"{nivel} · {grado}.º {seccion} ({anio})";

    public Aula()
    {
        id = 0;
        nivel = Constantes.NivelPrimaria;
        grado = 1;
        seccion = "A";
        anio = 0;
        capacidad = 30;
        activo = true;
    }

    public override string[] ObtenerCampos()
    {
        return new string[]
        {
            id.ToString(CultureInfo.InvariantCulture),
            nivel,
            grado.ToString(CultureInfo.InvariantCulture),
            seccion,
            anio.ToString(CultureInfo.InvariantCulture),
            capacidad.ToString(CultureInfo.InvariantCulture),
            activo ? "true" : "false"
        };
    }
}
