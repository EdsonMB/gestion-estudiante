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
        try
        {
            if (gestor == null)
            {
                gestor = new GestorBaseDatos();
            }

            EstudianteRepository repositorio = new EstudianteRepository(gestor);
            servicio = new EstudianteService(repositorio);
            servicioArchivos = new ArchivoService(gestor);
        }
        catch (Exception error)
        {
            MessageBox.Show(this, "No se pudo abrir la carpeta de datos. " + error.Message, "Archivo inválido");
            return;
        }

        Hide();
        using (FrmInicio inicio = new FrmInicio(servicio, servicioArchivos))
        {
            inicio.ShowDialog();
        }
        txtClave.Clear();
        Show();
        txtUsuario.Focus();
    }
}
