using System.Globalization;
using System.Text;
using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.archivos;

public partial class ArchivoBaseDatos
{
    private static readonly UTF8Encoding Utf8 = new UTF8Encoding(false, true);
    private readonly string? archivoUbicacion;
    private bool carpetaInicializada;
    public string Ruta { get; private set; }

    public ArchivoBaseDatos(string? ruta = null)
    {
        if (ruta == null)
        {
            archivoUbicacion = Path.Combine(Constantes.CarpetaAplicacion, Constantes.ArchivoUbicacion);
            ruta = Constantes.CarpetaDatos;
            if (File.Exists(archivoUbicacion))
            {
                ruta = File.ReadAllText(archivoUbicacion, Utf8).Trim();
                if (!Path.IsPathFullyQualified(ruta) || !Directory.Exists(ruta))
                {
                    throw new InvalidDataException("La carpeta de datos guardada ya no está disponible. Revise " + archivoUbicacion);
                }
            }
        }

        Ruta = Normalizar(ruta);
        carpetaInicializada = Existe();
    }

    public bool Existe()
    {
        return Directory.Exists(Ruta) || File.Exists(Ruta);
    }

    public void Usar(string ruta)
    {
        ruta = Normalizar(ruta);
        if (archivoUbicacion != null)
        {
            Escribir(archivoUbicacion, ruta);
        }

        Ruta = ruta;
        carpetaInicializada = true;
    }

    public BaseDatosEscolar Leer(string ruta)
    {
        BaseDatosEscolar datos = new BaseDatosEscolar();
        string archivo = Constantes.ArchivoEstudiantes;
        //estudiantes
        datos.estudiantes = LeerArchivo(ruta, archivo, Constantes.EstudiantesColumnas, (indice, campos, ids, codigos) =>
        {
            int numero = indice + 1;
            if (!LeerEnteroPositivo(campos[0], out int id) || campos[1].Length == 0
                || !DateTime.TryParseExact(campos[7], Constantes.FormatoFecha, CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out DateTime fecha)
                || !bool.TryParse(campos[8], out bool activo))
            {
                throw ErrorFila(archivo, numero);
            }

            if (!ids.Add(id) || !codigos.Add(campos[1]))
            {
                throw new InvalidDataException("Identificador o código repetido en " + archivo + ", línea " + numero + ".");
            }

            return new Estudiante
            {
                id = id,
                codigo = campos[1],
                nombre = campos[2],
                apellidos = campos[3],
                apoderado = campos[4],
                telefono = campos[5],
                correo = campos[6],
                fechaCreacion = fecha,
                activo = activo
            };
        });
        datos.aulas = LeerOpcional<Aula>(ruta, Constantes.ArchivoAulas, 7, c => new Aula
        {
            id = Entero(c[0]), nivel = c[1], grado = Entero(c[2]), seccion = c[3].Trim().ToUpperInvariant(),
            anio = Entero(c[4]), capacidad = Entero(c[5]), activo = bool.Parse(c[6])
        });
        datos.matriculas = LeerOpcional<Matricula>(ruta, Constantes.ArchivoMatriculas, 5, c => new Matricula
        {
            id = Entero(c[0]), estudianteId = Entero(c[1]), aulaId = Entero(c[2]),
            fecha = Fecha(c[3]), fechaCancelacion = c[4] == "" ? null : Fecha(c[4])
        });
        datos.asistencias = LeerOpcional(ruta, Constantes.ArchivoAsistencias, 5, LeerAsistencia, 4);
        GestionEstudiante.service.ValidacionDatos.Validar(datos);
        return datos;
    }

    private static Asistencia LeerAsistencia(string[] campos)
    {
        Asistencia asistencia = new Asistencia { id = Entero(campos[0]) };
        asistencia.estado = Enum.Parse<EstadoAsistencia>(campos[3]);
        if (campos.Length == 4)
        {
            if (asistencia.estado.ToString() != campos[3])
                throw new FormatException("Se esperaba Presente, Falta o Tardanza.");
            asistencia.codigoEstudiante = campos[1];
            asistencia.fecha = DateTime.ParseExact(campos[2], Constantes.FormatoFechaAsistencia,
                CultureInfo.InvariantCulture);
        }
        else
        {
            asistencia.matriculaId = Entero(campos[1]);
            asistencia.fecha = Fecha(campos[2]);
            asistencia.observacion = campos[4];
        }
        return asistencia;
    }

    private static List<T> LeerArchivo<T>(
        string ruta, string archivo, int columnas,
        Func<int, string[], HashSet<int>, HashSet<string>, T> funcion, int? columnasAlternativas = null)
    {
        string[] filas = File.ReadAllLines(Path.Combine(ruta, archivo), Utf8);
        HashSet<int> ids = new HashSet<int>();
        HashSet<string> codigos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<T> entidades = new List<T>();

        for (int i = 0; i < filas.Length; i++)
        {
            string[] campos = LeerCampos(filas[i], archivo, i + 1, columnas, columnasAlternativas);
            entidades.Add(funcion(i, campos, ids, codigos));
        }

        return entidades;
    }

    public void Guardar<T>(string archivo, List<T> registros, Func<T, string[]> obtenerCampos)
    {
        StringBuilder texto = new StringBuilder();
        foreach (T registro in registros)
        {
            texto.AppendLine(FormatoTxt.CrearFila(obtenerCampos(registro)));
        }

        GuardarContenido(archivo, texto.ToString());
    }

