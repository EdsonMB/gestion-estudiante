#if REDISENO_VISUAL
namespace GestionEstudiante.RedisenoVisual;

public partial class FrmAccesoVisual : Form
{
    public FrmAccesoVisual()
    {
        InitializeComponent();
    }

    // Acceso de demostración: los campos todavía no autentican usuarios.
    private void btnIngresar_Click(object? sender, EventArgs e)
    {
        Hide();
        using (FrmInicioVisual inicio = new FrmInicioVisual())
        {
            inicio.ShowDialog();
        }
        txtClave.Clear();
        Show();
        txtUsuario.Focus();
    }
}
#endif
