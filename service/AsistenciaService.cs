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
        Aula? aula = aulas.Listar().Find(x => x.id == aulaId);
        if (aula?.anio != fecha.Year)
        {
            return new List<Matricula>();
        }

        return matriculas.Listar().FindAll(x => x.aulaId == aulaId && x.VigenteEn(fecha));
    }

    public void Guardar(int aulaId, DateTime fecha, List<Asistencia> cambios, List<Asistencia> bajas)
    {
        ValidarFecha(fecha);

        List<Asistencia> filas = new List<Asistencia>(cambios);
        filas.AddRange(bajas);
        List<Matricula> disponibles = MatriculasDelDia(aulaId, fecha);
        if (filas.Any(x => !disponibles.Exists(matricula => matricula.id == x.matriculaId)) ||
            filas.GroupBy(x => x.matriculaId).Any(grupo => grupo.Count() > 1))
        {
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

        List<Asistencia> eliminaciones = bajas.Select(baja => new Asistencia
        {
            matriculaId = baja.matriculaId,
            fecha = fecha.Date
        }).ToList();
        repositorio.Guardar(registros, eliminaciones);
    }

    internal static void ValidarRegistros(List<Asistencia> asistencias, List<Matricula> matriculas)
    {
        if (asistencias.GroupBy(x => (x.matriculaId, x.fecha.Date)).Any(g => g.Count() > 1))
        {
            throw new ArgumentException("Hay asistencias repetidas para una matrícula y fecha.");
        }

        foreach (Asistencia asistencia in asistencias)
        {
            ValidarMarca(asistencia.estado, asistencia.observacion);
            ValidarFecha(asistencia.fecha);
            Matricula matricula = matriculas.Find(x => x.id == asistencia.matriculaId)
                ?? throw new ArgumentException("Una asistencia referencia una matrícula inexistente.");
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
