using System.Text.Json;
using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.archivos;
public class ArchivoBaseDatos
{
    private readonly string? archivoUbicacion;
    public string Ruta { get; private set; }

    public ArchivoBaseDatos(string? ruta = null, string? ubicacion = null)
    {
        if (ruta == null)
        {
            string carpeta = Constantes.CarpetaDatos;
            archivoUbicacion = ubicacion;
            if (archivoUbicacion == null)
            {
                archivoUbicacion = Path.Combine(carpeta, "ubicacion.txt");
            }

            ruta = Path.Combine(carpeta, "gestion-escolar.json");
            if (File.Exists(archivoUbicacion))
            {
                string guardada = File.ReadAllText(archivoUbicacion).Trim();
                if (guardada.Length > 0)
                {
                    ruta = guardada;
                }
            }
        }

        Ruta = Path.GetFullPath(ruta);
    }

    public bool Existe()
    {
        return File.Exists(Ruta);
    }

    public void Usar(string ruta)
    {
        ruta = Path.GetFullPath(ruta);
        GuardarUbicacion(ruta);
        Ruta = ruta;
    }

    public void Mover(string destino)
    {
        destino = Path.GetFullPath(destino);
        if (destino.Equals(Ruta, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
        File.Move(Ruta, destino);
        try
        {
            GuardarUbicacion(destino);
        }
        catch
        {
            File.Move(destino, Ruta);
            throw;
        }

        Ruta = destino;
    }

    private void GuardarUbicacion(string ruta)
    {
        // Las rutas explícitas de las pruebas no cambian la ubicación del usuario.
        if (archivoUbicacion == null)
        {
            return;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(archivoUbicacion))!);
        File.WriteAllText(archivoUbicacion, ruta);
    }

    public BaseDatosEscolar Leer(string ruta)
    {
        string texto;
        using (StreamReader lector = new StreamReader(ruta))
        {
            texto = lector.ReadToEnd();
        }

        // Por ahora solo se convierte el JSON al modelo, sin revisar sus datos.
        BaseDatosEscolar? datos = JsonSerializer.Deserialize<BaseDatosEscolar>(texto);
        if (datos == null)
        {
            throw new InvalidDataException("No se pudo leer el archivo.");
        }

        return datos;
    }

    public void Guardar(string ruta, BaseDatosEscolar datos)
    {
        string original = "{}";
        if (Existe())
        {
            original = File.ReadAllText(Ruta);
        }

        // Se copian las secciones originales, sin modelos ni operaciones de otros módulos.
        string contenido = "{\n";
        string secuencias = "{";
        using (JsonDocument documento = JsonDocument.Parse(original))
        {
            foreach (JsonProperty campo in documento.RootElement.EnumerateObject())
            {
                if (campo.Name != "estudiantes" && campo.Name != "secuencias")
                {
                    contenido += JsonSerializer.Serialize(campo.Name) + ": " + campo.Value.GetRawText() + ",\n";
                }
            }

            JsonElement contadores;
            if (documento.RootElement.TryGetProperty("secuencias", out contadores))
            {
                foreach (JsonProperty contador in contadores.EnumerateObject())
                {
                    if (contador.Name != "siguienteIdEstudiante")
                    {
                        secuencias += JsonSerializer.Serialize(contador.Name) + ": " + contador.Value.GetRawText() + ",";
                    }
                }
            }
        }

        secuencias += "\"siguienteIdEstudiante\": " + datos.secuencias.siguienteIdEstudiante + "}";
        contenido += "\"secuencias\": " + secuencias + ",\n";
        contenido += "\"estudiantes\": " + JsonSerializer.Serialize(datos.estudiantes) + "\n}";
        Escribir(ruta, contenido);
    }

    public void Copiar(string origen, string destino)
    {
        // Recupera el contenido completo, incluidas las secciones de otros módulos.
        Escribir(destino, File.ReadAllText(origen));
    }

    private void Escribir(string ruta, string contenido)
    {
        ruta = Path.GetFullPath(ruta);
        Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);
        string temporal = ruta + ".tmp";
        try
        {
            using (StreamWriter escritor = new StreamWriter(temporal))
            {
                escritor.Write(contenido);
            }

            File.Move(temporal, ruta, true);
        }
        finally
        {
            if (File.Exists(temporal))
            {
                File.Delete(temporal);
            }
        }
    }

}
