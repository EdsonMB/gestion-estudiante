namespace GestionEstudiante.model;

// Datos activos de la aplicación.
public class BaseDatosEscolar
{
    public List<Aula> aulas { get; set; } = new();
    public List<Matricula> matriculas { get; set; } = new();
    public List<Asistencia> asistencias { get; set; } = new();
    public List<Estudiante> estudiantes { get; set; }

    public BaseDatosEscolar()
    {
        estudiantes = new List<Estudiante>();
    }
}
