using GestionEstudiante.repository;
using System.Text.Json;
using GestionEstudiante.archivos;
using GestionEstudiante.model;
using GestionEstudiante.service;

internal static class Program
{
    private static void Comprobar(bool correcto, string mensaje)
    {
        if (!correcto)
        {
            throw new Exception(mensaje);
        }

        Console.WriteLine("OK: " + mensaje);
    }

    private static Estudiante Nuevo(string nombre)
    {
        Estudiante estudiante = new Estudiante();
        estudiante.nombre = nombre;
        estudiante.apellidos = "Rojas";
        estudiante.apoderado = "María Rojas";
        estudiante.telefono = "900000001";
        estudiante.correo = "Ana@example.com";
        return estudiante;
    }

    private static void ComprobarDatosConservados(string ruta)
    {
        using (JsonDocument documento = JsonDocument.Parse(File.ReadAllText(ruta)))
        {
            JsonElement raiz = documento.RootElement;
            Comprobar(raiz.GetProperty("aulas")[0].GetProperty("datoExtra").GetString() == "Piura" && raiz.GetProperty("matriculas")[0].GetProperty("id").GetInt32() == 5 && raiz.GetProperty("asistencias")[0].GetProperty("estado").GetString() == "Presente" && raiz.GetProperty("catalogos").GetProperty("niveles")[0].GetString() == "Primaria" && raiz.GetProperty("secuencias").GetProperty("siguienteIdAula").GetInt32() == 24 && raiz.GetProperty("secuencias").GetProperty("siguienteIdMatricula").GetInt32() == 6 && raiz.GetProperty("nota").GetString() == "Conservar", "Se conservan las otras secciones y contadores del JSON");
        }
    }

