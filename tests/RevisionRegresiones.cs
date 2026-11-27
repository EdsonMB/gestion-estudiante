using GestionEstudiante.archivos;
using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.service;
using GestionEstudiante.util;

internal static class RevisionRegresiones
{
    public static int Ejecutar(string raiz)
    {
        int correctas = 0;
        List<string> fallos = new();
        void Caso(string nombre, Action prueba)
        {
            try { prueba(); correctas++; }
            catch (Exception error) { fallos.Add(nombre + ": " + error.Message); }
        }
        GestorBaseDatos Crear(string nombre, int anio)
        {
            var gestor = new GestorBaseDatos(Path.Combine(raiz, nombre));
            new EstudianteService(new EstudianteRepository(gestor)).Guardar(new Estudiante
                { nombre = "Ana", apellidos = "Torres", apoderado = "Rosa", telefono = "900000001", correo = "" });
            var servicio = new EscolarService(gestor);
            servicio.Aulas.Guardar(new Aula { anio = anio, capacidad = 10 });
            servicio.Aulas.Guardar(new Aula { anio = anio, seccion = "B", capacidad = 10 });
            servicio.Matriculas.Registrar(1, 1, new DateTime(anio, 1, 1));
            return gestor;
        }
        Caso("No recrear una carpeta perdida con datos parciales", () =>
        {
            var gestor = Crear("carpeta-perdida", DateTime.Today.Year);
            string original = gestor.Ruta + "-original";
            Directory.Move(gestor.Ruta, original);
            var escolar = new EscolarService(gestor);
            Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = DateTime.Today.Year, seccion = "C" }));
            Exigir(!Directory.Exists(gestor.Ruta) && escolar.Aulas.Listar().Count == 2, "Se recreó la carpeta o se cambió la memoria.");
            new ArchivoService(gestor).CrearRespaldo(Path.Combine(raiz, "rescate"));
            Exigir(Directory.Exists(Path.Combine(raiz, "rescate")), "No se pudo rescatar la sesión.");
        });
        Caso("Una cancelación y alta no comparten día", () =>
        {
            var gestor = Crear("misma-fecha", DateTime.Today.Year);
            var escolar = new EscolarService(gestor);
            escolar.Matriculas.Cancelar(1);
            Rechazar(() => escolar.Matriculas.Registrar(1, 2, DateTime.Today));
            Exigir(escolar.Matriculas.Listar().Count == 1, "Se publicó la matrícula rechazada.");
        });
        Caso("Importación rechaza periodos superpuestos", () =>
        {
            var gestor = Crear("solapados", DateTime.Today.Year);
            gestor.Datos.matriculas[0].fechaCancelacion = DateTime.Today;
            gestor.Datos.matriculas.Add(new Matricula { id = 2, estudianteId = 1, aulaId = 2, fecha = DateTime.Today });
            gestor.Guardar(Constantes.ArchivoMatriculas, gestor.Datos.matriculas, x => x.ObtenerCampos());
            Rechazar(() => new ArchivoBaseDatos(gestor.Ruta).Leer(gestor.Ruta));
        });
        Caso("La vigencia termina con el año escolar", () =>
        {
            var matricula = new Matricula { fecha = new DateTime(2025, 3, 1) };
            Exigir(!matricula.VigenteEn(new DateTime(2026, 1, 1)), "Se muestra Sin registro fuera del año.");
            Exigir(matricula.VigenteEn(new DateTime(2025, 12, 31)), "Se perdió el último día del año.");
        });
        Caso("Reporte fuera del año sin filas ficticias", () =>
        {
            var gestor = Crear("rango-reporte", DateTime.Today.Year - 1);
            var reporte = new ReporteAsistencia(new EscolarService(gestor));
            var resultado = reporte.Generar(new FiltroReporte(DateTime.Today.Year - 1, 0,
                new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today));
            Exigir(resultado.Filas.Count == 0, "Aparecen estudiantes aunque el rango esté fuera del año.");
        });
        Caso("Una carpeta no equivale a un TXT opcional vacío", () =>
        {
            var gestor = Crear("archivo-es-carpeta", DateTime.Today.Year);
            File.Delete(Path.Combine(gestor.Ruta, Constantes.ArchivoAsistencias));
            Directory.CreateDirectory(Path.Combine(gestor.Ruta, Constantes.ArchivoAsistencias));
            Rechazar(() => new GestorBaseDatos(gestor.Ruta));
        });
        Caso("Aulas importadas no se duplican mediante espacios", () =>
        {
            var gestor = Crear("secciones", DateTime.Today.Year);
            gestor.Datos.aulas[1].seccion = " a ";
            gestor.Guardar(Constantes.ArchivoAulas, gestor.Datos.aulas, x => x.ObtenerCampos());
            Rechazar(() => new GestorBaseDatos(gestor.Ruta));
        });
        Caso("Una matrícula posterior conserva el historial anterior", () =>
        {
            int anio = DateTime.Today.Year - 1;
            var gestor = Crear("historial-valido", anio);
            gestor.Datos.matriculas[0].fechaCancelacion = new DateTime(anio, 1, 2);
            gestor.Guardar(Constantes.ArchivoMatriculas, gestor.Datos.matriculas, x => x.ObtenerCampos());
            var escolar = new EscolarService(gestor);
            escolar.Matriculas.Registrar(1, 2, new DateTime(anio, 1, 3));
            escolar.Asistencias.GuardarDia(1, new DateTime(anio, 1, 2), new()
                { new Asistencia { matriculaId = 1, estado = EstadoAsistencia.Presente } });
            escolar.Asistencias.GuardarDia(2, new DateTime(anio, 1, 3), new()
                { new Asistencia { matriculaId = 2, estado = EstadoAsistencia.Falta } });
            var recargado = new GestorBaseDatos(gestor.Ruta);
            var reporte = new ReporteAsistencia(new EscolarService(recargado));
            var resultado = reporte.Generar(new FiltroReporte(anio, 0,
                new DateTime(anio - 1, 12, 25), new DateTime(anio + 1, 1, 8)));
            Exigir(resultado.Filas.Count == 1 && resultado.Filas[0][5] == "2" && resultado.Filas[0][6] == "50.0%",
                "El historial no conserva ambos días y su porcentaje.");
            Exigir(escolar.Asistencias.MatriculasDelDia(1, new DateTime(anio, 1, 3)).Count == 0,
                "La matrícula cancelada sigue en el aula anterior.");
        });
        Caso("Se puede editar la capacidad de un aula importada normalizada", () =>
        {
            var gestor = Crear("seccion-normalizada", DateTime.Today.Year);
            gestor.Datos.aulas[0].seccion = " a ";
            gestor.Guardar(Constantes.ArchivoAulas, gestor.Datos.aulas, x => x.ObtenerCampos());
            var recargado = new GestorBaseDatos(gestor.Ruta);
            new EscolarService(recargado).Aulas.Guardar(new Aula
                { id = 1, anio = DateTime.Today.Year, capacidad = 12 });
            Exigir(recargado.Datos.aulas[0].capacidad == 12, "Se bloqueó una edición válida.");
        });
        foreach (string fallo in fallos) Console.Error.WriteLine("REGRESIÓN: " + fallo);
        if (fallos.Count > 0) throw new Exception(fallos.Count + " casos de revisión fallaron.");
        return correctas;
    }
    private static void Exigir(bool condicion, string mensaje)
    {
        if (!condicion) throw new Exception(mensaje);
    }
    private static void Rechazar(Action accion)
    {
        try { accion(); }
        catch (Exception error) when (error is ArgumentException or InvalidOperationException or InvalidDataException or IOException)
        { return; }
        throw new Exception("La operación debía rechazarse.");
    }
}
