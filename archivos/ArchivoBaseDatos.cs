using System.Globalization;
using System.Text;
using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.archivos;

public class ArchivoBaseDatos
{
    private static readonly string[] Archivos = { Constantes.ArchivoEstudiantes, Constantes.ArchivoAulas,
        Constantes.ArchivoMatriculas, Constantes.ArchivoAsistencias };
    private static readonly UTF8Encoding Utf8 = new UTF8Encoding(false, true);
    private readonly string? archivoUbicacion;
    public string Ruta { get; private set; } = "";

    public ArchivoBaseDatos(string? ruta = null)
    {
        if (ruta == null)
        {
            archivoUbicacion = Path.Combine(Constantes.CarpetaAplicacion, Constantes.ArchivoUbicacion);
        }
        else
        {
            Ruta = Normalizar(ruta);
        }
    }

    public string? LeerUbicacionGuardada()
    {
        if (archivoUbicacion == null || !File.Exists(archivoUbicacion))
        {
            return null;
        }

        string ruta = File.ReadAllText(archivoUbicacion, Utf8).Trim();
        if (!Path.IsPathFullyQualified(ruta))
        {
            throw new InvalidDataException("La ubicación de datos guardada no es válida.");
        }
        return ruta;
    }

    public void Usar(string ruta, bool recordar)
    {
        ruta = Normalizar(ruta);
        if (recordar && archivoUbicacion != null)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(archivoUbicacion)!);
            Escribir(archivoUbicacion, ruta);
        }

        Ruta = ruta;
    }

    public BaseDatosEscolar AbrirSeleccion(string ruta)
    {
        if (!Directory.Exists(ruta))
        {
            throw new DirectoryNotFoundException("La carpeta de datos seleccionada no está disponible.");
        }
        if (Directory.GetFileSystemEntries(ruta).Length == 0)
        {
            InicializarCarpeta(ruta);
        }

        return Leer(ruta);
    }

    private static void InicializarCarpeta(string ruta)
    {
        List<string> creados = new List<string>();
        try
        {
            foreach (string archivo in Archivos)
            {
                string rutaArchivo = Path.Combine(ruta, archivo);
                using FileStream vacio = File.Open(rutaArchivo, FileMode.CreateNew, FileAccess.Write);
                creados.Add(rutaArchivo);
            }
        }
        catch
        {
            foreach (string archivo in creados)
            {
                File.Delete(archivo);
            }
            throw;
        }
    }

    public BaseDatosEscolar Leer(string ruta)
    {
        if (!Directory.Exists(ruta))
        {
            throw new DirectoryNotFoundException("La carpeta de datos seleccionada no está disponible.");
        }
        ValidarArchivos(ruta);
        BaseDatosEscolar datos = new BaseDatosEscolar();
        //Estudiantes
        datos.estudiantes = LeerArchivo(ruta, Constantes.ArchivoEstudiantes,
            Constantes.EstudiantesColumnas, LeerEstudiante);
        ValidarCodigos(datos.estudiantes);
        //Aulas
        datos.aulas = LeerArchivo(ruta, Constantes.ArchivoAulas,
            Constantes.AulasColumnas, LeerAula);
        //Matriculas
        datos.matriculas = LeerArchivo(ruta, Constantes.ArchivoMatriculas,
            Constantes.MatriculasColumnas, LeerMatricula);
        //Asistencias
        datos.asistencias = LeerArchivo(ruta, Constantes.ArchivoAsistencias,
            Constantes.AsistenciasColumnas, LeerAsistencia);
        return datos;
    }

    private static void ValidarCodigos(List<Estudiante> estudiantes)
    {
        HashSet<string> codigos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < estudiantes.Count; i++)
        {
            if (!codigos.Add(estudiantes[i].codigo))
            {
                throw new InvalidDataException($"Código repetido en {Constantes.ArchivoEstudiantes}, línea {i + 1}.");
            }
        }
    }

    private static Estudiante LeerEstudiante(string[] campos)
    {
        if (campos[1].Length == 0)
        {
            throw new FormatException("El código del estudiante está vacío.");
        }

        return new Estudiante
        {
            id = Entero(campos[0]),
            codigo = campos[1],
            nombre = campos[2],
            apellidos = campos[3],
            apoderado = campos[4],
            telefono = campos[5],
            correo = campos[6],
            fechaCreacion = Fecha(campos[7]),
            activo = bool.Parse(campos[8])
        };
    }

    private Aula LeerAula(string[] campos)
    {
        return new Aula
        {
            id = Entero(campos[0]),
            nivel = campos[1],
            grado = Entero(campos[2]),
            seccion = campos[3].Trim().ToUpperInvariant(),
            anio = Entero(campos[4]),
            capacidad = Entero(campos[5]),
            activo = bool.Parse(campos[6])
        };
    }

    private Matricula LeerMatricula(string[] campos)
    {
        return new Matricula
        {
            id = Entero(campos[0]),
            estudianteId = Entero(campos[1]),
            aulaId = Entero(campos[2]),
            fecha = Fecha(campos[3]),
            fechaCancelacion = campos[4] == "" ? null : Fecha(campos[4])
        };
    }

    private Asistencia LeerAsistencia(string[] campos)
    {
        string estado = campos[3];
        if (!Asistencia.EsEstadoValido(estado))
        {
            throw new FormatException("Se esperaba Presente, Falta o Tardanza.");
        }

        return new Asistencia
        {
            id = Entero(campos[0]),
            matriculaId = Entero(campos[1]),
            fecha = Fecha(campos[2]),
            estado = estado,
            observacion = campos[4]
        };
    }

    private static List<T> LeerArchivo<T>(string ruta, string archivo, int columnas, Func<string[], T> convertir)
        where T : ModeloBase
    {
        string[] filas = File.ReadAllLines(Path.Combine(ruta, archivo), Utf8);
        HashSet<int> ids = new HashSet<int>();
        List<T> entidades = new List<T>();

        for (int i = 0; i < filas.Length; i++)
        {
            string[] campos = LeerCampos(filas[i], archivo, i + 1, columnas);
            T entidad;
            try
            {
                entidad = convertir(campos);
            }
            catch (Exception error) when (error is FormatException or ArgumentException or OverflowException)
            {
                throw new InvalidDataException($"Registro inválido en {archivo}, línea {i + 1}. {error.Message}", error);
            }

            if (!ids.Add(entidad.id))
            {
                throw new InvalidDataException($"Identificador repetido en {archivo}, línea {i + 1}.");
            }
            entidades.Add(entidad);
        }

        return entidades;
    }

    private static void ValidarArchivos(string ruta)
    {
        foreach (string archivo in Archivos)
        {
            if (!File.Exists(Path.Combine(ruta, archivo)))
            {
                throw new InvalidDataException($"La carpeta de datos debe contener el archivo {archivo}.");
            }
        }
    }

    public void Guardar<T>(string archivo, List<T> registros) where T : ModeloBase
    {
        if (!Directory.Exists(Ruta))
        {
            throw new IOException("Seleccione una carpeta de datos disponible antes de guardar.");
        }
        ValidarArchivos(Ruta);

        StringBuilder texto = new StringBuilder();
        foreach (T registro in registros)
        {
            texto.AppendLine(FormatoTxt.CrearFila(registro.ObtenerCampos()));
        }

        Escribir(Path.Combine(Ruta, archivo), texto.ToString());
    }

    private static string Normalizar(string ruta)
    {
        return Path.TrimEndingDirectorySeparator(Path.GetFullPath(ruta));
    }

    private static string[] LeerCampos(string linea, string archivo, int numero, int cantidad)
    {
        string[] campos = linea.Split(Constantes.Separador);
        if (campos.Length != cantidad)
        {
            throw ErrorFila(archivo, numero);
        }

        for (int i = 0; i < campos.Length; i++)
        {
            campos[i] = DecodificarCampo(campos[i], archivo, numero);
        }
        return campos;
    }

    private static string DecodificarCampo(string campo, string archivo, int numero)
    {
        StringBuilder texto = new StringBuilder();
        for (int i = 0; i < campo.Length; i++)
        {
            char caracter = campo[i];
            if (caracter == '\\')
            {
                i++;
                if (i == campo.Length) throw ErrorFila(archivo, numero);
                switch (campo[i])
                {
                    case '\\': caracter = '\\'; break;
                    case 'p': caracter = Constantes.Separador; break;
                    case 'r': caracter = '\r'; break;
                    case 'n': caracter = '\n'; break;
                    default: throw ErrorFila(archivo, numero);
                }
            }
            texto.Append(caracter);
        }
        return texto.ToString();
    }

    private static int Entero(string texto)
    {
        if (!int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out int valor) || valor <= 0)
        {
            throw new FormatException("Se esperaba un entero positivo.");
        }
        return valor;
    }

    private static DateTime Fecha(string texto)
    {
        return DateTime.ParseExact(texto, Constantes.FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }

    private static InvalidDataException ErrorFila(string archivo, int numero)
    {
        return new InvalidDataException("Registro inválido en " + archivo + ", línea " + numero + ".");
    }

    private static void Escribir(string ruta, string contenido)
    {
        string temporal = ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporal, contenido, Utf8);
            File.Move(temporal, ruta, true);
        }
        finally
        {
            File.Delete(temporal);
        }
    }
}
