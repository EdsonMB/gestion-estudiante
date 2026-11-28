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

    public List<Aula> Listar()
    {
        return repositorio.Listar();
    }

    public int Ocupados(int id)
    {
        return ContarOcupados(matriculas.Listar(), id);
    }

    public int Disponibles(Aula aula)
    {
        return aula.capacidad - Ocupados(aula.id);
    }

    public void Guardar(Aula aula)
    {
        Aula copia = new Aula { id = aula.id, nivel = aula.nivel, grado = aula.grado,
            seccion = aula.seccion.Trim().ToUpperInvariant(), anio = aula.anio, capacidad = aula.capacidad, activo = aula.activo };
        ValidarAula(copia);
        Aula? anterior = null;
        foreach (Aula existente in Listar())
        {
            if (existente.id == copia.id)
            {
                anterior = existente;
            }
            else if (Clave(existente) == Clave(copia))
            {
                throw new InvalidOperationException("Ya existe un aula con ese nivel, grado, sección y año.");
            }
        }

        List<Matricula> inscripciones = matriculas.Listar();
        int ocupados = ContarOcupados(inscripciones, copia.id);
        if (copia.capacidad < ocupados)
            throw new InvalidOperationException("La capacidad no puede ser menor que la cantidad de matriculados.");
        if (anterior != null && Clave(anterior) != Clave(copia))
        {
            foreach (Matricula matricula in inscripciones)
            {
                if (matricula.aulaId == copia.id)
                    throw new InvalidOperationException("Un aula con historial solo permite cambiar su capacidad y estado.");
            }
        }
        if (!copia.activo && ocupados > 0)
            throw new InvalidOperationException("Cancele las matrículas activas antes de desactivar el aula.");
        repositorio.Guardar(copia);
    }
    public void CambiarEstado(int id)
    {
        foreach (Aula aula in Listar())
        {
            if (aula.id == id)
            {
                Guardar(new Aula { id = id, nivel = aula.nivel, grado = aula.grado, seccion = aula.seccion,
                    anio = aula.anio, capacidad = aula.capacidad, activo = !aula.activo });
                return;
            }
        }
        throw new InvalidOperationException("Aula no encontrada.");
    }

    internal static void ValidarRegistros(List<Aula> aulas, List<Matricula> matriculas)
    {
        HashSet<(string Nivel, int Grado, string Seccion, int Anio)> claves = new();
        foreach (Aula aula in aulas)
        {
            ValidarAula(aula);
            if (!claves.Add(Clave(aula)))
            {
                throw new ArgumentException("Hay aulas repetidas para el mismo nivel, grado, sección y año.");
            }
            if (ContarOcupados(matriculas, aula.id) > aula.capacidad)
            {
                throw new ArgumentException("La ocupación supera la capacidad de un aula.");
            }
        }

    }

    private static (string Nivel, int Grado, string Seccion, int Anio) Clave(Aula aula)
    {
        return (aula.nivel, aula.grado, aula.seccion.Trim().ToUpperInvariant(), aula.anio);
    }

    private static int ContarOcupados(List<Matricula> matriculas, int aulaId)
    {
        int ocupados = 0;
        foreach (Matricula matricula in matriculas)
        {
            if (matricula.aulaId == aulaId && matricula.Activa)
            {
                ocupados++;
            }
        }
        return ocupados;
    }

    private static void ValidarAula(Aula aula)
    {
        int minimo = aula.nivel == Constantes.NivelInicial
            ? Constantes.GradoMinimoInicial : Constantes.GradoMinimoPrimariaYSecundaria;
        int maximo = 0;
        switch (aula.nivel)
        {
            case Constantes.NivelInicial:
            case Constantes.NivelSecundaria:
                maximo = Constantes.GradoMaximoInicialYSecundaria;
                break;
            case Constantes.NivelPrimaria:
                maximo = Constantes.GradoMaximoPrimaria;
                break;
        }

        if (aula.grado < minimo || aula.grado > maximo)
        {
            throw new ArgumentException("Seleccione nivel y grado válidos " +
                $"(Inicial: {Constantes.GradoMinimoInicial} a {Constantes.GradoMaximoInicialYSecundaria} años; " +
                $"Primaria: {Constantes.GradoMinimoPrimariaYSecundaria} a {Constantes.GradoMaximoPrimaria}; " +
                $"Secundaria: {Constantes.GradoMinimoPrimariaYSecundaria} a {Constantes.GradoMaximoInicialYSecundaria}).");
        }
        if (string.IsNullOrWhiteSpace(aula.seccion) || aula.seccion.Length > Constantes.LongitudMaximaSeccion)
        {
            throw new ArgumentException($"La sección es obligatoria y admite hasta {Constantes.LongitudMaximaSeccion} caracteres.");
        }
        if (aula.anio < Constantes.AnioEscolarMinimo || aula.anio > Constantes.AnioEscolarMaximo ||
            aula.capacidad < Constantes.CapacidadMinimaAula || aula.capacidad > Constantes.CapacidadMaximaAula)
        {
            throw new ArgumentException($"Ingrese un año entre {Constantes.AnioEscolarMinimo} y {Constantes.AnioEscolarMaximo} " +
                $"y una capacidad entre {Constantes.CapacidadMinimaAula} y {Constantes.CapacidadMaximaAula}.");
        }
    }
}
