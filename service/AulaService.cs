using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;
namespace GestionEstudiante.service;

public class AulaService
{
    private readonly GestorBaseDatos gestor;
    private readonly Repositorio<Aula> repositorio;
    public AulaService(GestorBaseDatos gestor)
    {
        this.gestor = gestor;
        repositorio = new Repositorio<Aula>(gestor, Constantes.ArchivoAulas, d => d.aulas, (d, lista) => d.aulas = lista);
    }
    public List<Aula> Listar() => repositorio.Listar();
    public int Ocupados(int id) => gestor.Datos.matriculas.Count(x => x.aulaId == id && x.Activa);
    public int Disponibles(Aula aula) => aula.capacidad - Ocupados(aula.id);

    public void Guardar(Aula aula)
    {
        Aula copia = new Aula { id = aula.id, nivel = aula.nivel, grado = aula.grado,
            seccion = aula.seccion.Trim().ToUpperInvariant(), anio = aula.anio, capacidad = aula.capacidad, activo = aula.activo };
        ValidacionDatos.ValidarAula(copia);
        if (Listar().Any(x => x.id != copia.id && x.nivel == copia.nivel && x.grado == copia.grado &&
            x.seccion.Equals(copia.seccion, StringComparison.OrdinalIgnoreCase) && x.anio == copia.anio))
            throw new InvalidOperationException("Ya existe un aula con ese nivel, grado, sección y año.");
        if (copia.capacidad < Ocupados(copia.id))
            throw new InvalidOperationException("La capacidad no puede ser menor que la cantidad de matriculados.");
        Aula? anterior = Listar().SingleOrDefault(x => x.id == copia.id);
        if (anterior != null && gestor.Datos.matriculas.Any(x => x.aulaId == copia.id) &&
            (anterior.nivel != copia.nivel || anterior.grado != copia.grado || anterior.seccion != copia.seccion || anterior.anio != copia.anio))
            throw new InvalidOperationException("Un aula con historial solo permite cambiar su capacidad y estado.");
        if (!copia.activo && Ocupados(copia.id) > 0)
            throw new InvalidOperationException("Cancele las matrículas activas antes de desactivar el aula.");
        repositorio.Guardar(copia);
    }
    public void CambiarEstado(int id)
    {
        Aula aula = Listar().Single(x => x.id == id);
        Guardar(new Aula { id = id, nivel = aula.nivel, grado = aula.grado, seccion = aula.seccion,
            anio = aula.anio, capacidad = aula.capacidad, activo = !aula.activo });
    }
}
