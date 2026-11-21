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
        return gestor.Datos.estudiantes.Count + " estudiantes";
    }

    public void CargarUsar(string ruta)
    {
        gestor.CargarUsar(ruta);
    }

    public void MoverArchivo(string destino)
    {
        gestor.Mover(destino);
    }

}
