using System.Globalization;
using System.Text;
using GestionEstudiante.util;
using GestionEstudiante.model;
namespace GestionEstudiante.service;

public record FiltroReporte(int Anio, int AulaId, DateTime Desde, DateTime Hasta);
public record ResultadoReporte(string Titulo, string[] Columnas, List<string[]> Filas);

// Abstracción y polimorfismo: el formulario usa Reporte sin conocer cómo calcula cada variante.
public abstract class Reporte
{
    public abstract ResultadoReporte Generar(FiltroReporte filtro);

    public void Exportar(string ruta, ResultadoReporte resultado)
    {
        if (!Path.GetExtension(ruta).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("El reporte debe guardarse en un archivo .txt.");
        string texto = string.Join(Environment.NewLine, new[] { resultado.Columnas }.Concat(resultado.Filas)
            .Select(fila => FormatoTxt.CrearFila(fila)));
        string temporal = ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporal, texto, new UTF8Encoding(false));
            File.Move(temporal, ruta, true);
        }
        finally { File.Delete(temporal); }
    }

}

public class ReporteOcupacion : Reporte
{
    private readonly AulaService aulas;

    public ReporteOcupacion(AulaService aulas)
    {
        this.aulas = aulas;
    }

    public override ResultadoReporte Generar(FiltroReporte filtro)
    {
        List<string[]> filas = new();
        foreach (Aula aula in aulas.Listar().Where(x => x.anio == filtro.Anio &&
            (filtro.AulaId == 0 || x.id == filtro.AulaId)))
        {
            int ocupados = aulas.Ocupados(aula.id);
            filas.Add(new[] { aula.Descripcion, aula.capacidad.ToString(), ocupados.ToString(),
                (aula.activo ? aula.capacidad - ocupados : 0).ToString(), aula.activo ? "Activa" : "Inactiva" });
        }
        return new ResultadoReporte(Constantes.ReporteOcupacion, new[] { "Aula", "Capacidad", "Matriculados", "Cupos disponibles", "Estado" }, filas);
    }
}

public class ReporteAsistencia : Reporte
{
    private readonly EstudianteService estudiantes;
    private readonly MatriculaService matriculas;
    private readonly AsistenciaService asistencias;

    public ReporteAsistencia(EstudianteService estudiantes, MatriculaService matriculas, AsistenciaService asistencias)
    {
        this.estudiantes = estudiantes;
        this.matriculas = matriculas;
        this.asistencias = asistencias;
    }

    public override ResultadoReporte Generar(FiltroReporte filtro)
    {
        if (filtro.Desde.Date > filtro.Hasta.Date) throw new ArgumentException("La fecha inicial no puede ser posterior a la final.");
        DateTime inicioAnio = new DateTime(filtro.Anio, 1, 1);
        DateTime finAnio = new DateTime(filtro.Anio, 12, 31);
        DateTime desde = filtro.Desde.Date < inicioAnio ? inicioAnio : filtro.Desde.Date;
        DateTime hasta = filtro.Hasta.Date > finAnio ? finAnio : filtro.Hasta.Date;
        string[] columnas = { "Código", "Estudiante", "Presentes", "Faltas", "Tardanzas", "Registros", "% asistencia" };
        if (desde > hasta) return new ResultadoReporte(Constantes.ReporteAsistencia, columnas, new());
        var matriculasDelPeriodo = matriculas.Listar().Where(x => x.fecha.Year == filtro.Anio &&
            (filtro.AulaId == 0 || x.aulaId == filtro.AulaId) && x.fecha.Date <= hasta &&
            (!x.fechaCancelacion.HasValue || x.fechaCancelacion.Value.Date >= desde)).ToList();
        List<string[]> filas = new();
        List<Estudiante> estudiantesDelSistema = estudiantes.Listar();
        List<Asistencia> asistenciasDelSistema = asistencias.Listar();
        foreach (var grupo in matriculasDelPeriodo.GroupBy(x => x.estudianteId))
        {
            List<Asistencia> registros = asistenciasDelSistema.Where(x => grupo.Any(m => m.id == x.matriculaId) &&
                x.fecha.Date >= desde && x.fecha.Date <= hasta).ToList();
            int presentes = registros.Count(x => x.estado == Constantes.AsistenciaPresente);
            int faltas = registros.Count(x => x.estado == Constantes.AsistenciaFalta);
            int tardanzas = registros.Count(x => x.estado == Constantes.AsistenciaTardanza);
            string porcentaje = registros.Count == 0 ? "Sin registros" :
                (100m * (presentes + tardanzas) / registros.Count).ToString("0.0", CultureInfo.InvariantCulture) + "%";
            Estudiante estudiante = estudiantesDelSistema.Single(x => x.id == grupo.Key);
            filas.Add(new[] { estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos,
                presentes.ToString(), faltas.ToString(), tardanzas.ToString(), registros.Count.ToString(), porcentaje });
        }
        return new ResultadoReporte(Constantes.ReporteAsistencia, columnas, filas);
    }
}
