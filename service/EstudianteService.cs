using GestionEstudiante.model;
using GestionEstudiante.repository;

namespace GestionEstudiante.service;

public class EstudianteService
{
    // El formulario pasa por este servicio para trabajar con los datos.
    // Normalizamos los textos antes de agregar o modificar un registro.
    private readonly EstudianteRepository repository = new();
    public int Capacidad
    {
        get
        {
            return EstudianteRepository.Capacidad;
        }
    }
    // Enviamos el nuevo estudiante al repositorio, donde se asignan código y fecha.
    public void Agregar(Estudiante estudiante)
    {
        NormalizarDatos(estudiante);
        repository.Agregar(estudiante);
    }
    // Actualizamos un registro existente usando su identificador.
    public void Modificar(Estudiante estudiante)
    {
        NormalizarDatos(estudiante);
        repository.Modificar(estudiante);
    }
    // Aplicamos la misma regla de almacenamiento al registrar y al editar, incluido el correo.
    // Trim quita espacios de los extremos y ToUpperInvariant convierte el texto a mayúsculas.
    private static void NormalizarDatos(Estudiante estudiante)
    {
        estudiante.codigo = estudiante.codigo.Trim().ToUpperInvariant();
        estudiante.nombre = estudiante.nombre.Trim().ToUpperInvariant();
        estudiante.carrera = estudiante.carrera.Trim().ToUpperInvariant();
        estudiante.correo = estudiante.correo.Trim().ToUpperInvariant();
        estudiante.telefono = estudiante.telefono.Trim().ToUpperInvariant();
    }
    // Indicamos qué estudiante debe retirarse del almacenamiento.
    public void Eliminar(int id)
    {
        repository.Eliminar(id);
    }
    // Pedimos los estudiantes registrados, sin incluir los espacios vacíos del arreglo.
    public Estudiante[] Listar()
    {
        return repository.Listar();
    }
    // Buscamos un estudiante concreto; el resultado puede ser null si no existe.
    public Estudiante? ObtenerPorId(int id)
    {
        return repository.ObtenerPorId(id);
    }
    // Consultamos cuántos estudiantes hay para el total y los cupos disponibles.
    public int Conteo()
    {
        return repository.Conteo();
    }
}
