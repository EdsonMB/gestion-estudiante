using GestionEstudiante.model;
using GestionEstudiante.repository;

namespace GestionEstudiante.service;

public class AsistenciaService
{
    private readonly AsistenciaRepository repositorio;
    private readonly AulaRepository aulas;
    private readonly MatriculaRepository matriculas;

    public AsistenciaService(AsistenciaRepository repositorio, AulaRepository aulas, MatriculaRepository matriculas)
    {
        this.repositorio = repositorio;
        this.aulas = aulas;
        this.matriculas = matriculas;
    }

    public List<Asistencia> Listar()
    {
        return repositorio.Listar();
    }

    public List<Matricula> MatriculasDelDia(int aulaId, DateTime fecha)
    {
        bool aulaDelAnio = false;
        foreach (Aula aula in aulas.Listar())
        {
            if (aula.id == aulaId && aula.anio == fecha.Year)
            {
                aulaDelAnio = true;
                break;
            }
        }

        List<Matricula> disponibles = new List<Matricula>();
        if (aulaDelAnio)
        {
            foreach (Matricula matricula in matriculas.Listar())
            {
                if (matricula.aulaId == aulaId && matricula.VigenteEn(fecha))
                    disponibles.Add(matricula);
            }
        }
        return disponibles;
    }

    public void Guardar(int aulaId, DateTime fecha, List<Asistencia> cambios, List<Asistencia> bajas)
    {
        ValidarFecha(fecha);

        List<Asistencia> filas = new List<Asistencia>(cambios);
        filas.AddRange(bajas);
        HashSet<int> disponibles = new HashSet<int>();
        foreach (Matricula matricula in MatriculasDelDia(aulaId, fecha))
        {
            disponibles.Add(matricula.id);
        }
        foreach (Asistencia fila in filas)
        {
            if (!disponibles.Remove(fila.matriculaId))
                throw new ArgumentException("La lista de asistencia no corresponde al aula y fecha seleccionadas.");
        }

        List<Asistencia> registros = new List<Asistencia>();
        foreach (Asistencia cambio in cambios)
        {
            string observacion = cambio.observacion.Trim();
            ValidarMarca(cambio.estado, observacion);

            registros.Add(new Asistencia
            {
                matriculaId = cambio.matriculaId,
                fecha = fecha.Date,
                estado = cambio.estado,
                observacion = observacion
            });
        }

        List<Asistencia> eliminaciones = new List<Asistencia>();
        foreach (Asistencia baja in bajas)
        {
            eliminaciones.Add(new Asistencia { matriculaId = baja.matriculaId, fecha = fecha.Date });
        }
        repositorio.Guardar(registros, eliminaciones);
    }

    internal static void ValidarRegistros(List<Asistencia> asistencias, List<Matricula> matriculas)
    {
        HashSet<(int Matricula, DateTime Fecha)> claves = new();
        Dictionary<int, Matricula> matriculasPorId = new Dictionary<int, Matricula>();
        foreach (Matricula matricula in matriculas)
        {
            matriculasPorId.Add(matricula.id, matricula);
        }

        foreach (Asistencia asistencia in asistencias)
        {
            if (!claves.Add((asistencia.matriculaId, asistencia.fecha.Date)))
                throw new ArgumentException("Hay asistencias repetidas para una matrícula y fecha.");
            ValidarMarca(asistencia.estado, asistencia.observacion);
            ValidarFecha(asistencia.fecha);
            if (!matriculasPorId.TryGetValue(asistencia.matriculaId, out Matricula? matricula))
                throw new ArgumentException("Una asistencia referencia una matrícula inexistente.");
            if (!matricula.VigenteEn(asistencia.fecha))
            {
                throw new ArgumentException("La fecha de asistencia debe estar dentro de la vigencia de la matrícula.");
            }
        }
    }

    private static void ValidarFecha(DateTime fecha)
    {
        if (fecha.Date > DateTime.Today)
        {
            throw new ArgumentException("No se puede registrar asistencia futura.");
        }
    }

    private static void ValidarMarca(string estado, string observacion)
    {
        if (!Asistencia.EsEstadoValido(estado) || observacion.Length > 250)
        {
            throw new ArgumentException("Seleccione un estado válido y una observación de hasta 250 caracteres.");
        }
    }
}
