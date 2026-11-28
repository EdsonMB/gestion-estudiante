using GestionEstudiante.service;

namespace GestionEstudiante;

public partial class FrmArchivos : Form
{
    private readonly ArchivoService servicio = null!;

    public FrmArchivos()
    {
        InitializeComponent();
    }

    public FrmArchivos(ArchivoService servicio)
    {
        this.servicio = servicio;
        InitializeComponent();
    }

    private void FrmArchivos_Load(object? sender, EventArgs e)
    {
        ActualizarInformacion();
    }

    public void ActualizarInformacion()
    {
        lblRuta.Text = servicio.Ruta;
        lblCantidad.Text = servicio.ObtenerResumenArchivo();
    }

    private void btnCargarUsar_Click(object? sender, EventArgs e)
    {
        string? carpeta = ElegirCarpeta(this);
        if (carpeta == null)
        {
            return;
        }

        DialogResult respuesta = MessageBox.Show(this,
            "¿Desea usar esta carpeta? Los siguientes cambios se guardarán allí.",
            "Cargar carpeta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (respuesta != DialogResult.Yes)
        {
            return;
        }

        try
        {
            servicio.CargarUsar(carpeta);
            ActualizarInformacion();
            MessageBox.Show(this, "La carpeta seleccionada está activa.", "Carpeta cargada");
        }
        catch (Exception error)
        {
            MostrarError(error);
        }
    }

    internal static string? ElegirCarpeta(IWin32Window propietario)
    {
        using FolderBrowserDialog dialogo = new FolderBrowserDialog();
        dialogo.Description = "Seleccione una carpeta vacía o una que contenga los cuatro archivos TXT.";
        dialogo.UseDescriptionForTitle = true;
        DialogResult resultado = dialogo.ShowDialog(propietario);
        return resultado == DialogResult.OK ? dialogo.SelectedPath : null;
    }

    private void MostrarError(Exception error)
    {
        MessageBox.Show(this, error.Message, "No se pudo procesar la carpeta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
