#nullable enable
namespace GestionEstudiante;
partial class FrmArchivos
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private GroupBox grupoArchivo = null!;
    private Label lblRuta = null!;
    private Label lblCantidad = null!;
    private Button btnCargarUsar = null!;
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        lblDescripcion = new Label();
        grupoArchivo = new GroupBox();
        lblRuta = new Label();
        lblCantidad = new Label();
        btnCargarUsar = new Button();
        SuspendLayout();
        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(900, 44);
        lblTitulo.Text = "Archivos";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        this.Controls.Add(lblTitulo);
        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 76);
        lblDescripcion.Size = new Size(950, 32);
        lblDescripcion.Text = "Consulta la carpeta activa o selecciona otra carpeta de datos.";
        this.Controls.Add(lblDescripcion);
        // grupoArchivo
        grupoArchivo.Name = "grupoArchivo";
        grupoArchivo.Location = new Point(28, 132);
        grupoArchivo.Size = new Size(1020, 215);
        grupoArchivo.Text = "Información guardada en esta computadora";
        grupoArchivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(grupoArchivo);
        // lblRuta
        lblRuta.Name = "lblRuta";
        lblRuta.Location = new Point(20, 36);
        lblRuta.Size = new Size(970, 65);
        grupoArchivo.Controls.Add(lblRuta);
        // lblCantidad
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Location = new Point(20, 109);
        lblCantidad.Size = new Size(970, 32);
        grupoArchivo.Controls.Add(lblCantidad);
        // btnCargarUsar
        btnCargarUsar.Name = "btnCargarUsar";
        btnCargarUsar.Location = new Point(20, 156);
        btnCargarUsar.Size = new Size(150, 40);
        btnCargarUsar.Text = "Cargar / usar";
        grupoArchivo.Controls.Add(btnCargarUsar);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 750);
        BackColor = Color.FromArgb(245, 245, 245);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmArchivos";
        Text = "Archivos";
        btnCargarUsar.Click += btnCargarUsar_Click;
        Load += FrmArchivos_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}