    private void GuardarContenido(string archivo, string contenido)
    {
        if (Existe())
        {
            Escribir(Path.Combine(Ruta, archivo), contenido);
            return;
        }

        if (carpetaInicializada)
            throw new IOException("La carpeta activa ya no está disponible. Recupérela o cree un respaldo de la sesión antes de continuar.");

        string temporal = RutaTemporal(Ruta);
        try
        {
            foreach (string nombre in Archivos)
                Escribir(Path.Combine(temporal, nombre), nombre == archivo ? contenido : "");
            Directory.Move(temporal, Ruta);
            carpetaInicializada = true;
        }
        finally
        {
            LimpiarTemporal(temporal);
        }
    }

    private void PrepararTraslado(string destino)
    {
        string temporal = RutaTemporal(destino);
        try
        {
            CopiarConjunto(Ruta, temporal);
            Directory.Move(temporal, destino);
        }
        finally
        {
            LimpiarTemporal(temporal);
        }
    }

    public void Mover(string destino)
    {
        destino = Normalizar(destino);
        ValidarDestino(destino);
        if (!Existe())
        {
            GuardarContenido(Constantes.ArchivoEstudiantes, string.Empty);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
        string origen = Ruta;
        if (!Path.GetPathRoot(origen)!.Equals(Path.GetPathRoot(destino), StringComparison.OrdinalIgnoreCase))
        {
            TrasladarEntreUnidades(origen, destino);
            return;
        }

        Directory.Move(origen, destino);
        try
        {
            Usar(destino);
        }
        catch
        {
            Directory.Move(destino, origen);
            throw;
        }
    }

    private void TrasladarEntreUnidades(string origen, string destino)
    {
        ValidarCarpetaDedicada(origen);
        PrepararTraslado(destino);
        string anterior = RutaTemporal(origen);
        try
        {
            Directory.Move(origen, anterior);
            try
            {
                Usar(destino);
            }
            catch
            {
                Directory.Move(anterior, origen);
                throw;
            }
        }
        catch
        {
            LimpiarTemporal(destino);
            throw;
        }

        LimpiarTemporal(anterior);
    }

    private static void ValidarCarpetaDedicada(string ruta)
    {
        if (Directory.GetDirectories(ruta).Length != 0)
        {
            throw new IOException("La carpeta activa contiene subcarpetas. Use una carpeta dedicada a los datos.");
        }

        foreach (string archivo in Directory.GetFiles(ruta))
        {
            if (!Path.GetExtension(archivo).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("La carpeta activa contiene otros archivos. Use una carpeta dedicada a los datos.");
            }
        }
    }

    private void ValidarDestino(string destino)
    {
        if (Relacionadas(Ruta, destino) || Directory.Exists(destino) || File.Exists(destino))
        {
            throw new ArgumentException("Elija una carpeta nueva fuera de la carpeta de datos activa.");
        }
    }

    private void CopiarConjunto(string origen, string destino)
    {
        Directory.CreateDirectory(destino);
        foreach (string ruta in Directory.GetFiles(origen, "*.txt"))
        {
            File.Copy(ruta, Path.Combine(destino, Path.GetFileName(ruta)));
        }

        Leer(destino);
    }

    private static bool Relacionadas(string primera, string segunda)
    {
        return primera.Equals(segunda, StringComparison.OrdinalIgnoreCase)
            || primera.StartsWith(Path.EndsInDirectorySeparator(segunda) ? segunda : segunda + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase)
            || segunda.StartsWith(Path.EndsInDirectorySeparator(primera) ? primera : primera + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase);
    }

    private static string Normalizar(string ruta)
    {
        return Path.TrimEndingDirectorySeparator(Path.GetFullPath(ruta));
    }

    private static string RutaTemporal(string ruta)
    {
        return ruta + "." + Guid.NewGuid().ToString("N") + ".tmp";
    }

    private static void LimpiarTemporal(string ruta)
    {
        try
        {
            if (Directory.Exists(ruta))
            {
                foreach (string archivo in Directory.GetFiles(ruta))
                {
                    File.Delete(archivo);
                }
                Directory.Delete(ruta);
            }
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }

    private static string[] LeerCampos(string linea, string archivo, int numero, int cantidad, int? cantidadAlternativa)
    {
        string[] campos = linea.Split(Constantes.Separador);
        if (campos.Length != cantidad && campos.Length != cantidadAlternativa)
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
                caracter = campo[i] switch
                {
                    '\\' => '\\', 'p' => Constantes.Separador, 'r' => '\r', 'n' => '\n',
                    _ => throw ErrorFila(archivo, numero)
                };
            }
            texto.Append(caracter);
        }
        return texto.ToString();
    }

    private static bool LeerEnteroPositivo(string campo, out int valor)
    {
        return int.TryParse(campo, NumberStyles.None, CultureInfo.InvariantCulture, out valor) && valor > 0;
    }

    private static InvalidDataException ErrorFila(string archivo, int numero)
    {
        return new InvalidDataException("Registro inválido en " + archivo + ", línea " + numero + ".");
    }

    private static void Escribir(string ruta, string contenido)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);
        string temporal = RutaTemporal(ruta);
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
