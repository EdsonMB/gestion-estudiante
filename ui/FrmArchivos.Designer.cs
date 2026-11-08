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
    private Button btnMover = null!;
    private GroupBox grupoCopia = null!;
    private Label lblCopia = null!;
    private Button btnCrear = null!;
    private Button btnRecuperar = null!;
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
        btnMover = new Button();
        grupoCopia = new GroupBox();
        lblCopia = new Label();
        btnCrear = new Button();
        btnRecuperar = new Button();
        SuspendLayout();
        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(900, 44);
        lblTitulo.Text = "Archivos y respaldo";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        this.Controls.Add(lblTitulo);
        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 76);
        lblDescripcion.Size = new Size(950, 32);
        lblDescripcion.Text = "Conserva una copia de tu información y recupérala cuando sea necesario.";
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
        // btnMover
        btnMover.Name = "btnMover";
        btnMover.Location = new Point(184, 156);
        btnMover.Size = new Size(150, 40);
        btnMover.Text = "Mover archivo";
        grupoArchivo.Controls.Add(btnMover);
        // grupoCopia
        grupoCopia.Name = "grupoCopia";
        grupoCopia.Location = new Point(28, 374);
        grupoCopia.Size = new Size(1020, 215);
        grupoCopia.Text = "Copia de seguridad";
        grupoCopia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(grupoCopia);
        // lblCopia
        lblCopia.Name = "lblCopia";
        lblCopia.Location = new Point(20, 36);
        lblCopia.Size = new Size(970, 65);
        lblCopia.Text = "Al recuperar una copia se reemplazarán los datos actuales. Se pedirá confirmación.";
        grupoCopia.Controls.Add(lblCopia);
        // btnCrear
        btnCrear.Name = "btnCrear";
        btnCrear.Location = new Point(20, 142);
        btnCrear.Size = new Size(150, 40);
        btnCrear.Text = "Crear copia";
        grupoCopia.Controls.Add(btnCrear);
        // btnRecuperar
        btnRecuperar.Name = "btnRecuperar";
        btnRecuperar.Location = new Point(184, 142);
        btnRecuperar.Size = new Size(150, 40);
        btnRecuperar.Text = "Recuperar copia";
        grupoCopia.Controls.Add(btnRecuperar);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 750);
        BackColor = Color.FromArgb(245, 245, 245);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmArchivos";
        Text = "Archivos y respaldo";
        btnMover.Click += btnMover_Click;
        btnCargarUsar.Click += btnCargarUsar_Click;
        btnCrear.Click += btnCrear_Click;
        btnRecuperar.Click += btnRecuperar_Click;
        Load += FrmArchivos_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}
