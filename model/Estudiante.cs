namespace GestionEstudiante.model;

// Agrupa los datos de un estudiante; el formulario permite ingresarlos y consultarlos.
public class Estudiante
{
    // El repositorio asigna el identificador numérico y el código visible, por ejemplo E000001.
    public int id { get; set; }
    public string codigo { get; set; } = "";
    public string nombre { get; set; } = "";
    public string carrera { get; set; } = "";
    // Correo y teléfono pueden quedar vacíos porque son datos opcionales.
    public string correo { get; set; } = "";
    public string telefono { get; set; } = "";
    // Se asigna al registrar y se conserva cuando se modifican los demás datos.
    public DateTime fechaCreacion { get; internal set; }
}
