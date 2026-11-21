namespace GestionEstudiante;

public partial class FrmMatriculas : Form
{
    public FrmMatriculas()
    {
        InitializeComponent();
    }

    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using FrmMatricula formulario = new FrmMatricula();
        formulario.ShowDialog(this);
    }
}
