using System.Globalization;
namespace GestionEstudiante.model;

public class Aula : ModeloBase
{
    public string nivel { get; set; } = "Primaria";
    public int grado { get; set; } = 1;
    public string seccion { get; set; } = "A";
    public int anio { get; set; }
    public int capacidad { get; set; } = 30;
    public bool activo { get; set; } = true;
    public string Descripcion => nivel == "Inicial" ? $"{nivel} · {grado} años {seccion} ({anio})" : $"{nivel} · {grado}.º {seccion} ({anio})";
    public override string[] ObtenerCampos() => new[]
    {
        id.ToString(CultureInfo.InvariantCulture), nivel, grado.ToString(CultureInfo.InvariantCulture),
        seccion, anio.ToString(CultureInfo.InvariantCulture), capacidad.ToString(CultureInfo.InvariantCulture),
        activo.ToString().ToLowerInvariant()
    };
}
