using GestionEstudiante.repository;
using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;

public partial class FrmAcceso : Form
{
    private GestorBaseDatos? gestor;

    public FrmAcceso()
    {
        InitializeComponent();
    }

    private void btnIngresar_Click(object? sender, EventArgs e)
    {
        if (txtUsuario.Text.Trim() != Constantes.UsuarioAdministrador || txtClave.Text != Constantes.ClaveAdministrador)
        {
            MessageBox.Show(this, "El usuario o la contraseña son incorrectos.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtClave.Clear();
            txtClave.Focus();
            return;
        }

        EstudianteService servicio;
        ArchivoService servicioArchivos;
        AulaService servicioAulas;
        MatriculaService servicioMatriculas;
        AsistenciaService servicioAsistencia;
        try
        {
            if (gestor == null)
            {
                GestorBaseDatos nuevoGestor = new GestorBaseDatos();
                if (!AbrirCarpetaDeDatos(nuevoGestor))
                {
                    return;
                }
                gestor = nuevoGestor;
            }

            EstudianteRepository estudiantes = new EstudianteRepository(gestor);
            AulaRepository aulas = new AulaRepository(gestor);
            MatriculaRepository matriculas = new MatriculaRepository(gestor);
            AsistenciaRepository asistencia = new AsistenciaRepository(gestor);
            servicio = new EstudianteService(estudiantes);
            servicioArchivos = new ArchivoService(gestor);
            servicioAulas = new AulaService(aulas, matriculas);
            servicioMatriculas = new MatriculaService(matriculas, estudiantes, aulas);
            servicioAsistencia = new AsistenciaService(asistencia, aulas, matriculas);
        }
        catch (Exception error)
        {
            MessageBox.Show(this, "No se pudo abrir la carpeta de datos. " + error.Message, "Archivo inválido");
            return;
        }

        Hide();
        using (FrmInicio inicio = new FrmInicio(servicio, servicioArchivos, servicioAulas,
            servicioMatriculas, servicioAsistencia))
        {
            inicio.ShowDialog();
        }
        txtClave.Clear();
        Show();
        txtUsuario.Focus();
    }

    private bool AbrirCarpetaDeDatos(GestorBaseDatos nuevoGestor)
    {
        try
        {
            if (nuevoGestor.CargarUbicacionGuardada())
            {
                return true;
            }
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message + " Seleccione otra carpeta de datos.",
                "Carpeta de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        string? carpeta = FrmArchivos.ElegirCarpeta(this);
        if (carpeta == null)
        {
            return false;
        }

        nuevoGestor.CargarUsar(carpeta);
        return true;
    }
}
