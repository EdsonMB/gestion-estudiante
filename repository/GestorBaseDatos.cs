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
        get
        {
            return archivo.Ruta;
        }
    }

    public GestorBaseDatos(string? ruta = null)
    {
        archivo = new ArchivoBaseDatos(ruta);
        Datos = new BaseDatosEscolar();
        if (archivo.Existe())
        {
            Datos = archivo.Leer(Ruta);
        }
    }

    public void Guardar(BaseDatosEscolar nuevos)
    {
        archivo.Guardar(Ruta, nuevos);
        Datos = nuevos;
    }

    public void CrearCopia(string ruta)
    {
        archivo.Guardar(ruta, Datos);
    }

    public void CargarUsar(string ruta)
    {
        BaseDatosEscolar nuevos = archivo.Leer(ruta);
        archivo.Usar(ruta);
        Datos = nuevos;
    }

    public void Mover(string destino)
    {
        if (!archivo.Existe())
        {
            archivo.Guardar(Ruta, Datos);
        }

        archivo.Mover(destino);
    }

    public void Recuperar(string ruta)
    {
        BaseDatosEscolar nuevos = archivo.Leer(ruta);
        archivo.Copiar(ruta, Ruta);
        Datos = nuevos;
    }
}
