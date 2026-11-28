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
        return new List<Asistencia>(gestor.Datos.asistencias);
    }

    public void Guardar(List<Asistencia> cambios, List<Asistencia> bajas)
    {
        if (cambios.Count == 0 && bajas.Count == 0)
        {
            return;
        }

        List<Asistencia> registros = Listar();
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
                // Las marcas retiradas dejan huecos en los identificadores.
                while (registros.Exists(registro => registro.id == id))
                {
                    id = checked(id + 1);
                }
                cambio.id = id;
                registros.Add(cambio);
            }
        }

        foreach (Asistencia baja in bajas)
        {
            int posicion = BuscarPosicion(registros, baja);
            if (posicion >= 0)
            {
                registros.RemoveAt(posicion);
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
            if (registro.fecha.Date == cambio.fecha.Date && registro.matriculaId == cambio.matriculaId)
            {
                return i;
            }
        }

        return -1;
    }
}
