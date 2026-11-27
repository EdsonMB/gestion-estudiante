using System.Globalization;
using GestionEstudiante.model;
using GestionEstudiante.util;

namespace GestionEstudiante.repository;

public class EstudianteRepository
{
    private readonly GestorBaseDatos gestor;

    public EstudianteRepository(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public List<Estudiante> Listar()
    {
        return gestor.Datos.estudiantes;
    }

    public bool TieneMatriculaActiva(int estudianteId) =>
        gestor.Datos.matriculas.Any(x => x.estudianteId == estudianteId && x.Activa);

    public void Guardar(Estudiante estudiante)
    {
        // Se publica la lista nueva solo cuando se guarda el archivo.
        List<Estudiante> registros = new List<Estudiante>(gestor.Datos.estudiantes);
        if (estudiante.id == 0)
        {
            int id = registros.Count + 1;
            string codigo = Constantes.PrefijoCodigoEstudiante + id.ToString(
                Constantes.FormatoCodigoEstudiante, CultureInfo.InvariantCulture);
            foreach (Estudiante existente in registros)
            {
                if (existente.id == id || existente.codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("El próximo identificador o código ya existe en el archivo de estudiantes.");
                }
            }

            estudiante.id = id;
            estudiante.codigo = codigo;
            estudiante.fechaCreacion = DateTime.Now;
            estudiante.activo = true;
            registros.Add(estudiante);
        }
        else
        {
            int posicion = BuscarPosicion(registros, estudiante.id);
            Estudiante anterior = registros[posicion];
            estudiante.codigo = anterior.codigo;
            estudiante.fechaCreacion = anterior.fechaCreacion;
            registros[posicion] = estudiante;
        }

        gestor.Guardar(Constantes.ArchivoEstudiantes, registros, ObtenerCampos);
        gestor.Datos.estudiantes = registros;
    }

    private static string[] ObtenerCampos(Estudiante estudiante)
    {
        return estudiante.ObtenerCampos();
    }

    private static int BuscarPosicion(List<Estudiante> estudiantes, int id)
    {
        for (int i = 0; i < estudiantes.Count; i++)
        {
            if (estudiantes[i].id == id)
            {
                return i;
            }
        }

        throw new InvalidOperationException("Estudiante no encontrado.");
    }
}
