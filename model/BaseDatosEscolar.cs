namespace GestionEstudiante.model;

// Contenido del archivo JSON: próximo identificador y estudiantes registrados.
public class BaseDatosEscolar
{
    public Secuencias secuencias { get; set; }
    public Estudiante[] estudiantes { get; set; }

    public BaseDatosEscolar()
    {
        secuencias = new Secuencias();
        estudiantes = new Estudiante[0];
    }
}
