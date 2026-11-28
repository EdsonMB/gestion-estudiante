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
            if (registros.Exists(registro => registro.id == id))
            {
                throw new InvalidDataException("El próximo identificador de aula ya existe.");
            }

            aula.id = id;
            registros.Add(aula);
        }
        else
        {
            int posicion = registros.FindIndex(registro => registro.id == aula.id);
            if (posicion < 0)
            {
                throw new InvalidOperationException("Aula no encontrada.");
            }
            registros[posicion] = aula;
        }

        gestor.Guardar(Constantes.ArchivoAulas, registros, registro => registro.ObtenerCampos());
        gestor.Datos.aulas = registros;
    }
}
