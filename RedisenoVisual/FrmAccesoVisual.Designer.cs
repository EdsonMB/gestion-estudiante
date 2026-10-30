#if REDISENO_VISUAL
namespace GestionEstudiante.RedisenoVisual;

partial class FrmAccesoVisual
{
    private System.ComponentModel.IContainer? components = null;
    private Panel panelAcceso = null!;
    private Label lblMarca = null!;
    private Label lblTitulo = null!;
    private Label lblDetalle = null!;
    private Label lblUsuario = null!;
    private TextBox txtUsuario = null!;
    private Label lblClave = null!;
    private TextBox txtClave = null!;
    private Button btnIngresar = null!;
    private Label lblNota = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelAcceso = new Panel();
        lblMarca = new Label();
        lblTitulo = new Label();
        lblDetalle = new Label();
        lblUsuario = new Label();
        txtUsuario = new TextBox();
        lblClave = new Label();
        txtClave = new TextBox();
        btnIngresar = new Button();
        lblNota = new Label();
        SuspendLayout();

        // panelAcceso
        panelAcceso.Name = "panelAcceso";
        panelAcceso.Location = new Point(230, 65);
        panelAcceso.Size = new Size(440, 470);
        panelAcceso.BackColor = Color.White;
        panelAcceso.BorderStyle = BorderStyle.FixedSingle;
        panelAcceso.Anchor = AnchorStyles.None;
        Controls.Add(panelAcceso);

        // lblMarca
        lblMarca.Name = "lblMarca";
        lblMarca.Location = new Point(30, 25);
        lblMarca.Size = new Size(375, 35);
        lblMarca.Text = "Gestión Escolar";
        lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        panelAcceso.Controls.Add(lblMarca);

        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(30, 72);
        lblTitulo.Size = new Size(375, 30);
        lblTitulo.Text = "Inicio de sesión";
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        panelAcceso.Controls.Add(lblTitulo);

        // lblDetalle
        lblDetalle.Name = "lblDetalle";
        lblDetalle.Location = new Point(30, 108);
        lblDetalle.Size = new Size(375, 40);
        lblDetalle.Text = "Acceso del equipo de desarrollo";
        lblDetalle.ForeColor = Color.DimGray;
        panelAcceso.Controls.Add(lblDetalle);

        // lblUsuario
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Location = new Point(30, 162);
        lblUsuario.Size = new Size(375, 22);
        lblUsuario.Text = "Usuario";
        panelAcceso.Controls.Add(lblUsuario);

        // txtUsuario
        txtUsuario.Name = "txtUsuario";
        txtUsuario.Location = new Point(30, 188);
        txtUsuario.Size = new Size(375, 29);
        txtUsuario.TabIndex = 0;
        txtUsuario.AccessibleName = "Usuario";
        panelAcceso.Controls.Add(txtUsuario);

        // lblClave
        lblClave.Name = "lblClave";
        lblClave.Location = new Point(30, 236);
        lblClave.Size = new Size(375, 22);
        lblClave.Text = "Contraseña";
        panelAcceso.Controls.Add(lblClave);

        // txtClave
        txtClave.Name = "txtClave";
        txtClave.Location = new Point(30, 262);
        txtClave.Size = new Size(375, 29);
        txtClave.TabIndex = 1;
        txtClave.UseSystemPasswordChar = true;
        txtClave.AccessibleName = "Contraseña";
        panelAcceso.Controls.Add(txtClave);

        // btnIngresar
        btnIngresar.Name = "btnIngresar";
        btnIngresar.Location = new Point(30, 324);
        btnIngresar.Size = new Size(375, 42);
        btnIngresar.Text = "Ingresar";
        btnIngresar.TabIndex = 2;
        btnIngresar.BackColor = Color.FromArgb(43, 43, 43);
        btnIngresar.ForeColor = Color.White;
        btnIngresar.UseVisualStyleBackColor = false;
        panelAcceso.Controls.Add(btnIngresar);

        // lblNota
        lblNota.Name = "lblNota";
        lblNota.Location = new Point(30, 384);
        lblNota.Size = new Size(375, 50);
        lblNota.Text = "Acceso de demostración.\nNo se validan credenciales.";
        lblNota.ForeColor = Color.DimGray;
        panelAcceso.Controls.Add(lblNota);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 9F);
        ClientSize = new Size(900, 600);
        MinimumSize = new Size(700, 590);
        Name = "FrmAccesoVisual";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Acceso - Gestión Escolar";
        BackColor = SystemColors.Control;
        AcceptButton = btnIngresar;
        btnIngresar.Click += btnIngresar_Click;
        ResumeLayout(false);
    }
}
#endif
