using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.repository;

public class MatriculaRepository
{
    private readonly GestorBaseDatos gestor;

    public MatriculaRepository(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public List<Matricula> Listar()
    {
        return new List<Matricula>(gestor.Datos.matriculas);
    }

    public void Guardar(Matricula matricula)
    {
        List<Matricula> registros = Listar();
        if (matricula.id == 0)
        {
            int id = registros.Count + 1;
            if (BuscarPosicion(registros, id) >= 0)
            {
                throw new InvalidDataException("El próximo identificador de matrícula ya existe.");
            }

            matricula.id = id;
            registros.Add(matricula);
        }
        else
        {
            int posicion = BuscarPosicion(registros, matricula.id);
            if (posicion < 0)
            {
                throw new InvalidOperationException("Matrícula no encontrada.");
            }
            registros[posicion] = matricula;
        }

        gestor.Guardar(Constantes.ArchivoMatriculas, registros);
        gestor.Datos.matriculas = registros;
    }

    private static int BuscarPosicion(List<Matricula> registros, int id)
    {
        for (int i = 0; i < registros.Count; i++)
        {
            if (registros[i].id == id) return i;
        }
        return -1;
    }
}
