namespace GestionEstudiante.model;

// Conserva el siguiente identificador aunque se cierre la aplicación.
public class Secuencias
{
    public int siguienteIdEstudiante { get; set; }

    public Secuencias()
    {
        siguienteIdEstudiante = 1;
    }
}
