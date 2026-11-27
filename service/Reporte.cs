using System.Globalization;
using System.Text;
using System.Text.Json;
using GestionEstudiante.model;
namespace GestionEstudiante.service;

public record FiltroReporte(int Anio, int AulaId, DateTime Desde, DateTime Hasta);
public record ResultadoReporte(string Titulo, string[] Columnas, List<string[]> Filas);

// Abstracción y polimorfismo: el formulario usa Reporte sin conocer cómo calcula cada variante.
public abstract class Reporte
{
    protected readonly EscolarService servicio;
    protected Reporte(EscolarService servicio) { this.servicio = servicio; }
    public abstract ResultadoReporte Generar(FiltroReporte filtro);

    public void Exportar(string ruta, ResultadoReporte resultado, bool json)
    {
        string texto = json ? JsonSerializer.Serialize(resultado, new JsonSerializerOptions { WriteIndented = true })
            : string.Join(Environment.NewLine, new[] { resultado.Columnas }.Concat(resultado.Filas)
                .Select(fila => string.Join(",", fila.Select(EscaparCsv))));
        string temporal = ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporal, texto, new UTF8Encoding(true));
            File.Move(temporal, ruta, true);
        }
        finally { File.Delete(temporal); }
    }
    private static string EscaparCsv(string valor)
    {
        // Evita que un nombre introducido por el usuario se interprete como una fórmula.
        if (valor.Length > 0 && "=+-@\t\r\n".Contains(valor[0])) valor = "'" + valor;
        return "\"" + valor.Replace("\"", "\"\"") + "\"";
    }
}

public class ReporteOcupacion : Reporte
{
    public ReporteOcupacion(EscolarService servicio) : base(servicio) { }
    public override ResultadoReporte Generar(FiltroReporte filtro)
    {
        List<string[]> filas = new();
        foreach (Aula aula in servicio.Aulas.Listar().Where(x => x.anio == filtro.Anio &&
            (filtro.AulaId == 0 || x.id == filtro.AulaId)))
        {
            int ocupados = servicio.Aulas.Ocupados(aula.id);
            filas.Add(new[] { aula.Descripcion, aula.capacidad.ToString(), ocupados.ToString(),
                (aula.activo ? aula.capacidad - ocupados : 0).ToString(), aula.activo ? "Activa" : "Inactiva" });
        }
        return new ResultadoReporte("Ocupación de aulas", new[] { "Aula", "Capacidad", "Matriculados", "Cupos disponibles", "Estado" }, filas);
    }
}

public class ReporteAsistencia : Reporte
{
    public ReporteAsistencia(EscolarService servicio) : base(servicio) { }
    public override ResultadoReporte Generar(FiltroReporte filtro)
    {
        if (filtro.Desde.Date > filtro.Hasta.Date) throw new ArgumentException("La fecha inicial no puede ser posterior a la final.");
        var matriculas = servicio.Matriculas.Listar().Where(x => servicio.Aulas.Listar().Single(a => a.id == x.aulaId).anio == filtro.Anio &&
            (filtro.AulaId == 0 || x.aulaId == filtro.AulaId) && x.fecha.Date <= filtro.Hasta.Date &&
            (!x.fechaCancelacion.HasValue || x.fechaCancelacion.Value.Date >= filtro.Desde.Date)).ToList();
        List<string[]> filas = new();
        foreach (var grupo in matriculas.GroupBy(x => x.estudianteId))
        {
            List<Asistencia> registros = servicio.Asistencias.Listar().Where(x => grupo.Any(m => m.id == x.matriculaId) &&
                x.fecha.Date >= filtro.Desde.Date && x.fecha.Date <= filtro.Hasta.Date).ToList();
            int presentes = registros.Count(x => x.estado == EstadoAsistencia.Presente);
            int faltas = registros.Count(x => x.estado == EstadoAsistencia.Falta);
            int tardanzas = registros.Count(x => x.estado == EstadoAsistencia.Tardanza);
            string porcentaje = registros.Count == 0 ? "Sin registros" :
                (100m * (presentes + tardanzas) / registros.Count).ToString("0.0", CultureInfo.InvariantCulture) + "%";
            filas.Add(new[] { servicio.Estudiantes.Single(x => x.id == grupo.Key).codigo, servicio.NombreEstudiante(grupo.Key),
                presentes.ToString(), faltas.ToString(), tardanzas.ToString(), registros.Count.ToString(), porcentaje });
        }
        return new ResultadoReporte("Asistencia por estudiante", new[] { "Código", "Estudiante", "Presentes", "Faltas", "Tardanzas", "Registros", "% asistencia" }, filas);
    }
}
