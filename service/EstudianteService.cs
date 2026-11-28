using System.Net.Mail;
using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;

namespace GestionEstudiante.service;
public class EstudianteService
{
    private readonly EstudianteRepository repository;
    public EstudianteService(EstudianteRepository repository)
    {
        this.repository = repository;
    }

    public List<Estudiante> Listar()
    {
        return repository.Listar();
    }

    public string NombreEstudiante(int id)
    {
        Estudiante estudiante = BuscarPorId(id);
        return estudiante.nombre + " " + estudiante.apellidos;
    }

    private Estudiante BuscarPorId(int id)
    {
        foreach (Estudiante estudiante in Listar())
        {
            if (estudiante.id == id)
            {
                return estudiante;
            }
        }
        throw new InvalidOperationException("Estudiante no encontrado.");
    }

    // Estado: 0 = todos, 1 = activos, 2 = inactivos.
    public List<Estudiante> Buscar(string texto, int estado)
    {
        texto = texto.Trim();
        List<Estudiante> encontrados = new List<Estudiante>();
        foreach (Estudiante estudiante in repository.Listar())
        {
            bool coincide = estudiante.codigo.Contains(texto, StringComparison.OrdinalIgnoreCase) || (estudiante.nombre + " " + estudiante.apellidos).Contains(texto, StringComparison.OrdinalIgnoreCase);
            bool mostrarEstado = estado == 0 || (estado == 1 && estudiante.activo) || (estado == 2 && !estudiante.activo);
            if (coincide && mostrarEstado)
            {
                encontrados.Add(estudiante);
            }
        }

        return encontrados;
    }

    public void Guardar(Estudiante estudiante)
    {
        GuardarDatos(estudiante, estudiante.activo);
    }

    private void GuardarDatos(Estudiante estudiante, bool activo)
    {
        if (!activo && repository.TieneMatriculaActiva(estudiante.id))
            throw new InvalidOperationException("Cancele la matrícula activa antes de dar de baja al estudiante.");
        Estudiante copia = new Estudiante();
        copia.id = estudiante.id;
        copia.nombre = estudiante.nombre.Trim();
        copia.apellidos = estudiante.apellidos.Trim();
        copia.apoderado = estudiante.apoderado.Trim();
        copia.telefono = estudiante.telefono.Trim();
        copia.correo = estudiante.correo.Trim();
        copia.activo = activo;
        ValidarEstudiante(copia);
        repository.Guardar(copia);
    }

    public void DarDeBaja(Estudiante estudiante)
    {
        if (repository.TieneMatriculaActiva(estudiante.id))
            throw new InvalidOperationException("Cancele la matrícula activa antes de dar de baja al estudiante.");
        if (!estudiante.activo)
        {
            throw new InvalidOperationException("El estudiante ya está inactivo.");
        }

        GuardarDatos(estudiante, false);
    }

    public void Activar(int id)
    {
        Estudiante estudiante = BuscarPorId(id);
        if (estudiante.activo)
        {
            throw new InvalidOperationException("El estudiante ya está activo.");
        }

        repository.Guardar(new Estudiante
        {
            id = estudiante.id,
            nombre = estudiante.nombre,
            apellidos = estudiante.apellidos,
            apoderado = estudiante.apoderado,
            telefono = estudiante.telefono,
            correo = estudiante.correo,
            activo = true
        });
    }

    internal static void ValidarEstudiante(Estudiante estudiante)
    {
        if (string.IsNullOrWhiteSpace(estudiante.nombre) || string.IsNullOrWhiteSpace(estudiante.apellidos) || string.IsNullOrWhiteSpace(estudiante.apoderado))
        {
            throw new ArgumentException("Complete nombres, apellidos y nombre del apoderado.");
        }

        if (estudiante.nombre.Length > Constantes.LongitudMaximaNombres ||
            estudiante.apellidos.Length > Constantes.LongitudMaximaApellidos ||
            estudiante.apoderado.Length > Constantes.LongitudMaximaApoderado)
        {
            throw new ArgumentException($"Use hasta {Constantes.LongitudMaximaNombres} caracteres en nombres, " +
                $"{Constantes.LongitudMaximaApellidos} en apellidos y {Constantes.LongitudMaximaApoderado} en apoderado.");
        }

        if (estudiante.telefono == null || estudiante.telefono.Length != Constantes.LongitudTelefono)
        {
            throw new ArgumentException($"El teléfono del apoderado debe tener {Constantes.LongitudTelefono} dígitos.");
        }

        foreach (char digito in estudiante.telefono)
        {
            if (digito < '0' || digito > '9')
            {
                throw new ArgumentException("El teléfono solo admite dígitos.");
            }
        }

        if (estudiante.correo == null)
        {
            throw new ArgumentException("Deje el correo vacío si no lo conoce.");
        }

        if (estudiante.correo.Length > 0)
        {
            MailAddress? direccion;
            if (!MailAddress.TryCreate(estudiante.correo, out direccion) || direccion.Address != estudiante.correo)
            {
                throw new ArgumentException("Ingrese un correo válido o deje el campo vacío.");
            }
        }
    }
}
