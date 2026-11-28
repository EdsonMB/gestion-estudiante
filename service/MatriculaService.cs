using GestionEstudiante.model;
using GestionEstudiante.repository;
namespace GestionEstudiante.service;

public class MatriculaService
{
    private readonly MatriculaRepository repositorio;
    private readonly EstudianteRepository estudiantes;
    private readonly AulaRepository aulas;

    public MatriculaService(MatriculaRepository repositorio, EstudianteRepository estudiantes, AulaRepository aulas)
    {
        this.repositorio = repositorio;
        this.estudiantes = estudiantes;
        this.aulas = aulas;
    }

    public List<Matricula> Listar() => repositorio.Listar();
    public void Registrar(int estudianteId, int aulaId, DateTime fecha)
    {
        Estudiante estudiante = estudiantes.Listar().SingleOrDefault(x => x.id == estudianteId && x.activo)
            ?? throw new InvalidOperationException("Seleccione un estudiante activo.");
        Aula aula = aulas.Listar().SingleOrDefault(x => x.id == aulaId && x.activo)
            ?? throw new InvalidOperationException("Seleccione un aula activa.");
        Matricula nueva = new Matricula { estudianteId = estudiante.id, aulaId = aula.id, fecha = fecha.Date };
        ValidarFechas(nueva, aula.anio);
        List<Matricula> matriculas = Listar();
        if (matriculas.Any(x => x.estudianteId == estudianteId && x.Activa && x.fecha.Year == aula.anio))
            throw new InvalidOperationException("El estudiante ya tiene una matrícula activa en ese año.");
        if (matriculas.Any(x => x.estudianteId == estudianteId && !x.Activa &&
            x.fecha.Year == aula.anio && x.fechaCancelacion!.Value.Date >= fecha.Date))
            throw new InvalidOperationException("La nueva matrícula debe comenzar después del día de cancelación, porque ese día aún pertenece a la matrícula anterior.");
        if (matriculas.Count(x => x.aulaId == aulaId && x.Activa) >= aula.capacidad)
            throw new InvalidOperationException("El aula no tiene cupos disponibles.");
        repositorio.Guardar(nueva);
    }
    public void Cancelar(int id)
    {
        Matricula anterior = Listar().Single(x => x.id == id);
        if (!anterior.Activa) throw new InvalidOperationException("La matrícula ya está cancelada.");
        repositorio.Guardar(new Matricula { id = id, estudianteId = anterior.estudianteId,
            aulaId = anterior.aulaId, fecha = anterior.fecha, fechaCancelacion = DateTime.Today });
    }

    internal static void ValidarRegistros(List<Matricula> matriculas, List<Estudiante> estudiantes, List<Aula> aulas)
    {
        foreach (Matricula matricula in matriculas)
        {
            Estudiante estudiante = estudiantes.Find(x => x.id == matricula.estudianteId)
                ?? throw new ArgumentException("Una matrícula referencia un estudiante inexistente.");
            Aula aula = aulas.Find(x => x.id == matricula.aulaId)
                ?? throw new ArgumentException("Una matrícula referencia un aula inexistente.");

            ValidarFechas(matricula, aula.anio);
            if (matricula.Activa && (!estudiante.activo || !aula.activo))
            {
                throw new ArgumentException("Una matrícula activa requiere estudiante y aula activos.");
            }
        }

        foreach (var grupo in matriculas.GroupBy(x => (x.estudianteId, x.fecha.Year)))
        {
            if (grupo.Count(x => x.Activa) > 1)
            {
                throw new ArgumentException("Un estudiante tiene más de una matrícula activa en el mismo año.");
            }

            List<Matricula> historial = grupo.OrderBy(x => x.fecha).ToList();
            for (int i = 1; i < historial.Count; i++)
            {
                Matricula anterior = historial[i - 1];
                DateTime fin = anterior.fechaCancelacion?.Date ?? new DateTime(anterior.fecha.Year, 12, 31);
                if (historial[i].fecha.Date <= fin)
                {
                    throw new ArgumentException("Las matrículas de un estudiante no pueden compartir días del mismo año escolar.");
                }
            }
        }
    }

    private static void ValidarFechas(Matricula matricula, int anioAula)
    {
        if (matricula.fecha.Year != anioAula || matricula.fecha.Date > DateTime.Today)
        {
            throw new ArgumentException("La fecha debe pertenecer al año del aula y no puede ser futura.");
        }

        if (matricula.fechaCancelacion?.Date < matricula.fecha.Date || matricula.fechaCancelacion?.Date > DateTime.Today)
        {
            throw new ArgumentException("La fecha de cancelación debe estar entre el inicio de la matrícula y hoy.");
        }
    }
}
