using GestionEstudiante.model;
using GestionEstudiante.repository;

namespace GestionEstudiante.service;

public class ArchivoService
{
    private readonly EstudianteRepository repository;

    public ArchivoService(EstudianteRepository repository)
    {
        this.repository = repository;
    }

    public string Ruta
    {
        get
        {
            return repository.Ruta;
        }
    }

    public string ObtenerResumenArchivo()
    {
        return repository.Listar().Length + " estudiantes";
    }

    public void CrearCopia(string ruta)
    {
        if (Path.GetFullPath(ruta).Equals(Path.GetFullPath(Ruta), StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Elija una ubicación distinta al archivo principal.");
        }

        repository.CrearCopia(ruta);
    }

    public void CargarUsar(string ruta)
    {
        BaseDatosEscolar datos = repository.LeerArchivo(ruta);
        repository.Usar(ruta, datos);
    }

    public void MoverArchivo(string destino)
    {
        repository.Mover(destino);
    }

    public void Recuperar(string ruta)
    {
        BaseDatosEscolar datos = repository.LeerArchivo(ruta);
        repository.Recuperar(ruta, datos);
    }

}
