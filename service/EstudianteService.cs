using System.Net.Mail;
using GestionEstudiante.model;
using GestionEstudiante.repository;

namespace GestionEstudiante.service;
public class EstudianteService
{
    private readonly EstudianteRepository repository;
    public EstudianteService(EstudianteRepository repository)
    {
        this.repository = repository;
    }

    public Estudiante[] Listar()
    {
        return repository.Listar();
    }

    // Estado: 0 = todos, 1 = activos, 2 = inactivos.
    public Estudiante[] Buscar(string texto, int estado)
    {
        texto = texto.Trim();
        Estudiante[] estudiantes = repository.Listar();
        Estudiante[] encontrados = new Estudiante[estudiantes.Length];
        int cantidad = 0;
        foreach (Estudiante estudiante in estudiantes)
        {
            bool coincide = estudiante.codigo.Contains(texto, StringComparison.OrdinalIgnoreCase) || (estudiante.nombre + " " + estudiante.apellidos).Contains(texto, StringComparison.OrdinalIgnoreCase);
            bool mostrarEstado = estado == 0 || (estado == 1 && estudiante.activo) || (estado == 2 && !estudiante.activo);
            if (coincide && mostrarEstado)
            {
                encontrados[cantidad] = estudiante;
                cantidad++;
            }
        }

        Estudiante[] resultado = new Estudiante[cantidad];
        Array.Copy(encontrados, resultado, cantidad);
        return resultado;
    }

    public void Guardar(Estudiante estudiante)
    {
        Estudiante copia = new Estudiante();
        copia.id = estudiante.id;
        copia.nombre = estudiante.nombre.Trim();
        copia.apellidos = estudiante.apellidos.Trim();
        copia.apoderado = estudiante.apoderado.Trim();
        copia.telefono = estudiante.telefono.Trim();
        copia.correo = estudiante.correo.Trim();
        copia.activo = estudiante.activo;
        ValidarEstudiante(copia);
        repository.Guardar(copia);
    }

    public void DarDeBaja(Estudiante estudiante)
    {
        // TODO: comprobar matrícula activa cuando se implemente el módulo de matrículas.
        if (!estudiante.activo)
        {
            throw new InvalidOperationException("El estudiante ya está inactivo.");
        }

        Estudiante copia = new Estudiante();
        copia.id = estudiante.id;
        copia.nombre = estudiante.nombre;
        copia.apellidos = estudiante.apellidos;
        copia.apoderado = estudiante.apoderado;
        copia.telefono = estudiante.telefono;
        copia.correo = estudiante.correo;
        copia.activo = false;
        Guardar(copia);
    }

    private static void ValidarEstudiante(Estudiante estudiante)
    {
        if (string.IsNullOrWhiteSpace(estudiante.nombre) || string.IsNullOrWhiteSpace(estudiante.apellidos) || string.IsNullOrWhiteSpace(estudiante.apoderado))
        {
            throw new ArgumentException("Complete nombres, apellidos y nombre del apoderado.");
        }

        if (estudiante.nombre.Length > 80 || estudiante.apellidos.Length > 100 || estudiante.apoderado.Length > 120)
        {
            throw new ArgumentException("Use hasta 80 caracteres en nombres, 100 en apellidos y 120 en apoderado.");
        }

        if (estudiante.telefono == null || estudiante.telefono.Length != 9)
        {
            throw new ArgumentException("El teléfono del apoderado debe tener 9 dígitos.");
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
