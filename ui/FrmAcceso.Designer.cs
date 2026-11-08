#nullable enable
namespace GestionEstudiante;
partial class FrmAcceso
{
    private System.ComponentModel.IContainer? components = null;
    private Panel panelAcceso = null!;
    private Label lblMarca = null!;
    private Label lblTitulo = null!;
    private Label lblUsuario = null!;
    private TextBox txtUsuario = null!;
    private Label lblClave = null!;
    private TextBox txtClave = null!;
    private Button btnIngresar = null!;
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
        panelAcceso = new Panel();
        lblMarca = new Label();
        lblTitulo = new Label();
        lblUsuario = new Label();
        txtUsuario = new TextBox();
        lblClave = new Label();
        txtClave = new TextBox();
        btnIngresar = new Button();
        panelAcceso.SuspendLayout();
        SuspendLayout();
        // 
        // panelAcceso
        // 
        panelAcceso.BackColor = Color.White;
        panelAcceso.Controls.Add(lblMarca);
        panelAcceso.Controls.Add(lblTitulo);
        panelAcceso.Controls.Add(lblUsuario);
        panelAcceso.Controls.Add(txtUsuario);
        panelAcceso.Controls.Add(lblClave);
        panelAcceso.Controls.Add(txtClave);
        panelAcceso.Controls.Add(btnIngresar);
        panelAcceso.Dock = DockStyle.Fill;
        panelAcceso.Location = new Point(0, 0);
        panelAcceso.Name = "panelAcceso";
        panelAcceso.Size = new Size(440, 371);
        panelAcceso.TabIndex = 0;
        // 
        // lblMarca
        // 
        lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblMarca.Location = new Point(30, 25);
        lblMarca.Name = "lblMarca";
        lblMarca.Size = new Size(375, 35);
        lblMarca.TabIndex = 0;
        lblMarca.Text = "Gestión Escolar";
        // 
        // lblTitulo
        // 
        lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitulo.Location = new Point(30, 72);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(375, 30);
        lblTitulo.TabIndex = 1;
        lblTitulo.Text = "Inicio de sesión";
        // 
        // lblUsuario
        // 
        lblUsuario.Location = new Point(30, 124);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(375, 22);
        lblUsuario.TabIndex = 3;
        lblUsuario.Text = "Usuario";
        // 
        // txtUsuario
        // 
        txtUsuario.AccessibleName = "Usuario";
        txtUsuario.Location = new Point(30, 150);
        txtUsuario.Name = "txtUsuario";
        txtUsuario.Size = new Size(375, 23);
        txtUsuario.TabIndex = 0;
        // 
        // lblClave
        // 
        lblClave.Location = new Point(30, 198);
        lblClave.Name = "lblClave";
        lblClave.Size = new Size(375, 22);
        lblClave.TabIndex = 4;
        lblClave.Text = "Contraseña";
        // 
        // txtClave
        // 
        txtClave.AccessibleName = "Contraseña";
        txtClave.Location = new Point(30, 224);
        txtClave.Name = "txtClave";
        txtClave.Size = new Size(375, 23);
        txtClave.TabIndex = 1;
        txtClave.UseSystemPasswordChar = true;
        // 
        // btnIngresar
        // 
        btnIngresar.BackColor = Color.FromArgb(43, 43, 43);
        btnIngresar.ForeColor = Color.White;
        btnIngresar.Location = new Point(30, 286);
        btnIngresar.Name = "btnIngresar";
        btnIngresar.Size = new Size(375, 42);
        btnIngresar.TabIndex = 2;
        btnIngresar.Text = "Ingresar";
        btnIngresar.UseVisualStyleBackColor = false;
        btnIngresar.Click += btnIngresar_Click;
        // 
        // FrmAcceso
        // 
        AcceptButton = btnIngresar;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(440, 371);
        Controls.Add(panelAcceso);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "FrmAcceso";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Acceso - Gestión Escolar";
        panelAcceso.ResumeLayout(false);
        panelAcceso.PerformLayout();
        ResumeLayout(false);
    }
}
