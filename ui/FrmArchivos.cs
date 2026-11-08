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

    private void btnCrear_Click(object? sender, EventArgs e)
    {
        using SaveFileDialog dialogo = new SaveFileDialog();
        dialogo.Filter = "Archivo de respaldo (*.json)|*.json";
        dialogo.FileName = "gestion-escolar.respaldo.json";
        DialogResult resultadoDialogo = dialogo.ShowDialog(this);
        if (resultadoDialogo != DialogResult.OK)
        {
            return;
        }

        try
        {
            servicio.CrearCopia(dialogo.FileName);
            MessageBox.Show(this, "Copia de seguridad creada.", "Respaldo creado");
        }
        catch (Exception error)
        {
            MostrarError(error);
        }
    }

    private void btnCargarUsar_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialogo = new OpenFileDialog();
        dialogo.Title = "Selecciona el archivo que se utilizará como base de datos";
        dialogo.Filter = "Base de datos escolar (*.json)|*.json";
        DialogResult resultadoDialogo = dialogo.ShowDialog(this);
        if (resultadoDialogo != DialogResult.OK)
        {
            return;
        }

        try
        {
            DialogResult respuesta = MessageBox.Show(this,
                "¿Desea usar el archivo seleccionado? Los siguientes cambios se guardarán allí.",
                "Cargar / usar archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            servicio.CargarUsar(dialogo.FileName);
            ActualizarInformacion();
            MessageBox.Show(this, "Ahora se está utilizando el archivo seleccionado.", "Archivo cargado");
        }
        catch (Exception error)
        {
            MostrarError(error);
        }
    }

    private void btnMover_Click(object? sender, EventArgs e)
    {
        using FolderBrowserDialog dialogo = new FolderBrowserDialog();
        dialogo.Description = "Selecciona la carpeta a la que se moverá el archivo activo";
        dialogo.UseDescriptionForTitle = true;
        DialogResult resultadoDialogo = dialogo.ShowDialog(this);
        if (resultadoDialogo != DialogResult.OK)
        {
            return;
        }

        try
        {
            string destino = Path.Combine(dialogo.SelectedPath, Path.GetFileName(servicio.Ruta));
            servicio.MoverArchivo(destino);
            ActualizarInformacion();
            MessageBox.Show(this, "El archivo está en la carpeta elegida. Los cambios se guardarán allí.", "Archivo movido");
        }
        catch (Exception error)
        {
            MostrarError(error);
        }
    }

    private void btnRecuperar_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialogo = new OpenFileDialog();
        dialogo.Filter = "Base de datos escolar (*.json)|*.json";
        DialogResult resultadoDialogo = dialogo.ShowDialog(this);
        if (resultadoDialogo != DialogResult.OK)
        {
            return;
        }

        try
        {
            DialogResult respuesta = MessageBox.Show(this,
                "¿Desea recuperar la copia de seguridad y reemplazar los datos actuales?",
                "Confirmar recuperación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            servicio.Recuperar(dialogo.FileName);
            ActualizarInformacion();
            MessageBox.Show(this, "Operación completada correctamente.", "Información");
        }
        catch (Exception)
        {
            MessageBox.Show(this, "No se pudo completar la operación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void MostrarError(Exception error)
    {
        MessageBox.Show(this, error.Message, "No se pudo procesar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
