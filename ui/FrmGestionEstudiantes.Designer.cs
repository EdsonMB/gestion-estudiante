#nullable enable
namespace GestionEstudiante;
partial class FrmGestionEstudiantes
{
    private DataGridViewTextBoxColumn colCodigo = null!;
    private DataGridViewTextBoxColumn colNombre = null!;
    private DataGridViewTextBoxColumn colApoderado = null!;
    private DataGridViewTextBoxColumn colTelefono = null!;
    private DataGridViewTextBoxColumn colEstado = null!;
    private DataGridViewButtonColumn colEditar = null!;
    private DataGridViewButtonColumn colBaja = null!;
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private TextBox txtBuscar = null!;
    private ComboBox cboEstado = null!;
    private Button btnNuevo = null!;
    private Button btnBuscar = null!;
    private DataGridView dgvEstudiantes = null!;
    private Label lblResultados = null!;
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
        txtBuscar = new TextBox();
        cboEstado = new ComboBox();
        btnNuevo = new Button();
        btnBuscar = new Button();
        dgvEstudiantes = new DataGridView();
        lblResultados = new Label();
        colCodigo = new DataGridViewTextBoxColumn();
        colCodigo.Name = "colCodigo";
        colCodigo.HeaderText = "Código";
        colNombre = new DataGridViewTextBoxColumn();
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Estudiante";
        colApoderado = new DataGridViewTextBoxColumn();
        colApoderado.Name = "colApoderado";
        colApoderado.HeaderText = "Apoderado";
        colTelefono = new DataGridViewTextBoxColumn();
        colTelefono.Name = "colTelefono";
        colTelefono.HeaderText = "Teléfono";
        colEstado = new DataGridViewTextBoxColumn();
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEditar = new DataGridViewButtonColumn();
        colEditar.Name = "colEditar";
        colEditar.HeaderText = "Editar";
        colBaja = new DataGridViewButtonColumn();
        colBaja.Name = "colBaja";
        colBaja.HeaderText = "Dar de baja";
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
        SuspendLayout();
        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(700, 42);
        lblTitulo.Text = "Estudiantes";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        this.Controls.Add(lblTitulo);
        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 72);
        lblDescripcion.Size = new Size(900, 28);
        lblDescripcion.Text = "Consulta sus datos y mantén actualizado el contacto de sus apoderados.";
        this.Controls.Add(lblDescripcion);
        // txtBuscar
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Location = new Point(28, 126);
        txtBuscar.Size = new Size(420, 32);
        txtBuscar.PlaceholderText = "Buscar por código, nombres o apellidos";
        this.Controls.Add(txtBuscar);
        // cboEstado
        cboEstado.Name = "cboEstado";
        cboEstado.Location = new Point(560, 126);
        cboEstado.Size = new Size(180, 32);
        cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        this.Controls.Add(cboEstado);
        // btnNuevo
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Location = new Point(758, 121);
        btnNuevo.Size = new Size(185, 40);
        btnNuevo.Text = "+ Nuevo estudiante";
        this.Controls.Add(btnNuevo);
        // btnBuscar
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Location = new Point(462, 121);
        btnBuscar.Size = new Size(84, 40);
        btnBuscar.Text = "Buscar";
        this.Controls.Add(btnBuscar);
        // dgvEstudiantes
        dgvEstudiantes.Name = "dgvEstudiantes";
        dgvEstudiantes.Location = new Point(28, 186);
        dgvEstudiantes.Size = new Size(1020, 470);
        dgvEstudiantes.ReadOnly = true;
        dgvEstudiantes.AllowUserToAddRows = false;
        dgvEstudiantes.AllowUserToDeleteRows = false;
        dgvEstudiantes.AutoGenerateColumns = false;
        dgvEstudiantes.RowHeadersVisible = false;
        dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEstudiantes.MultiSelect = false;
        dgvEstudiantes.BackgroundColor = Color.White;
        dgvEstudiantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvEstudiantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(dgvEstudiantes);
        // lblResultados
        lblResultados.Name = "lblResultados";
        lblResultados.Location = new Point(28, 678);
        lblResultados.Size = new Size(900, 30);
        lblResultados.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        this.Controls.Add(lblResultados);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 750);
        BackColor = Color.FromArgb(245, 245, 245);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmGestionEstudiantes";
        Text = "Estudiantes";
        cboEstado.Items.AddRange(new object[] { "Todos los estados", "Activos", "Inactivos" });
        cboEstado.SelectedIndex = 0;
        dgvEstudiantes.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colNombre, colApoderado, colTelefono, colEstado, colEditar, colBaja });
        txtBuscar.TextChanged += FiltroCambiado;
        cboEstado.SelectedIndexChanged += FiltroCambiado;
        btnNuevo.Click += btnNuevo_Click;
        btnBuscar.Click += btnBuscar_Click;
        AcceptButton = btnBuscar;
        dgvEstudiantes.CellContentClick += dgvEstudiantes_CellContentClick;
        Load += FrmGestionEstudiantes_Load;
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
