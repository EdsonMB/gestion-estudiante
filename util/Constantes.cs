namespace GestionEstudiante.util;

public static class Constantes
{
    public const string Version = "1.0";
    public const string NombreCentroEducativo = "Centro Educativo - Jose Martí";
    public const int AnioEscolar = 2026;
    public const int AnioEscolarMinimo = 2000;
    public const int AnioEscolarMaximo = 2100;
    public const int CapacidadMinimaAula = 1;
    public const int CapacidadMaximaAula = 40;
    public const int LongitudMaximaSeccion = 10;
    public const int GradoMinimoInicial = 3;
    public const int GradoMinimoPrimariaYSecundaria = 1;
    public const int GradoMaximoPrimaria = 6;
    public const int GradoMaximoInicialYSecundaria = 5;
    public const int LongitudMaximaNombres = 80;
    public const int LongitudMaximaApellidos = 100;
    public const int LongitudMaximaApoderado = 120;
    public const int LongitudTelefono = 9;
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
    public const string TodosLosNiveles = "Todos los niveles";
    public const string TodasLasAulas = "Todas las aulas";
    public const string TodosLosEstados = "Todos los estados";
    public const string EstudiantesActivos = "Activos";
    public const string EstudiantesInactivos = "Inactivos";
    public const string MatriculasActivasYCanceladas = "Activas y canceladas";
    public const string MatriculasSoloActivas = "Solo activas";
    public const string MatriculasSoloCanceladas = "Solo canceladas";
    public const string AsistenciaSinRegistro = "Sin registro";
    public const string AsistenciaPresente = "Presente";
    public const string AsistenciaFalta = "Falta";
    public const string AsistenciaTardanza = "Tardanza";
    public const string ReporteOcupacion = "Ocupación de aulas";
    public const string ReporteAsistencia = "Asistencia por estudiante";

    public const int EstudiantesColumnas = 9;
    public const int AulasColumnas = 7;
    public const int MatriculasColumnas = 5;
    public const int AsistenciasColumnas = 5;

    public static readonly string CarpetaAplicacion = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GestionEstudiante");
}
