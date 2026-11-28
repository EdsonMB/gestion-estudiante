using GestionEstudiante.archivos;
using GestionEstudiante.model;
using GestionEstudiante.service;

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
        Datos = ruta == null ? new BaseDatosEscolar() : ValidarDatos(archivo.Leer(Ruta));
    }

    public bool CargarUbicacionGuardada()
    {
        string? ruta = archivo.LeerUbicacionGuardada();
        if (ruta == null)
        {
            return false;
        }

        BaseDatosEscolar nuevos = ValidarDatos(archivo.Leer(ruta));
        archivo.Usar(ruta, recordar: false);
        Datos = nuevos;
        return true;
    }

    public void Guardar<T>(string nombreArchivo, List<T> registros, Func<T, string[]> obtenerCampos)
    {
        archivo.Guardar(nombreArchivo, registros, obtenerCampos);
    }

    public void CargarUsar(string ruta)
    {
        BaseDatosEscolar nuevos = ValidarDatos(archivo.AbrirSeleccion(ruta));
        archivo.Usar(ruta, recordar: true);
        Datos = nuevos;
    }

    private static BaseDatosEscolar ValidarDatos(BaseDatosEscolar datos)
    {
        try
        {
            foreach (Estudiante estudiante in datos.estudiantes)
            {
                EstudianteService.ValidarEstudiante(estudiante);
            }
            AulaService.ValidarRegistros(datos.aulas, datos.matriculas);
            MatriculaService.ValidarRegistros(datos.matriculas, datos.estudiantes, datos.aulas);
            AsistenciaService.ValidarRegistros(datos.asistencias, datos.matriculas);
        }
        catch (ArgumentException error)
        {
            throw new InvalidDataException("La carpeta contiene datos inconsistentes: " + error.Message, error);
        }

        return datos;
    }
}
