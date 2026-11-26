using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.repository;

public class AsistenciaRepository
{
    private readonly GestorBaseDatos gestor;

    public AsistenciaRepository(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public List<Asistencia> Listar()
    {
        return gestor.Datos.asistencias;
    }

    public void Guardar(List<Asistencia> cambios)
    {
        if (cambios.Count == 0)
        {
            return;
        }

        List<Asistencia> registros = new List<Asistencia>(Listar());
        foreach (Asistencia cambio in cambios)
        {
            int posicion = BuscarPosicion(registros, cambio);
            if (posicion >= 0)
            {
                cambio.id = registros[posicion].id;
                registros[posicion] = cambio;
            }
            else
            {
                int id = registros.Count + 1;
                if (registros.Exists(registro => registro.id == id))
                {
                    throw new InvalidDataException("El próximo identificador de asistencia ya existe.");
                }

                cambio.id = id;
                registros.Add(cambio);
            }
        }

        gestor.Guardar(Constantes.ArchivoAsistencias, registros, registro => registro.ObtenerCampos());
        gestor.Datos.asistencias = registros;
    }

    private static int BuscarPosicion(List<Asistencia> registros, Asistencia cambio)
    {
        for (int i = 0; i < registros.Count; i++)
        {
            Asistencia registro = registros[i];
            if (registro.fecha.Date == cambio.fecha.Date
                && string.Equals(registro.codigoEstudiante, cambio.codigoEstudiante, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }
}