    private static void Main()
    {
        string carpeta = Path.Combine(Environment.CurrentDirectory, "tmp", "prueba-" + Guid.NewGuid());
        Directory.CreateDirectory(carpeta);
        string ruta = Path.Combine(carpeta, "gestion-escolar.json");
        ArchivoBaseDatos archivo = new ArchivoBaseDatos(ruta);
        EstudianteRepository repositorio = new EstudianteRepository(ruta);
        EstudianteService servicio = new EstudianteService(repositorio);
        ArchivoService servicioArchivos = new ArchivoService(repositorio);
        Comprobar(servicio.Listar().Length == 0, "Inicio sin archivo");
        servicio.Guardar(Nuevo(" Ana "));
        repositorio = new EstudianteRepository(ruta);
        servicio = new EstudianteService(repositorio);
        servicioArchivos = new ArchivoService(repositorio);
        Comprobar(servicio.Listar()[0].nombre == "Ana" && servicio.Listar()[0].codigo == "E000001", "Registro y reinicio");
        Comprobar(servicio.Buscar("ana", 1).Length == 1 && servicio.Buscar("E000001", 0).Length == 1 && servicio.Buscar("rojas", 0).Length == 1 && servicio.Buscar("sin coincidencias", 0).Length == 0, "Búsqueda por nombre, apellido y código sin distinguir mayúsculas");
        string[] telefonosIncorrectos =
        {
            "123",
            "90000000A"
        };
        foreach (string telefono in telefonosIncorrectos)
        {
            Estudiante incorrecto = Nuevo("Inválido");
            incorrecto.telefono = telefono;
            bool rechazado = false;
            try
            {
                servicio.Guardar(incorrecto);
            }
            catch (ArgumentException)
            {
                rechazado = true;
            }

            Comprobar(rechazado && servicio.Listar().Length == 1, "Teléfono incorrecto no agrega estudiante");
        }

        Estudiante incompleto = Nuevo("   ");
        bool campoRechazado = false;
        try
        {
            servicio.Guardar(incompleto);
        }
        catch (ArgumentException)
        {
            campoRechazado = true;
        }

        Comprobar(campoRechazado && servicio.Listar().Length == 1, "Nombre vacío rechazado");
        incompleto = Nuevo("Ana");
        incompleto.correo = "correo incorrecto";
        campoRechazado = false;
        try
        {
            servicio.Guardar(incompleto);
        }
        catch (ArgumentException)
        {
            campoRechazado = true;
        }

        Comprobar(campoRechazado && servicio.Listar().Length == 1, "Correo incorrecto rechazado");
        Estudiante editar = Nuevo("Lucía");
        editar.id = 1;
        DateTime fecha = servicio.Listar()[0].fechaCreacion;
        servicio.Guardar(editar);
        Comprobar(servicio.Listar()[0].fechaCreacion == fecha && servicio.Listar()[0].correo == "Ana@example.com", "Edición conserva fecha, código y escritura del correo");
        servicio.DarDeBaja(servicio.Listar()[0]);
        Comprobar(!new EstudianteService(ruta).Listar()[0].activo, "Baja lógica persistente");
        Comprobar(servicio.Buscar("", 1).Length == 0 && servicio.Buscar("", 2).Length == 1 && servicio.Buscar("", 0).Length == 1, "Filtros de activos e inactivos conservan el historial");
        string completo = "{\"aulas\":[{\"id\":23,\"datoExtra\":\"Piura\"}]," + "\"matriculas\":[{\"id\":5}],\"asistencias\":[{\"estado\":\"Presente\"}]," + "\"catalogos\":{\"niveles\":[\"Primaria\"]},\"nota\":\"Conservar\"," + File.ReadAllText(ruta).Substring(1);
        completo = completo.Replace("\"siguienteIdEstudiante\":", "\"siguienteIdAula\":24,\"siguienteIdMatricula\":6,\"siguienteIdEstudiante\":");
        File.WriteAllText(ruta, completo);
        repositorio = new EstudianteRepository(ruta);
        servicio = new EstudianteService(repositorio);
        servicioArchivos = new ArchivoService(repositorio);
        servicio.Guardar(Nuevo("Mateo"));
        ComprobarDatosConservados(ruta);
        string copia = Path.Combine(carpeta, "copia.json");
        servicioArchivos.CrearCopia(copia);
        ComprobarDatosConservados(copia);
        string respaldo = JsonSerializer.Serialize(archivo.Leer(copia));
        servicio.Guardar(Nuevo("Temporal"));
        servicioArchivos.Recuperar(copia);
        ComprobarDatosConservados(ruta);
        Comprobar(JsonSerializer.Serialize(archivo.Leer(ruta)) == respaldo, "Copia y recuperación completas");
        string invalido = Path.Combine(carpeta, "invalido.json");
        string[] errores = { "", "{", "[]", "null" };
        foreach (string error in errores)
        {
            File.WriteAllText(invalido, error);
            bool rechazado = false;
            try
            {
                servicioArchivos.CargarUsar(invalido);
            }
            catch (Exception)
            {
                rechazado = true;
            }

            Comprobar(rechazado && servicio.Listar().Length == 2, "Archivo ilegible rechazado sin cambiar la sesión");
        }

        string antes = File.ReadAllText(ruta);
        bool fallo = false;
        using (FileStream bloqueo = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            try
            {
                servicio.Guardar(Nuevo("No guardar"));
            }
            catch (IOException)
            {
                fallo = true;
            }
            catch (UnauthorizedAccessException)
            {
                fallo = true;
            }
        }

        Comprobar(fallo && servicio.Listar().Length == 2 && File.ReadAllText(ruta) == antes, "Error de escritura conserva archivo y estudiantes");
        servicio.Guardar(Nuevo("Reintento"));
        Comprobar(servicio.Listar()[2].id == 3, "Un error no consume identificadores");
        for (int i = 0; i < 9; i++)
            servicio.Guardar(Nuevo("Alumno " + i));
        Comprobar(new EstudianteService(ruta).Listar().Length == 12, "Registro sin el límite de diez estudiantes");
        string movido = Path.Combine(carpeta, "otra-carpeta", "gestion-escolar.json");
        string contenidoAntes = File.ReadAllText(ruta);
        servicioArchivos.MoverArchivo(movido);
        Comprobar(!File.Exists(ruta) && servicioArchivos.Ruta == movido && File.ReadAllText(movido) == contenidoAntes, "Mover cambia la ubicación sin perder contenido");
        servicio.Guardar(Nuevo("Después de mover"));
        Comprobar(new EstudianteService(movido).Listar().Length == 13 && !File.Exists(ruta), "Los cambios se guardan en la ubicación nueva");
        ComprobarDatosConservados(movido);
        string contenidoMovido = File.ReadAllText(movido);
        servicioArchivos.CargarUsar(copia);
        Comprobar(servicioArchivos.Ruta == copia && servicio.Listar().Length == 2 && File.ReadAllText(movido) == contenidoMovido, "Cargar / usar cambia la base activa sin sobrescribir la anterior");
        servicio.Guardar(Nuevo("Después de cargar"));
        Comprobar(new EstudianteService(copia).Listar().Length == 3 && File.ReadAllText(movido) == contenidoMovido, "El guardado usa el archivo seleccionado");
        ComprobarDatosConservados(copia);
        bool cargaRechazada = false;
        try
        {
            servicioArchivos.CargarUsar(invalido);
        }
        catch (InvalidDataException)
        {
            cargaRechazada = true;
        }

        Comprobar(cargaRechazada && servicioArchivos.Ruta == copia && servicio.Listar().Length == 3, "Una carga inválida conserva la base activa");
        bool movimientoRechazado = false;
        try
        {
            servicioArchivos.MoverArchivo(movido);
        }
        catch (IOException)
        {
            movimientoRechazado = true;
        }

        Comprobar(movimientoRechazado && servicioArchivos.Ruta == copia && File.Exists(copia) && File.ReadAllText(movido) == contenidoMovido, "Mover no sobrescribe otro archivo existente");
        string sinValidar = Path.Combine(carpeta, "sin-validar.json");
        string contenidoSinValidar = File.ReadAllText(copia).Replace("900000001", "telefono pendiente");
        File.WriteAllText(sinValidar, contenidoSinValidar);
        servicioArchivos.CargarUsar(sinValidar);
        Comprobar(servicioArchivos.Ruta == sinValidar && servicio.Listar()[0].telefono == "telefono pendiente",
            "Cargar no aplica las validaciones de datos del estudiante");
        Comprobar(new EstudianteService(sinValidar).Listar()[0].telefono == "telefono pendiente",
            "El inicio tampoco revisa los campos de los estudiantes cargados");
        servicioArchivos.CargarUsar(copia);

        string ubicacion = Path.Combine(carpeta, "ubicacion.txt");
        ArchivoBaseDatos seleccion = new ArchivoBaseDatos(null, ubicacion);
        seleccion.Usar(copia);
        ArchivoBaseDatos reinicio = new ArchivoBaseDatos(null, ubicacion);
        Comprobar(reinicio.Ruta == copia, "Se recuerda el archivo elegido al reiniciar");
        string segundaUbicacion = Path.Combine(carpeta, "movimiento-final", "copia.json");
        seleccion.Mover(segundaUbicacion);
        reinicio = new ArchivoBaseDatos(null, ubicacion);
        Comprobar(reinicio.Ruta == segundaUbicacion && !File.Exists(copia) && File.Exists(segundaUbicacion), "Se recuerda el archivo movido al reiniciar");
        ComprobarDatosConservados(segundaUbicacion);
        Console.WriteLine("Verificación terminada.");
    }
}
