using GestionEstudiante.archivos;
using GestionEstudiante.model;

namespace GestionEstudiante.repository;

// Se crea una sola vez y se comparte durante toda la ejecución de la aplicación.
public class GestorBaseDatos
{
    private readonly ArchivoBaseDatos archivo;

    public BaseDatosEscolar Datos { get; private set; }
    public string Ruta
    {
        get { return archivo.Ruta; }
    }

    public GestorBaseDatos(string? ruta = null)
    {
        archivo = new ArchivoBaseDatos(ruta);
        Datos = archivo.Existe() ? archivo.Leer(Ruta) : new BaseDatosEscolar();
    }

    public void Guardar<T>(string nombreArchivo, List<T> registros, Func<T, string[]> obtenerCampos)
    {
        archivo.Guardar(nombreArchivo, registros, obtenerCampos);
    }

    public void CargarUsar(string ruta)
    {
        BaseDatosEscolar nuevos = archivo.Leer(ruta);
        archivo.Usar(ruta);
        Datos = nuevos;
    }

    public void Mover(string destino)
    {
        archivo.Mover(destino);
    }
}
