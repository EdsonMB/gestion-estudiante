namespace GestionEstudiante.model;

// Datos activos de la aplicación.
public class BaseDatosEscolar
{
    public List<Estudiante> estudiantes { get; set; }

    public BaseDatosEscolar()
    {
        estudiantes = new List<Estudiante>();
    }
}
