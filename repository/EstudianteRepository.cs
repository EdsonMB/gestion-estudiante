using GestionEstudiante.model;

namespace GestionEstudiante.repository;

public class EstudianteRepository
{
    public const int Capacidad = 10;
    // Cada posición guarda un estudiante; null representa un espacio disponible.
    // El arreglo vive en memoria: sus datos se pierden al cerrar la aplicación.
    private readonly Estudiante?[] estudiantes = new Estudiante?[Capacidad];
    private int siguienteId = 1;

    // Buscamos un lugar libre en el arreglo y asignamos los datos del nuevo registro.
    // El ID sigue aumentando aunque eliminemos estudiantes, para no repetir códigos.
    public void Agregar(Estudiante estudiante)
    {
        int posicion = ObtenerPosicion();
        if (posicion < 0)
        {
            // Si el arreglo está lleno, no intentamos guardar fuera de sus límites.
            return;
        }
        estudiante.id = siguienteId++;
        estudiante.codigo = GenerarCodigo(estudiante.id);
        estudiante.fechaCreacion = DateTime.Now;
        estudiantes[posicion] = estudiante;
    }

    // Buscamos el primer espacio libre; -1 indica que el arreglo está lleno.
    private int ObtenerPosicion()
    {
        for (int i = 0; i < estudiantes.Length; i++)
        {
            if (estudiantes[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    // Formamos el código con el ID y seis dígitos, por ejemplo E000001.
    private string GenerarCodigo(int id)
    {
        return "E" + id.ToString("000000");
    }

    // Localizamos por ID, porque la posición en el arreglo no es el identificador.
    // Conservamos la fecha original: editar no equivale a crear de nuevo.
    public void Modificar(Estudiante estudiante)
    {
        int posicion = BuscarPosicionPorId(estudiante.id);
        if (posicion < 0)
        {
            return;
        }
        estudiante.fechaCreacion = estudiantes[posicion]!.fechaCreacion;
        estudiantes[posicion] = estudiante;
    }

    // Quitamos al estudiante y juntamos los registros para no dejar huecos entre ellos.
    public void Eliminar(int id)
    {
        int posicion = BuscarPosicionPorId(id);
        if (posicion < 0)
        {
            return;
        }
        estudiantes[posicion] = null;
        ReOrdenar();
    }

    // Movemos los espacios vacíos al final intercambiando posiciones vecinas.
    // Los estudiantes conservan sus IDs aunque cambien de posición en el arreglo.
    private void ReOrdenar()
    {
        for (int i = 0; i < estudiantes.Length - 1; i++)
        {
            for (int j = 0; j < estudiantes.Length - 1 - i; j++)
            {
                if (estudiantes[j] == null && estudiantes[j + 1] != null)
                {
                    Estudiante? auxiliar = estudiantes[j];
                    estudiantes[j] = estudiantes[j + 1];
                    estudiantes[j + 1] = auxiliar;
                }
            }
        }
    }

    // Recorremos el arreglo hasta encontrar el ID; -1 significa que no existe.
    private int BuscarPosicionPorId(int id)
    {
        for (int i = 0; i < estudiantes.Length; i++)
        {
            Estudiante? actual = estudiantes[i];
            if (actual != null && actual.id == id)
            {
                return i;
            }
        }
        return -1;
    }

    // Devolvemos el estudiante encontrado o null cuando el ID no existe.
    public Estudiante? ObtenerPorId(int id)
    {
        int posicion = BuscarPosicionPorId(id);
        if (posicion == -1)
        {
            return null;
        }
        return estudiantes[posicion];
    }

    // Copiamos los registros ocupados en el orden en que están guardados.
    // ReOrdenar se encarga de juntar los registros después de eliminar.
    public Estudiante[] Listar()
    {
        Estudiante[] listado = new Estudiante[Conteo()];
        int posicion = 0;
        for (int i = 0; i < estudiantes.Length; i++)
        {
            Estudiante? actual = estudiantes[i];
            if (actual != null)
            {
                listado[posicion] = actual;
                posicion++;
            }
        }
        return listado;
    }

    // Contamos registros ocupados, no el tamaño total del arreglo.
    public int Conteo()
    {
        int cantidad = 0;
        for (int i = 0; i < estudiantes.Length; i++)
        {
            if (estudiantes[i] != null)
            {
                cantidad++;
            }
        }
        return cantidad;
    }
}
