using GestionEstudiante.repository;

namespace GestionEstudiante.service;

public class ArchivoService
{
    private readonly GestorBaseDatos gestor;

    public ArchivoService(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public string Ruta
    {
        get
        {
            return gestor.Ruta;
        }
    }

    public string ObtenerResumenArchivo()
    {
        return gestor.Datos.estudiantes.Length + " estudiantes";
    }

    public void CrearCopia(string ruta)
    {
        if (Path.GetFullPath(ruta).Equals(Path.GetFullPath(Ruta), StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Elija una ubicación distinta al archivo principal.");
        }

        gestor.CrearCopia(ruta);
    }

    public void CargarUsar(string ruta)
    {
        gestor.CargarUsar(ruta);
    }

    public void MoverArchivo(string destino)
    {
        gestor.Mover(destino);
    }

    public void Recuperar(string ruta)
    {
        gestor.Recuperar(ruta);
    }

}
