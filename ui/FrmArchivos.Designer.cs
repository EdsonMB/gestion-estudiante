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
    private GroupBox grupoRespaldo = null!;
    private Label lblRespaldo = null!;
    private Button btnCrearCopia = null!;
    private Button btnRecuperar = null!;
    private Button btnMover = null!;
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
        lblTitulo.Text = "Archivos y respaldo";
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
        btnMover = new Button();
        btnMover.Name = "btnMover";
        btnMover.Text = "Mover carpeta";
        btnMover.Location = new Point(190, 156);
        btnMover.Size = new Size(150, 40);
        btnMover.Click += btnMover_Click;
        grupoArchivo.Controls.Add(btnMover);
        grupoRespaldo = new GroupBox();
        grupoRespaldo.Name = "grupoRespaldo";
        grupoRespaldo.Text = "Copia de seguridad";
        grupoRespaldo.Location = new Point(28, 375);
        grupoRespaldo.Size = new Size(1020, 180);
        grupoRespaldo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRespaldo = new Label();
        lblRespaldo.Location = new Point(20, 35);
        lblRespaldo.Size = new Size(970, 50);
        lblRespaldo.Text = "Guarda una copia ZIP de los archivos TXT. Al recuperarla se conserva la carpeta anterior.";
        btnCrearCopia = new Button();
        btnCrearCopia.Text = "Crear copia";
        btnCrearCopia.Location = new Point(20, 110);
        btnCrearCopia.Size = new Size(150, 40);
        btnCrearCopia.Click += btnCrearCopia_Click;
        btnRecuperar = new Button();
        btnRecuperar.Text = "Recuperar copia";
        btnRecuperar.Location = new Point(190, 110);
        btnRecuperar.Size = new Size(150, 40);
        btnRecuperar.Click += btnRecuperar_Click;
        grupoRespaldo.Controls.AddRange(new Control[] { lblRespaldo, btnCrearCopia, btnRecuperar });
        Controls.Add(grupoRespaldo);
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
