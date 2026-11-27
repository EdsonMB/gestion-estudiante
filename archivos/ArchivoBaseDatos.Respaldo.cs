using System.Globalization;
using System.IO.Compression;
using GestionEstudiante.model;
using GestionEstudiante.util;
namespace GestionEstudiante.archivos;

public partial class ArchivoBaseDatos
{
    private static readonly string[] Archivos = { Constantes.ArchivoEstudiantes, Constantes.ArchivoAulas,
        Constantes.ArchivoMatriculas, Constantes.ArchivoAsistencias };

    private static int Entero(string texto)
    {
        if (!LeerEnteroPositivo(texto, out int valor)) throw new FormatException("Se esperaba un entero positivo.");
        return valor;
    }

    private static DateTime Fecha(string texto) => DateTime.ParseExact(texto,
        Constantes.FormatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

    private static List<T> LeerOpcional<T>(string ruta, string archivo, int columnas, Func<string[], T> convertir)
        where T : ModeloBase
    {
        // Compatibilidad con carpetas anteriores que solo tenían estudiantes.txt.
        string rutaArchivo = Path.Combine(ruta, archivo);
        if (Directory.Exists(rutaArchivo))
            throw new InvalidDataException("Se esperaba un archivo TXT, pero se encontró una carpeta: " + archivo);
        if (!File.Exists(rutaArchivo)) return new List<T>();
        return LeerArchivo(ruta, archivo, columnas, (indice, campos, ids, codigos) =>
        {
            try
            {
                T valor = convertir(campos);
                if (!ids.Add(valor.id)) throw ErrorFila(archivo, indice + 1);
                return valor;
            }
            catch (Exception error) when (error is FormatException or ArgumentException or OverflowException)
            {
                throw new InvalidDataException($"Registro inválido en {archivo}, línea {indice + 1}.", error);
            }
        });
    }

    public void CrearRespaldo(string destino, BaseDatosEscolar datos)
    {
        string carpeta = Path.Combine(Path.GetTempPath(), "gestion-" + Guid.NewGuid().ToString("N"));
        string temporal = Path.GetFullPath(destino) + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            ArchivoBaseDatos copia = new ArchivoBaseDatos(carpeta);
            copia.Guardar(Constantes.ArchivoEstudiantes, datos.estudiantes, x => x.ObtenerCampos());
            copia.Guardar(Constantes.ArchivoAulas, datos.aulas, x => x.ObtenerCampos());
            copia.Guardar(Constantes.ArchivoMatriculas, datos.matriculas, x => x.ObtenerCampos());
            copia.Guardar(Constantes.ArchivoAsistencias, datos.asistencias, x => x.ObtenerCampos());
            copia.Leer(carpeta);
            ZipFile.CreateFromDirectory(carpeta, temporal);
            File.Move(temporal, destino, true);
        }
        finally
        {
            LimpiarTemporal(carpeta);
            File.Delete(temporal);
        }
    }

    public BaseDatosEscolar RecuperarRespaldo(string origen)
    {
        // Se recupera en otra carpeta: los datos anteriores permanecen intactos.
        string destino = Path.Combine(Path.GetDirectoryName(Ruta)!, "recuperado-" + Guid.NewGuid().ToString("N"));
        bool usado = false;
        try
        {
            using (ZipArchive zip = ZipFile.OpenRead(origen))
            {
                if (zip.Entries.Count != Archivos.Length ||
                    zip.Entries.Select(x => x.FullName).Distinct().Count() != Archivos.Length ||
                    zip.Entries.Any(x => !Archivos.Contains(x.FullName) || x.Length > 50_000_000))
                    throw new InvalidDataException("El respaldo debe contener los cuatro archivos TXT del sistema.");
                Directory.CreateDirectory(destino);
                foreach (ZipArchiveEntry entrada in zip.Entries)
                    entrada.ExtractToFile(Path.Combine(destino, entrada.FullName));
            }
            BaseDatosEscolar datos = Leer(destino);
            Usar(destino);
            usado = true;
            return datos;
        }
        finally
        {
            if (!usado) LimpiarTemporal(destino);
        }
    }
}
