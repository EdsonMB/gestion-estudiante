#nullable enable
namespace GestionEstudiante;
partial class FrmEstudiante
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblNombres = null!;
    private TextBox txtNombres = null!;
    private Label lblApellidos = null!;
    private TextBox txtApellidos = null!;
    private Label lblApoderado = null!;
    private TextBox txtApoderado = null!;
    private Label lblTelefono = null!;
    private TextBox txtTelefono = null!;
    private Label lblCorreo = null!;
    private TextBox txtCorreo = null!;
    private Label lblCodigo = null!;
    private TextBox txtCodigo = null!;
    private Label lblNota = null!;
    private Button btnCancelar = null!;
    private Button btnGuardar = null!;
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
        lblNombres = new Label();
        txtNombres = new TextBox();
        lblApellidos = new Label();
        txtApellidos = new TextBox();
        lblApoderado = new Label();
        txtApoderado = new TextBox();
        lblTelefono = new Label();
        txtTelefono = new TextBox();
        lblCorreo = new Label();
        txtCorreo = new TextBox();
        lblCodigo = new Label();
        txtCodigo = new TextBox();
        lblNota = new Label();
        btnCancelar = new Button();
        btnGuardar = new Button();
        SuspendLayout();
        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(900, 44);
        lblTitulo.Text = "Nuevo estudiante";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        this.Controls.Add(lblTitulo);
        // lblNombres
        lblNombres.Name = "lblNombres";
        lblNombres.Location = new Point(28, 100);
        lblNombres.Size = new Size(470, 28);
        lblNombres.Text = "Nombres *";
        this.Controls.Add(lblNombres);
        // txtNombres
        txtNombres.Name = "txtNombres";
        txtNombres.Location = new Point(28, 136);
        txtNombres.Size = new Size(470, 34);
        txtNombres.MaxLength = 80;
        txtNombres.PlaceholderText = "Escribe los nombres";
        this.Controls.Add(txtNombres);
        // lblApellidos
        lblApellidos.Name = "lblApellidos";
        lblApellidos.Location = new Point(530, 100);
        lblApellidos.Size = new Size(470, 28);
        lblApellidos.Text = "Apellidos *";
        this.Controls.Add(lblApellidos);
        // txtApellidos
        txtApellidos.Name = "txtApellidos";
        txtApellidos.Location = new Point(530, 136);
        txtApellidos.Size = new Size(470, 34);
        txtApellidos.MaxLength = 100;
        txtApellidos.PlaceholderText = "Escribe los apellidos";
        this.Controls.Add(txtApellidos);
        // lblApoderado
        lblApoderado.Name = "lblApoderado";
        lblApoderado.Location = new Point(28, 200);
        lblApoderado.Size = new Size(470, 28);
        lblApoderado.Text = "Nombre del apoderado *";
        this.Controls.Add(lblApoderado);
        // txtApoderado
        txtApoderado.Name = "txtApoderado";
        txtApoderado.Location = new Point(28, 236);
        txtApoderado.Size = new Size(470, 34);
        txtApoderado.MaxLength = 120;
        txtApoderado.PlaceholderText = "Escribe el nombre completo";
        this.Controls.Add(txtApoderado);
        // lblTelefono
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Location = new Point(530, 200);
        lblTelefono.Size = new Size(470, 28);
        lblTelefono.Text = "Teléfono del apoderado *";
        this.Controls.Add(lblTelefono);
        // txtTelefono
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Location = new Point(530, 236);
        txtTelefono.Size = new Size(470, 34);
        txtTelefono.PlaceholderText = "9 dígitos";
        this.Controls.Add(txtTelefono);
        // lblCorreo
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Location = new Point(28, 300);
        lblCorreo.Size = new Size(470, 28);
        lblCorreo.Text = "Correo del apoderado (opcional)";
        this.Controls.Add(lblCorreo);
        // txtCorreo
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Location = new Point(28, 336);
        txtCorreo.Size = new Size(470, 34);
        txtCorreo.PlaceholderText = "Opcional";
        this.Controls.Add(txtCorreo);
        // lblCodigo
        lblCodigo.Name = "lblCodigo";
        lblCodigo.Location = new Point(530, 300);
        lblCodigo.Size = new Size(470, 28);
        lblCodigo.Text = "Código";
        this.Controls.Add(lblCodigo);
        // txtCodigo
        txtCodigo.Name = "txtCodigo";
        txtCodigo.Location = new Point(530, 336);
        txtCodigo.Size = new Size(470, 34);
        this.Controls.Add(txtCodigo);
        // lblNota
        lblNota.Name = "lblNota";
        lblNota.Location = new Point(28, 414);
        lblNota.Size = new Size(960, 35);
        lblNota.Text = "El estudiante quedará activo al registrarse. Los campos con * son obligatorios.";
        this.Controls.Add(lblNota);
        // btnCancelar
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Location = new Point(650, 492);
        btnCancelar.Size = new Size(150, 42);
        btnCancelar.Text = "Cancelar";
        btnCancelar.DialogResult = DialogResult.Cancel;
        this.Controls.Add(btnCancelar);
        // btnGuardar
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Location = new Point(816, 492);
        btnGuardar.Size = new Size(185, 42);
        btnGuardar.Text = "Guardar estudiante";
        this.Controls.Add(btnGuardar);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 750);
        BackColor = Color.FromArgb(245, 245, 245);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmEstudiante";
        Text = "Datos del estudiante";
        ClientSize = new Size(1030, 570);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        txtCodigo.ReadOnly = true;
        txtTelefono.MaxLength = 9;
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
        btnGuardar.Click += btnGuardar_Click;
        ResumeLayout(false);
        PerformLayout();
    }
}
