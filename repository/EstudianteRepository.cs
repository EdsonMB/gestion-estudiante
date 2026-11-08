using GestionEstudiante.archivos;
using GestionEstudiante.model;

namespace GestionEstudiante.repository;
public class EstudianteRepository
{
    private readonly ArchivoBaseDatos archivo;
    private BaseDatosEscolar datos;
    public string Ruta
    {
        get
        {
            return archivo.Ruta;
        }
    }

    public EstudianteRepository(string? ruta = null)
    {
        datos = new BaseDatosEscolar();
        archivo = new ArchivoBaseDatos(ruta);
        if (archivo.Existe())
        {
            datos = archivo.Leer(Ruta);
        }
    }

    public Estudiante[] Listar()
    {
        return datos.estudiantes;
    }

    public BaseDatosEscolar LeerArchivo(string ruta)
    {
        return archivo.Leer(ruta);
    }

    public void Guardar(Estudiante estudiante)
    {
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

        archivo.Guardar(Ruta, nuevos);
        datos = nuevos;
    }

    public void GuardarArchivo()
    {
        archivo.Guardar(Ruta, datos);
    }

    public void CrearCopia(string ruta)
    {
        archivo.Guardar(ruta, datos);
    }

    public void Usar(string ruta, BaseDatosEscolar nuevos)
    {
        archivo.Usar(ruta);
        datos = nuevos;
    }

    public void Mover(string destino)
    {
        if (!archivo.Existe())
        {
            GuardarArchivo();
        }

        archivo.Mover(destino);
    }

    public void Recuperar(string ruta, BaseDatosEscolar nuevos)
    {
        archivo.Copiar(ruta, Ruta);
        datos = nuevos;
    }
}
