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
        StringBuilder texto = new StringBuilder(FormatoTxt.CrearFila(resultado.Columnas));
        foreach (string[] fila in resultado.Filas)
        {
            texto.AppendLine();
            texto.Append(FormatoTxt.CrearFila(fila));
        }
        string temporal = ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporal, texto.ToString(), new UTF8Encoding(false));
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
        foreach (Aula aula in aulas.Listar())
        {
            if (aula.anio != filtro.Anio || (filtro.AulaId != 0 && aula.id != filtro.AulaId))
                continue;
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
        Dictionary<int, int> estudiantePorMatricula = new Dictionary<int, int>();
        Dictionary<int, int[]> conteos = new Dictionary<int, int[]>();
        List<int> ordenEstudiantes = new List<int>();
        foreach (Matricula matricula in matriculas.Listar())
        {
            if (matricula.fecha.Year != filtro.Anio || (filtro.AulaId != 0 && matricula.aulaId != filtro.AulaId) ||
                matricula.fecha.Date > hasta || matricula.fechaCancelacion?.Date < desde)
                continue;

            estudiantePorMatricula.Add(matricula.id, matricula.estudianteId);
            if (!conteos.ContainsKey(matricula.estudianteId))
            {
                conteos.Add(matricula.estudianteId, new int[3]);
                ordenEstudiantes.Add(matricula.estudianteId);
            }
        }

        foreach (Asistencia asistencia in asistencias.Listar())
        {
            if (asistencia.fecha.Date < desde || asistencia.fecha.Date > hasta ||
                !estudiantePorMatricula.TryGetValue(asistencia.matriculaId, out int estudianteId))
                continue;

            int[] conteo = conteos[estudianteId];
            switch (asistencia.estado)
            {
                case Constantes.AsistenciaPresente: conteo[0]++; break;
                case Constantes.AsistenciaFalta: conteo[1]++; break;
                case Constantes.AsistenciaTardanza: conteo[2]++; break;
            }
        }

        List<string[]> filas = new List<string[]>();
        Dictionary<int, Estudiante> estudiantesPorId = new Dictionary<int, Estudiante>();
        foreach (Estudiante estudiante in estudiantes.Listar())
        {
            estudiantesPorId.Add(estudiante.id, estudiante);
        }
        foreach (int estudianteId in ordenEstudiantes)
        {
            int[] conteo = conteos[estudianteId];
            int presentes = conteo[0];
            int faltas = conteo[1];
            int tardanzas = conteo[2];
            int total = presentes + faltas + tardanzas;
            string porcentaje = total == 0 ? "Sin registros" :
                (100m * (presentes + tardanzas) / total).ToString("0.0", CultureInfo.InvariantCulture) + "%";
            Estudiante estudiante = estudiantesPorId[estudianteId];
            filas.Add(new[] { estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos,
                presentes.ToString(), faltas.ToString(), tardanzas.ToString(), total.ToString(), porcentaje });
        }
        return new ResultadoReporte(Constantes.ReporteAsistencia, columnas, filas);
    }
}
