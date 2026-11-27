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
        string? carpeta = ElegirCarpeta("Seleccione la carpeta que contiene los archivos TXT.");
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

    private string? ElegirCarpeta(string descripcion)
    {
        using FolderBrowserDialog dialogo = new FolderBrowserDialog();
        dialogo.Description = descripcion;
        dialogo.UseDescriptionForTitle = true;
        DialogResult resultado = dialogo.ShowDialog(this);
        return resultado == DialogResult.OK ? dialogo.SelectedPath : null;
    }

    private void btnCrearCopia_Click(object? sender, EventArgs e)
    {
        string? carpeta = ElegirCarpeta("Elija dónde crear la carpeta de respaldo con los archivos TXT.");
        if (carpeta == null) return;
        string destino = Path.Combine(carpeta, "respaldo-" + DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
        try
        {
            servicio.CrearRespaldo(destino);
            MessageBox.Show(this, "Copia creada con los cuatro archivos TXT en " + destino, "Respaldo");
        }
        catch (Exception error) { MostrarError(error); }
    }

    private void btnRecuperar_Click(object? sender, EventArgs e)
    {
        string? origen = ElegirCarpeta("Seleccione la carpeta del respaldo que contiene los cuatro archivos TXT.");
        if (origen == null) return;
        if (MessageBox.Show(this, "¿Recuperar esta copia? Se activará una nueva carpeta con el respaldo. La carpeta anterior se conservará.",
            "Recuperar copia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            servicio.RecuperarRespaldo(origen);
            ActualizarInformacion();
            MessageBox.Show(this, "Respaldo recuperado. Los módulos usarán los datos de la nueva carpeta.", "Respaldo");
        }
        catch (Exception error) { MostrarError(error); }
    }

    private void btnMover_Click(object? sender, EventArgs e)
    {
        string? carpeta = ElegirCarpeta("Elija la carpeta que contendrá la nueva carpeta de datos.");
        if (carpeta == null) return;
        string destino = Path.Combine(carpeta, "datos-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
        if (MessageBox.Show(this, "¿Mover los datos a " + destino + "?", "Mover datos",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try { servicio.MoverArchivo(destino); ActualizarInformacion(); }
        catch (Exception error) { MostrarError(error); }
    }

    private void MostrarError(Exception error)
    {
        MessageBox.Show(this, error.Message, "No se pudo procesar la carpeta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
