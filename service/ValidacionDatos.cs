using GestionEstudiante.model;
namespace GestionEstudiante.service;

public static class ValidacionDatos
{
    public static void ValidarAula(Aula aula)
    {
        int minimo = aula.nivel == "Inicial" ? 3 : 1;
        int maximo = aula.nivel == "Primaria" ? 6 : aula.nivel is "Secundaria" or "Inicial" ? 5 : 0;
        if (maximo == 0 || aula.grado < minimo || aula.grado > maximo)
            throw new ArgumentException("Seleccione nivel y grado válidos (Inicial: 3 a 5 años; Primaria: 1 a 6; Secundaria: 1 a 5).");
        if (string.IsNullOrWhiteSpace(aula.seccion) || aula.seccion.Length > 10)
            throw new ArgumentException("La sección es obligatoria y admite hasta 10 caracteres.");
        if (aula.anio < 2000 || aula.anio > 2100 || aula.capacidad < 1 || aula.capacidad > 1000)
            throw new ArgumentException("Ingrese un año entre 2000 y 2100 y una capacidad entre 1 y 1000.");
    }

    public static void Validar(BaseDatosEscolar datos)
    {
        try
        {
            foreach (Estudiante estudiante in datos.estudiantes) EstudianteService.ValidarEstudiante(estudiante);
            foreach (Aula aula in datos.aulas) ValidarAula(aula);
            if (datos.aulas.GroupBy(x => (x.nivel, x.grado, x.seccion.ToUpperInvariant(), x.anio)).Any(g => g.Count() > 1))
                throw new ArgumentException("Hay aulas repetidas para el mismo nivel, grado, sección y año.");
            foreach (Matricula matricula in datos.matriculas)
            {
                Estudiante estudiante = datos.estudiantes.SingleOrDefault(x => x.id == matricula.estudianteId)
                    ?? throw new ArgumentException("Una matrícula referencia un estudiante inexistente.");
                Aula aula = datos.aulas.SingleOrDefault(x => x.id == matricula.aulaId)
                    ?? throw new ArgumentException("Una matrícula referencia un aula inexistente.");
                if (matricula.fecha.Year != aula.anio || matricula.fecha.Date > DateTime.Today ||
                    matricula.fechaCancelacion?.Date < matricula.fecha.Date || matricula.fechaCancelacion?.Date > DateTime.Today)
                    throw new ArgumentException("Fechas de matrícula inválidas.");
                if (matricula.Activa && (!estudiante.activo || !aula.activo))
                    throw new ArgumentException("Una matrícula activa requiere estudiante y aula activos.");
            }
            if (datos.matriculas.Where(x => x.Activa).GroupBy(x =>
                (x.estudianteId, datos.aulas.Single(a => a.id == x.aulaId).anio)).Any(g => g.Count() > 1))
                throw new ArgumentException("Un estudiante tiene más de una matrícula activa en el mismo año.");
            foreach (Aula aula in datos.aulas)
                if (datos.matriculas.Count(x => x.aulaId == aula.id && x.Activa) > aula.capacidad)
                    throw new ArgumentException("La ocupación supera la capacidad de un aula.");
            if (datos.asistencias.GroupBy(x => (x.matriculaId, x.fecha.Date)).Any(g => g.Count() > 1))
                throw new ArgumentException("Hay asistencias repetidas para una matrícula y fecha.");
            foreach (Asistencia asistencia in datos.asistencias)
            {
                Matricula matricula = datos.matriculas.SingleOrDefault(x => x.id == asistencia.matriculaId)
                    ?? throw new ArgumentException("Una asistencia referencia una matrícula inexistente.");
                Aula aula = datos.aulas.Single(x => x.id == matricula.aulaId);
                if (!matricula.VigenteEn(asistencia.fecha) || asistencia.fecha.Year != aula.anio ||
                    asistencia.fecha.Date > DateTime.Today || !Enum.IsDefined(asistencia.estado) || asistencia.observacion.Length > 250)
                    throw new ArgumentException("La asistencia contiene una fecha, estado u observación inválidos.");
            }
        }
        catch (ArgumentException error)
        {
            throw new InvalidDataException("La carpeta contiene datos inconsistentes: " + error.Message, error);
        }
    }
}
