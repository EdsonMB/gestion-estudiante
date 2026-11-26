namespace GestionEstudiante.util;

public static class Constantes
{
    public const string Version = "1.0";
    public const string NombreCentroEducativo = "Centro Educativo Jul";
    public const int AnioEscolar = 2026;
    public const string UsuarioAdministrador = "admin";
    public const string ClaveAdministrador = "987987";
    public const char Separador = '|';
    public const string ArchivoEstudiantes = "estudiantes.txt";
    public const string ArchivoAsistencias = "asistencias.txt";
    public const string FormatoFechaAsistencia = "yyyy-MM-dd";
    public const string ArchivoUbicacion = "ubicacion-db.txt";
    public const string FormatoFecha = "O";
    public const string FormatoCodigoEstudiante = "000000";
    public const string PrefijoCodigoEstudiante = "E";
    public const string InstanciaAplicacion = "GestionEstudiante.App";

    //total de columnas
    public const int EstudiantesColumnas = 9;

    public static readonly string CarpetaAplicacion = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GestionEstudiante");
    public static readonly string CarpetaDatos = Path.Combine(CarpetaAplicacion, "datos");
}
