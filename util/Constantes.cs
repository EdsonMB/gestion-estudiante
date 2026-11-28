using System.Collections.ObjectModel;
using GestionEstudiante.model;

namespace GestionEstudiante.util;

public static class Constantes
{
    public const string Version = "1.0";
    public const string NombreCentroEducativo = "Centro Educativo Jul";
    public const int AnioEscolar = 2026;
    public const string UsuarioAdministrador = "admin";
    public const string ClaveAdministrador = "987987";
    public const char Separador = '|';
    public const string ArchivoAulas = "aulas.txt";
    public const string ArchivoMatriculas = "matriculas.txt";
    public const string ArchivoAsistencias = "asistencias.txt";
    public const string ArchivoEstudiantes = "estudiantes.txt";
    public const string ArchivoUbicacion = "ubicacion-db.txt";
    public const string FormatoFecha = "O";
    public const string FormatoCodigoEstudiante = "000000";
    public const string PrefijoCodigoEstudiante = "E";
    public const string InstanciaAplicacion = "GestionEstudiante.App";

    public const string NivelInicial = "Inicial";
    public const string NivelPrimaria = "Primaria";
    public const string NivelSecundaria = "Secundaria";
    public const string AsistenciaSinRegistro = "Sin registro";
    public const string AsistenciaPresente = nameof(EstadoAsistencia.Presente);
    public const string AsistenciaFalta = nameof(EstadoAsistencia.Falta);
    public const string AsistenciaTardanza = nameof(EstadoAsistencia.Tardanza);

    // Listas fijas de los combos. El orden de los filtros conserva sus índices actuales.
    public static readonly ReadOnlyCollection<string> Niveles = Array.AsReadOnly(new[]
    {
        NivelInicial, NivelPrimaria, NivelSecundaria
    });
    public static readonly ReadOnlyCollection<string> FiltrosNiveles = Array.AsReadOnly(
        new[] { "Todos los niveles" }.Concat(Niveles).ToArray());
    public static readonly ReadOnlyCollection<string> EstadosEstudiantes = Array.AsReadOnly(new[]
    {
        "Todos los estados", "Activos", "Inactivos"
    });
    public static readonly ReadOnlyCollection<string> EstadosMatriculas = Array.AsReadOnly(new[]
    {
        "Activas y canceladas", "Solo activas", "Solo canceladas"
    });
    public static readonly ReadOnlyCollection<string> EstadosAsistencia = Array.AsReadOnly(new[]
    {
        AsistenciaSinRegistro, AsistenciaPresente, AsistenciaFalta, AsistenciaTardanza
    });
    public static readonly ReadOnlyCollection<string> TiposReportes = Array.AsReadOnly(new[]
    {
        "Ocupación de aulas", "Asistencia por estudiante"
    });
    public static readonly ReadOnlyCollection<string> FiltrosAulas = Array.AsReadOnly(new[]
    {
        "Todas las aulas"
    });

    //total de columnas
    public const int EstudiantesColumnas = 9;

    public static readonly string CarpetaAplicacion = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GestionEstudiante");
    public static readonly string CarpetaDatos = Path.Combine(CarpetaAplicacion, "datos");
}
