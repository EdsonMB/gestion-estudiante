using System.Globalization;
using GestionEstudiante.util;

namespace GestionEstudiante.model;

// Datos personales del estudiante y contacto de su apoderado.
public class Estudiante : ModeloBase
{
    // Identificación asignada por el repositorio.
    public string codigo { get; set; }

    // Datos personales.
    public string nombre { get; set; }
    public string apellidos { get; set; }

    // Contacto del apoderado; solo el correo es opcional.
    public string apoderado { get; set; }
    public string telefono { get; set; }
    public string correo { get; set; }

    // La baja cambia el estado; la fecha de registro se conserva al editar.
    public bool activo { get; set; }
    public DateTime fechaCreacion { get; set; }

    public Estudiante()
    {
        id = 0;
        codigo = "";
        nombre = "";
        apellidos = "";
        apoderado = "";
        telefono = "";
        correo = "";
        activo = true;
        fechaCreacion = DateTime.MinValue;
    }

    public override string[] ObtenerCampos()
    {
        return new string[]
       {
            id.ToString(CultureInfo.InvariantCulture),
            codigo,
            nombre,
            apellidos,
            apoderado,
            telefono,
            correo,
            fechaCreacion.ToString(Constantes.FormatoFecha, CultureInfo.InvariantCulture),
            activo ? "true" : "false"
       };
    }
}
