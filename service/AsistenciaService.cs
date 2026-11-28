using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;
namespace GestionEstudiante.service;

public class AsistenciaService
{
    private readonly GestorBaseDatos gestor;
    private readonly Repositorio<Asistencia> repositorio;
    public AsistenciaService(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
        repositorio = new Repositorio<Asistencia>(gestor, Constantes.ArchivoAsistencias,
            d => d.asistencias, (d, lista) => d.asistencias = lista);
    }
    public List<Asistencia> Listar() => repositorio.Listar();
    public List<Matricula> MatriculasDelDia(int aulaId, DateTime fecha) => gestor.Datos.matriculas
        .Where(x => x.aulaId == aulaId && x.VigenteEn(fecha) &&
            gestor.Datos.aulas.Single(a => a.id == aulaId).anio == fecha.Year).ToList();

    public List<Estudiante> EstudiantesSinAula(DateTime fecha)
    {
        List<Asistencia> anteriores = MarcasSinAula(fecha);
        return gestor.Datos.estudiantes.Where(e => e.activo || BuscarPorCodigo(anteriores, e.codigo) != null).ToList();
    }

    public void GuardarDiaSinAula(DateTime fecha, List<Asistencia> registros)
    {
        if (fecha.Date > DateTime.Today) throw new ArgumentException("No se puede registrar asistencia futura.");

        List<Estudiante> estudiantes = EstudiantesSinAula(fecha);
        List<Asistencia> anteriores = MarcasSinAula(fecha);
        List<Asistencia> nuevos = Listar().Where(x => x.matriculaId != 0 || x.fecha.Date != fecha.Date ||
            !estudiantes.Any(e => e.codigo.Equals(x.codigoEstudiante, StringComparison.OrdinalIgnoreCase))).ToList();
        int siguiente = repositorio.SiguienteId();
        foreach (Asistencia registro in registros)
        {
            string codigo = registro.codigoEstudiante;
            if (!Enum.IsDefined(registro.estado) ||
                !estudiantes.Any(e => e.codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)) ||
                BuscarPorCodigo(nuevos.Where(x => x.matriculaId == 0 && x.fecha.Date == fecha.Date), codigo) != null)
                throw new ArgumentException("Seleccione un estado válido para cada estudiante, sin duplicados.");

            Asistencia? anterior = BuscarPorCodigo(anteriores, codigo);
            nuevos.Add(new Asistencia { id = anterior?.id ?? siguiente++, codigoEstudiante = anterior?.codigoEstudiante ?? codigo,
                fecha = fecha.Date, estado = registro.estado });
        }
        repositorio.Reemplazar(nuevos);
    }

    public List<Asistencia> MarcasSinAula(DateTime fecha) =>
        Listar().Where(x => x.matriculaId == 0 && x.fecha.Date == fecha.Date).ToList();

    private static Asistencia? BuscarPorCodigo(IEnumerable<Asistencia> registros, string codigo) =>
        registros.FirstOrDefault(x => x.codigoEstudiante.Equals(codigo, StringComparison.OrdinalIgnoreCase));

    public void GuardarDia(int aulaId, DateTime fecha, List<Asistencia> registros)
    {
        if (fecha.Date > DateTime.Today) throw new ArgumentException("No se puede registrar asistencia futura.");
        Aula aula = gestor.Datos.aulas.SingleOrDefault(x => x.id == aulaId)
            ?? throw new ArgumentException("Seleccione un aula existente.");
        if (fecha.Year != aula.anio) throw new ArgumentException("La fecha debe pertenecer al año del aula.");
        List<Matricula> matriculas = MatriculasDelDia(aulaId, fecha);
        if (registros.Any(x => !matriculas.Any(m => m.id == x.matriculaId)) ||
            registros.GroupBy(x => x.matriculaId).Any(g => g.Count() > 1))
            throw new ArgumentException("La lista de asistencia no corresponde al aula y fecha seleccionados.");
        List<Asistencia> existentes = Listar();
        List<Asistencia> nuevos = existentes.Where(x => x.fecha.Date != fecha.Date ||
            !matriculas.Any(m => m.id == x.matriculaId)).ToList();
        int siguiente = repositorio.SiguienteId();
        foreach (Asistencia registro in registros)
        {
            if (!Enum.IsDefined(registro.estado) || registro.observacion.Trim().Length > 250)
                throw new ArgumentException("Seleccione un estado válido y una observación de hasta 250 caracteres.");
            Asistencia? anterior = existentes.SingleOrDefault(x => x.matriculaId == registro.matriculaId && x.fecha.Date == fecha.Date);
            nuevos.Add(new Asistencia { id = anterior?.id ?? siguiente++, matriculaId = registro.matriculaId,
                fecha = fecha.Date, estado = registro.estado, observacion = registro.observacion.Trim() });
        }
        repositorio.Reemplazar(nuevos);
    }
}
