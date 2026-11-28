using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.util;

namespace GestionEstudiante.service;

public class AulaService
{
    private readonly AulaRepository repositorio;
    private readonly MatriculaRepository matriculas;

    public AulaService(AulaRepository repositorio, MatriculaRepository matriculas)
    {
        this.repositorio = repositorio;
        this.matriculas = matriculas;
    }

    public List<Aula> Listar() => repositorio.Listar();
    public int Ocupados(int id) => matriculas.Listar().Count(x => x.aulaId == id && x.Activa);
    public int Disponibles(Aula aula) => aula.capacidad - Ocupados(aula.id);

    public void Guardar(Aula aula)
    {
        Aula copia = new Aula { id = aula.id, nivel = aula.nivel, grado = aula.grado,
            seccion = aula.seccion.Trim().ToUpperInvariant(), anio = aula.anio, capacidad = aula.capacidad, activo = aula.activo };
        ValidarAula(copia);
        if (Listar().Any(x => x.id != copia.id && x.nivel == copia.nivel && x.grado == copia.grado &&
            x.seccion.Equals(copia.seccion, StringComparison.OrdinalIgnoreCase) && x.anio == copia.anio))
            throw new InvalidOperationException("Ya existe un aula con ese nivel, grado, sección y año.");
        if (copia.capacidad < Ocupados(copia.id))
            throw new InvalidOperationException("La capacidad no puede ser menor que la cantidad de matriculados.");
        Aula? anterior = Listar().SingleOrDefault(x => x.id == copia.id);
        if (anterior != null && matriculas.Listar().Any(x => x.aulaId == copia.id) &&
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

    internal static void ValidarRegistros(List<Aula> aulas, List<Matricula> matriculas)
    {
        foreach (Aula aula in aulas)
        {
            ValidarAula(aula);
            if (matriculas.Count(x => x.aulaId == aula.id && x.Activa) > aula.capacidad)
            {
                throw new ArgumentException("La ocupación supera la capacidad de un aula.");
            }
        }

        if (aulas.GroupBy(x => (x.nivel, x.grado, x.seccion.Trim().ToUpperInvariant(), x.anio)).Any(g => g.Count() > 1))
        {
            throw new ArgumentException("Hay aulas repetidas para el mismo nivel, grado, sección y año.");
        }
    }

    private static void ValidarAula(Aula aula)
    {
        int minimo = aula.nivel == Constantes.NivelInicial ? 3 : 1;
        int maximo = aula.nivel switch
        {
            Constantes.NivelInicial => 5,
            Constantes.NivelPrimaria => 6,
            Constantes.NivelSecundaria => 5,
            _ => 0
        };

        if (aula.grado < minimo || aula.grado > maximo)
        {
            throw new ArgumentException("Seleccione nivel y grado válidos (Inicial: 3 a 5 años; Primaria: 1 a 6; Secundaria: 1 a 5).");
        }
        if (string.IsNullOrWhiteSpace(aula.seccion) || aula.seccion.Length > 10)
        {
            throw new ArgumentException("La sección es obligatoria y admite hasta 10 caracteres.");
        }
        if (aula.anio < 2000 || aula.anio > 2100 || aula.capacidad < 1 || aula.capacidad > 1000)
        {
            throw new ArgumentException("Ingrese un año entre 2000 y 2100 y una capacidad entre 1 y 1000.");
        }
    }
}
