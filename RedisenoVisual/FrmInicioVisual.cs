#if REDISENO_VISUAL
namespace GestionEstudiante.RedisenoVisual;

public partial class FrmInicioVisual : Form
{
    private FrmGestionEstudiantes? estudiantes;

    public FrmInicioVisual()
    {
        InitializeComponent();
    }

    private void OcultarPantallas()
    {
        panelInicio.Hide();
        panelModulo.Hide();
        if (estudiantes != null) estudiantes.Hide();
    }

    private void btnInicio_Click(object? sender, EventArgs e)
    {
        OcultarPantallas();
        lblCabecera.Text = "Administración académica";
        panelInicio.Show();
        panelInicio.BringToFront();
    }

    private void btnEstudiantes_Click(object? sender, EventArgs e)
    {
        OcultarPantallas();
        lblCabecera.Text = "Estudiantes";
        // Se crea una sola vez para conservar sus registros al cambiar de sección.
        if (estudiantes == null)
        {
            estudiantes = new FrmGestionEstudiantes();
            estudiantes.TopLevel = false;
            estudiantes.FormBorderStyle = FormBorderStyle.None;
            estudiantes.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(estudiantes);
        }
        estudiantes.Show();
        estudiantes.BringToFront();
    }

    private void MostrarModulo(string titulo)
    {
        OcultarPantallas();
        lblCabecera.Text = titulo;
        lblModulo.Text = titulo;
        panelModulo.Show();
        panelModulo.BringToFront();
    }

    private void btnAulas_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Aulas");
    }

    private void btnMatriculas_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Matrículas");
    }

    private void btnAsistencia_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Asistencia");
    }

    private void btnReportes_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Reportes");
    }

    private void btnArchivos_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Archivos y respaldo");
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
#endif
