using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.repository;

public class AulaRepository
{
    private readonly GestorBaseDatos gestor;

    public AulaRepository(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public List<Aula> Listar()
    {
        return new List<Aula>(gestor.Datos.aulas);
    }

    public void Guardar(Aula aula)
    {
        List<Aula> registros = Listar();
        if (aula.id == 0)
        {
            int id = registros.Count + 1;
            if (BuscarPosicion(registros, id) >= 0)
            {
                throw new InvalidDataException("El próximo identificador de aula ya existe.");
            }

            aula.id = id;
            registros.Add(aula);
        }
        else
        {
            int posicion = BuscarPosicion(registros, aula.id);
            if (posicion < 0)
            {
                throw new InvalidOperationException("Aula no encontrada.");
            }
            registros[posicion] = aula;
        }

        gestor.Guardar(Constantes.ArchivoAulas, registros);
        gestor.Datos.aulas = registros;
    }

    private static int BuscarPosicion(List<Aula> registros, int id)
    {
        for (int i = 0; i < registros.Count; i++)
        {
            if (registros[i].id == id) return i;
        }
        return -1;
    }
}
