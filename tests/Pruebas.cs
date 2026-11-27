using GestionEstudiante.archivos;
using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.service;
using GestionEstudiante.util;

string raiz = Path.Combine(Path.GetTempPath(), "gestion-test-" + Guid.NewGuid().ToString("N"));
int comprobaciones = 0;
void Comprobar(bool condicion, string mensaje)
{
    if (!condicion) throw new Exception("FALLO: " + mensaje);
    comprobaciones++;
}
void Rechazar(Action accion, string mensaje)
{
    try { accion(); }
    catch (Exception e) when (e is ArgumentException or InvalidOperationException or InvalidDataException or IOException or UnauthorizedAccessException)
    { comprobaciones++; return; }
    throw new Exception("FALLO: se aceptó " + mensaje);
}
try
{
    var gestor = new GestorBaseDatos(Path.Combine(raiz, "datos"));
    var alumnos = new EstudianteService(new EstudianteRepository(gestor));
    var escolar = new EscolarService(gestor);
    var archivos = new ArchivoService(gestor);
    int anio = DateTime.Today.Year;
    DateTime inicio = new DateTime(anio, 1, 1);
    alumnos.Guardar(new Estudiante { nombre = "Ana|\\p\nLuz", apellidos = "Torres", apoderado = "Rosa", telefono = "900000001", correo = "rosa@example.com" });
    alumnos.Guardar(new Estudiante { nombre = "Luis", apellidos = "Rojas", apoderado = "Pedro", telefono = "900000002", correo = "" });
    alumnos.Guardar(new Estudiante { nombre = "Eva", apellidos = "Díaz", apoderado = "Juana", telefono = "900000003", correo = "" });
    escolar.Aulas.Guardar(new Aula { anio = anio, capacidad = 1 });
    escolar.Aulas.Guardar(new Aula { anio = anio, seccion = "B", capacidad = 2 });
    Comprobar(Directory.GetFiles(gestor.Ruta, "*.txt").Length == 4, "primera escritura crea el conjunto");
    Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = anio, seccion = "a" }), "aula duplicada");
    Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = anio, seccion = "C", capacidad = 0 }), "capacidad cero");
    Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = anio, nivel = "Secundaria", grado = 6 }), "grado inválido");
    escolar.Aulas.Guardar(new Aula { anio = anio, nivel = "Inicial", grado = 3 });
    Comprobar(escolar.Aulas.Listar().Last().Descripcion.Contains("3 años"), "aula de Inicial");
    Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = anio, nivel = "Inicial", grado = 1 }), "edad de Inicial inválida");
    escolar.Matriculas.Registrar(1, 1, inicio);
    Rechazar(() => escolar.Matriculas.Registrar(1, 2, inicio), "doble matrícula anual");
    Rechazar(() => escolar.Matriculas.Registrar(2, 1, inicio), "aula llena");
    Rechazar(() => escolar.Matriculas.Registrar(2, 2, DateTime.Today.AddDays(1)), "fecha futura");
    Rechazar(() => escolar.Matriculas.Registrar(2, 2, inicio.AddYears(-1)), "año incorrecto");
    Rechazar(() => escolar.Aulas.CambiarEstado(1), "desactivar aula ocupada");
    Rechazar(() => alumnos.DarDeBaja(alumnos.Listar()[0]), "baja de estudiante matriculado");
    escolar.Matriculas.Registrar(2, 2, inicio);
    escolar.Matriculas.Registrar(3, 2, inicio);
    Rechazar(() => escolar.Aulas.Guardar(new Aula { id = 2, anio = anio, seccion = "B", capacidad = 1 }), "reducir capacidad por debajo de ocupación");
    Rechazar(() => escolar.Aulas.Guardar(new Aula { id = 2, anio = anio - 1, seccion = "B", capacidad = 2 }), "cambiar identidad de aula con historial");
    Rechazar(() => escolar.Asistencias.GuardarDia(0, inicio, new()), "aula inexistente");
    Rechazar(() => escolar.Asistencias.GuardarDia(1, inicio.AddYears(-1), new()), "asistencia de otro año");
    escolar.Asistencias.GuardarDia(1, inicio, new() { new Asistencia { matriculaId = 1, estado = EstadoAsistencia.Presente, observacion = "Entró|tarde\\\nAvisó" } });
    escolar.Asistencias.GuardarDia(1, inicio, new() { new Asistencia { matriculaId = 1, estado = EstadoAsistencia.Tardanza, observacion = "Entró|tarde\\\nAvisó" } });
    Comprobar(escolar.Asistencias.Listar().Count == 1 && escolar.Asistencias.Listar()[0].id == 1, "corrección mantiene id sin duplicar");
    Comprobar(escolar.Asistencias.Listar()[0].estado == EstadoAsistencia.Tardanza, "corrección persiste estado");
    Rechazar(() => escolar.Asistencias.GuardarDia(2, inicio, new() { new Asistencia { matriculaId = 1 } }), "asistencia de otra aula");
    Rechazar(() => escolar.Asistencias.GuardarDia(1, inicio, new() { new Asistencia { matriculaId = 1 }, new Asistencia { matriculaId = 1 } }), "asistencia repetida");
    Rechazar(() => escolar.Asistencias.GuardarDia(1, inicio, new() { new Asistencia { matriculaId = 1, estado = (EstadoAsistencia)100 } }), "estado inválido");
    Rechazar(() => escolar.Asistencias.GuardarDia(1, DateTime.Today.AddDays(1), new()), "asistencia futura");
    Rechazar(() => escolar.Asistencias.GuardarDia(1, inicio.AddDays(-1), new() { new Asistencia { matriculaId = 1 } }), "asistencia anterior a matrícula");
    var recargado = new GestorBaseDatos(gestor.Ruta);
    Comprobar(recargado.Datos.estudiantes[0].nombre == "Ana|\\p\nLuz", "escape TXT del estudiante");
    Comprobar(recargado.Datos.asistencias[0].observacion == "Entró|tarde\\\nAvisó", "escape TXT de la asistencia");
    Comprobar(recargado.Datos.matriculas.Count == 3 && recargado.Datos.aulas.Count == 3, "persistencia completa");
    Reporte reporte = new ReporteAsistencia(escolar);
    var filtro = new FiltroReporte(anio, 0, inicio, DateTime.Today);
    var resultado = reporte.Generar(filtro);
    Comprobar(resultado.Filas[0][6] == "100.0%", "tardanza cuenta como asistencia");
    Comprobar(resultado.Filas[1][6] == "Sin registros" && resultado.Filas[1][3] == "0", "sin registros no cuenta como falta");
    Rechazar(() => reporte.Generar(filtro with { Desde = DateTime.Today.AddDays(1) }), "rango de reporte invertido");
    string rutaReporte = Path.Combine(raiz, "reporte.txt");
    reporte.Exportar(rutaReporte, resultado);
    string[] lineasReporte = File.ReadAllLines(rutaReporte);
    Comprobar(lineasReporte[0] == "Código|Estudiante|Presentes|Faltas|Tardanzas|Registros|% asistencia", "cabecera TXT con palotes");
    Comprobar(lineasReporte.Length == resultado.Filas.Count + 1 && lineasReporte.All(x => x.Split('|').Length == 7), "una fila física por registro TXT");
    Comprobar(lineasReporte[1].Contains(@"Ana\p\\p\nLuz Torres"), "palotes, barras y saltos escapados sin perder información");
    string otroFormato = Path.Combine(raiz, "reporte.csv");
    File.WriteAllText(otroFormato, "conservar");
    Rechazar(() => reporte.Exportar(otroFormato, resultado), "exportación con otra extensión");
    Comprobar(File.ReadAllText(otroFormato) == "conservar", "rechazo de formato conserva archivo existente");
    Reporte ocupacion = new ReporteOcupacion(escolar);
    ocupacion.Exportar(Path.Combine(raiz, "ocupacion.txt"), ocupacion.Generar(filtro));
    Comprobar(File.ReadAllLines(Path.Combine(raiz, "ocupacion.txt"))[0] == "Aula|Capacidad|Matriculados|Cupos disponibles|Estado", "ocupación también exporta TXT");
    string copia = Path.Combine(raiz, "respaldo");
    archivos.CrearRespaldo(copia);
    Comprobar(Directory.GetFiles(copia).Length == 4 && Directory.GetFiles(copia).All(x => Path.GetExtension(x) == ".txt"), "respaldo solo contiene cuatro TXT");
    Comprobar(File.ReadAllText(Path.Combine(copia, Constantes.ArchivoEstudiantes)) == File.ReadAllText(Path.Combine(gestor.Ruta, Constantes.ArchivoEstudiantes)), "respaldo mantiene formato TXT original");
    Rechazar(() => archivos.CrearRespaldo(copia), "no sobrescribir respaldo existente");
    Rechazar(() => archivos.CrearRespaldo(Path.Combine(gestor.Ruta, "respaldo")), "respaldo dentro de los datos activos");
    escolar.Matriculas.Cancelar(1);
    Comprobar(escolar.Aulas.Disponibles(escolar.Aulas.Listar()[0]) == 1, "cancelación libera cupo");
    Comprobar(escolar.Asistencias.Listar().Count == 1, "cancelación conserva historial");
    Rechazar(() => escolar.Matriculas.Cancelar(1), "cancelación repetida");
    alumnos.DarDeBaja(alumnos.Listar()[0]);
    escolar.Aulas.CambiarEstado(1);
    Rechazar(() => escolar.Matriculas.Registrar(1, 2, inicio), "estudiante inactivo");
    new ArchivoBaseDatos(gestor.Ruta).Leer(gestor.Ruta);
    archivos.RecuperarRespaldo(copia);
    Comprobar(alumnos.Listar()[0].activo && escolar.Matriculas.Listar()[0].Activa, "servicios existentes ven datos restaurados");
    Comprobar(Directory.Exists(Path.Combine(raiz, "datos")), "restauración conserva carpeta anterior");
    string antes = gestor.Ruta;
    string incompleto = Path.Combine(raiz, "incompleto");
    Directory.CreateDirectory(incompleto);
    File.WriteAllText(Path.Combine(incompleto, Constantes.ArchivoEstudiantes), "");
    Rechazar(() => archivos.RecuperarRespaldo(incompleto), "respaldo incompleto");
    foreach (string archivo in Directory.GetFiles(copia))
        File.Copy(archivo, Path.Combine(incompleto, Path.GetFileName(archivo)), true);
    File.WriteAllText(Path.Combine(incompleto, Constantes.ArchivoAsistencias), "fila inválida");
    Rechazar(() => archivos.RecuperarRespaldo(incompleto), "respaldo con TXT inválido");
    Comprobar(gestor.Ruta == antes && escolar.Matriculas.Listar()[0].Activa, "recuperación fallida no cambia sesión");
    // Una escritura fallida no debe publicar cambios en memoria.
    string aulasRuta = Path.Combine(gestor.Ruta, "aulas.txt");
    string contenido = File.ReadAllText(aulasRuta);
    File.Delete(aulasRuta);
    Directory.CreateDirectory(aulasRuta);
    Rechazar(() => escolar.Aulas.Guardar(new Aula { anio = anio, seccion = "C" }), "error de escritura");
    Comprobar(escolar.Aulas.Listar().Count == 3, "escritura fallida conserva memoria");
    Directory.Delete(aulasRuta);
    File.WriteAllText(aulasRuta, contenido);
    string legado = Path.Combine(raiz, "legado");
    Directory.CreateDirectory(legado);
    File.Copy(Path.Combine(gestor.Ruta, "estudiantes.txt"), Path.Combine(legado, "estudiantes.txt"));
    var antiguos = new GestorBaseDatos(legado);
    Comprobar(antiguos.Datos.estudiantes.Count == 3 && antiguos.Datos.aulas.Count == 0, "compatibilidad carpeta antigua");
    File.WriteAllText(Path.Combine(legado, "matriculas.txt"), "1|999|1|2026-01-01T00:00:00.0000000|\n");
    Rechazar(() => archivos.CargarUsar(legado), "importación con referencias inexistentes");
    Comprobar(gestor.Ruta == antes, "importación fallida conserva ruta");
    File.Delete(Path.Combine(legado, "matriculas.txt"));
    File.WriteAllText(Path.Combine(legado, "estudiantes.txt"), "1|E1|Ana\\q|Torres|Rosa|900000001||2026-01-01T00:00:00.0000000|true\n");
    Rechazar(() => archivos.CargarUsar(legado), "escape inválido");
    escolar.Asistencias.GuardarDia(1, inicio, new());
    Comprobar(escolar.Asistencias.Listar().Count == 0, "Sin registro elimina marca previa");
    reporte = new ReporteOcupacion(escolar);
    Comprobar(reporte.Generar(filtro).Filas.Sum(x => int.Parse(x[2])) == 3, "reporte de ocupación");
    archivos.MoverArchivo(Path.Combine(raiz, "movidos"));
    Comprobar(!Directory.Exists(antes) && gestor.Ruta.EndsWith("movidos"), "traslado actualiza ruta");
    Comprobar(new GestorBaseDatos(gestor.Ruta).Datos.matriculas.Count == 3, "traslado mantiene datos");
    new GestorBaseDatos(Path.Combine(args[0], "ejemplos"));
    comprobaciones++;
    comprobaciones += RevisionRegresiones.Ejecutar(raiz);
    Console.WriteLine($"OK: {comprobaciones} comprobaciones de dominio, persistencia y respaldos.");
}
finally { if (Directory.Exists(raiz)) Directory.Delete(raiz, true); }
