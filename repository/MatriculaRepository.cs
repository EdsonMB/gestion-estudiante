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
            if (registros.Exists(registro => registro.id == id))
            {
                throw new InvalidDataException("El próximo identificador de matrícula ya existe.");
            }

            matricula.id = id;
            registros.Add(matricula);
        }
        else
        {
            int posicion = registros.FindIndex(registro => registro.id == matricula.id);
            if (posicion < 0)
            {
                throw new InvalidOperationException("Matrícula no encontrada.");
            }
            registros[posicion] = matricula;
        }

        gestor.Guardar(Constantes.ArchivoMatriculas, registros, registro => registro.ObtenerCampos());
        gestor.Datos.matriculas = registros;
    }
}
