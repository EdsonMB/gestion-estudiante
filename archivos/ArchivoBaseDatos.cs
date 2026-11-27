using System.Globalization;
using System.Text;
using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.archivos;

public partial class ArchivoBaseDatos
{
    private static readonly UTF8Encoding Utf8 = new UTF8Encoding(false, true);
    private readonly string? archivoUbicacion;
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
            id = Entero(c[0]), nivel = c[1], grado = Entero(c[2]), seccion = c[3],
            anio = Entero(c[4]), capacidad = Entero(c[5]), activo = bool.Parse(c[6])
        });
        datos.matriculas = LeerOpcional<Matricula>(ruta, Constantes.ArchivoMatriculas, 5, c => new Matricula
        {
            id = Entero(c[0]), estudianteId = Entero(c[1]), aulaId = Entero(c[2]),
            fecha = Fecha(c[3]), fechaCancelacion = c[4] == "" ? null : Fecha(c[4])
        });
        datos.asistencias = LeerOpcional<Asistencia>(ruta, Constantes.ArchivoAsistencias, 5, c => new Asistencia
        {
            id = Entero(c[0]), matriculaId = Entero(c[1]), fecha = Fecha(c[2]),
            estado = Enum.Parse<EstadoAsistencia>(c[3]), observacion = c[4]
        });
        GestionEstudiante.service.ValidacionDatos.Validar(datos);
        return datos;
    }

    private static List<T> LeerArchivo<T>(
        string ruta, string archivo, int columnas,
        Func<int, string[], HashSet<int>, HashSet<string>, T> funcion)
    {
        string[] filas = File.ReadAllLines(Path.Combine(ruta, archivo), Utf8);
        HashSet<int> ids = new HashSet<int>();
        HashSet<string> codigos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<T> entidades = new List<T>();

        for (int i = 0; i < filas.Length; i++)
        {
            string[] campos = LeerCampos(filas[i], archivo, i + 1, columnas);
            entidades.Add(funcion(i, campos, ids, codigos));
        }

        return entidades;
    }

    public void Guardar<T>(string archivo, List<T> registros, Func<T, string[]> obtenerCampos)
    {
        StringBuilder texto = new StringBuilder();
        foreach (T registro in registros)
        {
            texto.AppendLine(Fila(obtenerCampos(registro)));
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

        string temporal = RutaTemporal(Ruta);
        try
        {
            foreach (string nombre in Archivos)
                Escribir(Path.Combine(temporal, nombre), nombre == archivo ? contenido : "");
            Directory.Move(temporal, Ruta);
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

    private static string Fila(params string[] campos)
    {
        string[] codificados = new string[campos.Length];
        for (int i = 0; i < campos.Length; i++)
        {
            codificados[i] = campos[i].Replace("\\", "\\\\").Replace(Constantes.Separador.ToString(), "\\p")
                .Replace("\r", "\\r").Replace("\n", "\\n");
        }

        return string.Join(Constantes.Separador, codificados);
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
            StringBuilder valor = new StringBuilder();
            for (int j = 0; j < campos[i].Length; j++)
            {
                char c = campos[i][j];
                if (c != '\\') { valor.Append(c); continue; }
                if (++j >= campos[i].Length) throw ErrorFila(archivo, numero);
                valor.Append(campos[i][j] switch
                {
                    '\\' => '\\', 'p' => '|', 'r' => '\r', 'n' => '\n',
                    _ => throw ErrorFila(archivo, numero)
                });
            }
            campos[i] = valor.ToString();
        }
        return campos;
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
