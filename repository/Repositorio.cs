using GestionEstudiante.model;
namespace GestionEstudiante.repository;

// Una sola implementación de persistencia para las entidades que heredan ModeloBase.
// Los selectores consultan los datos actuales incluso después de cargar un respaldo.
public class Repositorio<T> where T : ModeloBase
{
    private readonly GestorBaseDatos gestor;
    private readonly string archivo;
    private readonly Func<BaseDatosEscolar, List<T>> leer;
    private readonly Action<BaseDatosEscolar, List<T>> publicar;

    public Repositorio(GestorBaseDatos gestor, string archivo,
        Func<BaseDatosEscolar, List<T>> leer, Action<BaseDatosEscolar, List<T>> publicar)
    {
        this.gestor = gestor;
        this.archivo = archivo;
        this.leer = leer;
        this.publicar = publicar;
    }

    public List<T> Listar() => leer(gestor.Datos).ToList();
    public int SiguienteId() => checked(leer(gestor.Datos).Select(x => x.id).DefaultIfEmpty(0).Max() + 1);

    public void Guardar(T registro)
    {
        List<T> nuevos = Listar();
        if (registro.id == 0)
        {
            registro.id = SiguienteId();
            nuevos.Add(registro);
        }
        else
        {
            int indice = nuevos.FindIndex(x => x.id == registro.id);
            if (indice < 0) throw new InvalidOperationException("Registro no encontrado.");
            nuevos[indice] = registro;
        }
        Reemplazar(nuevos);
    }

    public void Reemplazar(List<T> registros)
    {
        gestor.Guardar(archivo, registros, x => x.ObtenerCampos());
        publicar(gestor.Datos, registros);
    }
}
