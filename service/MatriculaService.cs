using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;
namespace GestionEstudiante.service;

public class MatriculaService
{
    private readonly GestorBaseDatos gestor;
    private readonly Repositorio<Matricula> repositorio;
    public MatriculaService(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
        repositorio = new Repositorio<Matricula>(gestor, Constantes.ArchivoMatriculas,
            d => d.matriculas, (d, lista) => d.matriculas = lista);
    }
    public IReadOnlyList<Matricula> Listar() => repositorio.Listar();
    public void Registrar(int estudianteId, int aulaId, DateTime fecha)
    {
        Estudiante estudiante = gestor.Datos.estudiantes.SingleOrDefault(x => x.id == estudianteId && x.activo)
            ?? throw new InvalidOperationException("Seleccione un estudiante activo.");
        Aula aula = gestor.Datos.aulas.SingleOrDefault(x => x.id == aulaId && x.activo)
            ?? throw new InvalidOperationException("Seleccione un aula activa.");
        if (fecha.Year != aula.anio || fecha.Date > DateTime.Today)
            throw new ArgumentException("La fecha debe pertenecer al año del aula y no puede ser futura.");
        if (Listar().Any(x => x.estudianteId == estudianteId && x.Activa &&
            gestor.Datos.aulas.Single(a => a.id == x.aulaId).anio == aula.anio))
            throw new InvalidOperationException("El estudiante ya tiene una matrícula activa en ese año.");
        if (Listar().Any(x => x.estudianteId == estudianteId && !x.Activa &&
            gestor.Datos.aulas.Single(a => a.id == x.aulaId).anio == aula.anio && x.fechaCancelacion!.Value.Date > fecha.Date))
            throw new InvalidOperationException("La fecha debe ser posterior o igual a la última cancelación de ese año.");
        if (Listar().Count(x => x.aulaId == aulaId && x.Activa) >= aula.capacidad)
            throw new InvalidOperationException("El aula no tiene cupos disponibles.");
        repositorio.Guardar(new Matricula { estudianteId = estudiante.id, aulaId = aula.id, fecha = fecha.Date });
    }
    public void Cancelar(int id)
    {
        Matricula anterior = Listar().Single(x => x.id == id);
        if (!anterior.Activa) throw new InvalidOperationException("La matrícula ya está cancelada.");
        repositorio.Guardar(new Matricula { id = id, estudianteId = anterior.estudianteId,
            aulaId = anterior.aulaId, fecha = anterior.fecha, fechaCancelacion = DateTime.Today });
    }
}
