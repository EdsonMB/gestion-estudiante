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

    public List<Matricula> Listar()
    {
        return repositorio.Listar();
    }

    public void Registrar(int estudianteId, int aulaId, DateTime fecha)
    {
        Estudiante? estudiante = BuscarEstudiante(estudiantes.Listar(), estudianteId);
        if (estudiante == null || !estudiante.activo)
            throw new InvalidOperationException("Seleccione un estudiante activo.");
        Aula? aula = BuscarAula(aulas.Listar(), aulaId);
        if (aula == null || !aula.activo)
            throw new InvalidOperationException("Seleccione un aula activa.");
        Matricula nueva = new Matricula { estudianteId = estudiante.id, aulaId = aula.id, fecha = fecha.Date };
        ValidarFechas(nueva, aula.anio);
        int ocupados = 0;
        bool coincideConHistorial = false;
        foreach (Matricula matricula in Listar())
        {
            if (matricula.aulaId == aulaId && matricula.Activa)
            {
                ocupados++;
            }
            if (matricula.estudianteId != estudianteId || matricula.fecha.Year != aula.anio)
            {
                continue;
            }
            if (matricula.Activa)
            {
                throw new InvalidOperationException("El estudiante ya tiene una matrícula activa en ese año.");
            }
            if (matricula.fechaCancelacion?.Date >= nueva.fecha)
            {
                coincideConHistorial = true;
            }
        }
        if (coincideConHistorial)
            throw new InvalidOperationException("La nueva matrícula debe comenzar después del día de cancelación, porque ese día aún pertenece a la matrícula anterior.");
        if (ocupados >= aula.capacidad)
            throw new InvalidOperationException("El aula no tiene cupos disponibles.");
        repositorio.Guardar(nueva);
    }
    public void Cancelar(int id)
    {
        foreach (Matricula anterior in Listar())
        {
            if (anterior.id == id)
            {
                if (!anterior.Activa) throw new InvalidOperationException("La matrícula ya está cancelada.");
                repositorio.Guardar(new Matricula { id = id, estudianteId = anterior.estudianteId,
                    aulaId = anterior.aulaId, fecha = anterior.fecha, fechaCancelacion = DateTime.Today });
                return;
            }
        }
        throw new InvalidOperationException("Matrícula no encontrada.");
    }

    internal static void ValidarRegistros(List<Matricula> matriculas, List<Estudiante> estudiantes, List<Aula> aulas)
    {
        Dictionary<(int Estudiante, int Anio), List<Matricula>> historiales = new();
        foreach (Matricula matricula in matriculas)
        {
            Estudiante estudiante = BuscarEstudiante(estudiantes, matricula.estudianteId)
                ?? throw new ArgumentException("Una matrícula referencia un estudiante inexistente.");
            Aula aula = BuscarAula(aulas, matricula.aulaId)
                ?? throw new ArgumentException("Una matrícula referencia un aula inexistente.");

            ValidarFechas(matricula, aula.anio);
            if (matricula.Activa && (!estudiante.activo || !aula.activo))
            {
                throw new ArgumentException("Una matrícula activa requiere estudiante y aula activos.");
            }
            var clave = (matricula.estudianteId, matricula.fecha.Year);
            if (!historiales.TryGetValue(clave, out List<Matricula>? historial))
            {
                historial = new List<Matricula>();
                historiales.Add(clave, historial);
            }
            historial.Add(matricula);
        }

        foreach (List<Matricula> historial in historiales.Values)
        {
            int activas = 0;
            foreach (Matricula matricula in historial)
            {
                if (matricula.Activa) activas++;
            }
            if (activas > 1)
            {
                throw new ArgumentException("Un estudiante tiene más de una matrícula activa en el mismo año.");
            }

            historial.Sort(CompararFechas);
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

    private static int CompararFechas(Matricula primera, Matricula segunda)
    {
        return primera.fecha.CompareTo(segunda.fecha);
    }

    private static Estudiante? BuscarEstudiante(List<Estudiante> estudiantes, int id)
    {
        foreach (Estudiante estudiante in estudiantes)
        {
            if (estudiante.id == id) return estudiante;
        }
        return null;
    }

    private static Aula? BuscarAula(List<Aula> aulas, int id)
    {
        foreach (Aula aula in aulas)
        {
            if (aula.id == id) return aula;
        }
        return null;
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
