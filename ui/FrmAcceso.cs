using GestionEstudiante.repository;
using GestionEstudiante.service;

namespace GestionEstudiante;

public partial class FrmAcceso : Form
{
    public FrmAcceso()
    {
        InitializeComponent();
    }

    // Acceso de demostración: los campos todavía no autentican usuarios.
    private void btnIngresar_Click(object? sender, EventArgs e)
    {
        EstudianteService servicio;
        ArchivoService servicioArchivos;
        try
        {
            EstudianteRepository repositorio = new EstudianteRepository();
            servicio = new EstudianteService(repositorio);
            servicioArchivos = new ArchivoService(repositorio);
        }
        catch (Exception error)
        {
            MessageBox.Show(this, "No se pudo leer el archivo de datos. " + error.Message, "Archivo inválido");
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
