using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;
namespace GestionEstudiante.service;

// Comparte el gestor de la sesión; cada módulo aplica sus propias reglas de negocio.
public class EscolarService
{
    private readonly GestorBaseDatos gestor;
    public AulaService Aulas { get; }
    public MatriculaService Matriculas { get; }
    public AsistenciaService Asistencias { get; }
    public List<Estudiante> Estudiantes => gestor.Datos.estudiantes.ToList();

    public EscolarService(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
        Aulas = new AulaService(gestor);
        Matriculas = new MatriculaService(gestor);
        Asistencias = new AsistenciaService(gestor);
    }
    public string NombreEstudiante(int id)
    {
        Estudiante estudiante = gestor.Datos.estudiantes.Single(x => x.id == id);
        return estudiante.nombre + " " + estudiante.apellidos;
    }
}
