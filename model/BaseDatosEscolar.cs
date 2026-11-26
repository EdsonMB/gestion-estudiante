namespace GestionEstudiante.model;

// Datos activos de la aplicación.
public class BaseDatosEscolar
{
    public List<Estudiante> estudiantes { get; set; }
    public List<Asistencia> asistencias { get; set; }

    public BaseDatosEscolar()
    {
        estudiantes = new List<Estudiante>();
        asistencias = new List<Asistencia>();
    }
}
