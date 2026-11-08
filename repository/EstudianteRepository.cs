using GestionEstudiante.model;

namespace GestionEstudiante.repository;
public class EstudianteRepository
{
    private readonly GestorBaseDatos gestor;

    public EstudianteRepository(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
    }

    public Estudiante[] Listar()
    {
        return gestor.Datos.estudiantes;
    }

    public void Guardar(Estudiante estudiante)
    {
        BaseDatosEscolar datos = gestor.Datos;
        int posicion = -1;
        for (int i = 0; i < datos.estudiantes.Length; i++)
        {
            if (datos.estudiantes[i].id == estudiante.id)
            {
                posicion = i;
                break;
            }
        }

        bool nuevo = estudiante.id == 0;
        if (!nuevo && posicion < 0)
        {
            throw new InvalidOperationException("Estudiante no encontrado.");
        }

        Estudiante[] registros = new Estudiante[datos.estudiantes.Length + (nuevo ? 1 : 0)];
        Array.Copy(datos.estudiantes, registros, datos.estudiantes.Length);
        // El servicio entrega un objeto nuevo, independiente del registro anterior.
        if (nuevo)
        {
            estudiante.id = datos.secuencias.siguienteIdEstudiante;
            estudiante.codigo = "E" + estudiante.id.ToString("000000");
            estudiante.fechaCreacion = DateTime.Now;
            estudiante.activo = true;
            posicion = registros.Length - 1;
        }
        else
        {
            estudiante.codigo = datos.estudiantes[posicion].codigo;
            estudiante.fechaCreacion = datos.estudiantes[posicion].fechaCreacion;
        }

        registros[posicion] = estudiante;
        BaseDatosEscolar nuevos = new BaseDatosEscolar();
        nuevos.estudiantes = registros;
        nuevos.secuencias.siguienteIdEstudiante = datos.secuencias.siguienteIdEstudiante;
        if (nuevo)
        {
            nuevos.secuencias.siguienteIdEstudiante++;
        }

        gestor.Guardar(nuevos);
    }
}
