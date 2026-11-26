using GestionEstudiante.model;
using GestionEstudiante.repository;

namespace GestionEstudiante.service;

public class AsistenciaService
{
    private readonly AsistenciaRepository repository;

    public AsistenciaService(AsistenciaRepository repository)
    {
        this.repository = repository;
    }

    public List<Asistencia> Listar()
    {
        return repository.Listar();
    }

    public void Guardar(List<Asistencia> cambios)
    {
        List<Asistencia> registros = new List<Asistencia>();
        foreach (Asistencia item in cambios)
        {
            string codigo = item.codigoEstudiante;
            string estado = item.estado.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("El código del estudiante es obligatorio.");
            }

            if (!Asistencia.EsEstadoValido(estado))
            {
                throw new ArgumentException("Seleccione un estado válido: Presente, Falta o Tardanza.");
            }

            registros.Add(new Asistencia
            {
                codigoEstudiante = codigo,
                fecha = item.fecha.Date,
                estado = estado
            });
        }

        repository.Guardar(registros);
    }
}
