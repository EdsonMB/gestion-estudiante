namespace GestionEstudiante;

partial class FrmGestionEstudiantes
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlHeader = null!;
    private Label lblTitulo = null!;
    private GroupBox grpDatos = null!;
    private Label lblCodigo = null!;
    private Label lblNombre = null!;
    private Label lblCarrera = null!;
    private Label lblCorreo = null!;
    private Label lblTelefono = null!;
    private TextBox txtCodigo = null!;
    private TextBox txtNombre = null!;
    private TextBox txtCarrera = null!;
    private TextBox txtCorreo = null!;
    private TextBox txtTelefono = null!;
    private Button btnAgregar = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
    private Button btnLimpiar = null!;
    private DataGridView dgvEstudiantes = null!;
    private DataGridViewTextBoxColumn colNro = null!;
    private DataGridViewTextBoxColumn colCodigo = null!;
    private DataGridViewTextBoxColumn colNombre = null!;
    private DataGridViewTextBoxColumn colCarrera = null!;
    private DataGridViewTextBoxColumn colCorreo = null!;
    private DataGridViewTextBoxColumn colTelefono = null!;
    private GroupBox grpCadenas = null!;
    private Label lblCadenaEntrada = null!;
    private Label lblLongitud = null!;
    private Label lblMayusculas = null!;
    private Label lblMinusculas = null!;
    private Label lblPrimeraLetra = null!;
    private Label lblUltimaLetra = null!;
    private Label lblSubcadena = null!;
    private Label lblInvertido = null!;
    private Label lblSinEspacios = null!;
    private Label lblReemplazo = null!;
    private Label lblVocales = null!;
    private TextBox txtCadenaEntrada = null!;
    private TextBox txtLongitud = null!;
    private TextBox txtMayusculas = null!;
    private TextBox txtMinusculas = null!;
    private TextBox txtPrimeraLetra = null!;
    private TextBox txtUltimaLetra = null!;
    private TextBox txtSubcadena = null!;
    private TextBox txtInvertido = null!;
    private TextBox txtSinEspacios = null!;
    private TextBox txtReemplazo = null!;
    private TextBox txtVocales = null!;
    private Button btnProcesar = null!;
    private GroupBox grpBuscar = null!;
    private Label lblBuscarNombre = null!;
    private TextBox txtBuscar = null!;
    private Button btnBuscar = null!;
    private ListBox lstResultados = null!;
    private GroupBox grpInfo = null!;
    private Label lblTotalTexto = null!;
    private Label lblCarreraTexto = null!;
    private Label lblMedicinaTexto = null!;
    private Label lblTotalValor = null!;
    private Label lblCarreraValor = null!;
    private Label lblMedicinaValor = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        pnlHeader = new Panel();
        lblTitulo = new Label();
        grpDatos = new GroupBox();
        lblCodigo = new Label();
        lblNombre = new Label();
        lblCarrera = new Label();
        lblCorreo = new Label();
        lblTelefono = new Label();
        txtCodigo = new TextBox();
        txtNombre = new TextBox();
        txtCarrera = new TextBox();
        txtCorreo = new TextBox();
        txtTelefono = new TextBox();
        btnAgregar = new Button();
        btnModificar = new Button();
        btnEliminar = new Button();
        btnLimpiar = new Button();
        dgvEstudiantes = new DataGridView();
        colNro = new DataGridViewTextBoxColumn();
        colCodigo = new DataGridViewTextBoxColumn();
        colNombre = new DataGridViewTextBoxColumn();
        colCarrera = new DataGridViewTextBoxColumn();
        colCorreo = new DataGridViewTextBoxColumn();
        colTelefono = new DataGridViewTextBoxColumn();
        grpCadenas = new GroupBox();
        lblCadenaEntrada = new Label();
        lblLongitud = new Label();
        lblMayusculas = new Label();
        lblMinusculas = new Label();
        lblPrimeraLetra = new Label();
        lblUltimaLetra = new Label();
        lblSubcadena = new Label();
        lblInvertido = new Label();
        lblSinEspacios = new Label();
        lblReemplazo = new Label();
        lblVocales = new Label();
        txtCadenaEntrada = new TextBox();
        txtLongitud = new TextBox();
        txtMayusculas = new TextBox();
        txtMinusculas = new TextBox();
        txtPrimeraLetra = new TextBox();
        txtUltimaLetra = new TextBox();
        txtSubcadena = new TextBox();
        txtInvertido = new TextBox();
        txtSinEspacios = new TextBox();
        txtReemplazo = new TextBox();
        txtVocales = new TextBox();
        btnProcesar = new Button();
        grpBuscar = new GroupBox();
        lblBuscarNombre = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        lstResultados = new ListBox();
        grpInfo = new GroupBox();
        lblTotalTexto = new Label();
        lblCarreraTexto = new Label();
        lblMedicinaTexto = new Label();
        lblTotalValor = new Label();
        lblCarreraValor = new Label();
        lblMedicinaValor = new Label();
        pnlHeader.SuspendLayout();
        grpDatos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
        grpCadenas.SuspendLayout();
        grpBuscar.SuspendLayout();
        grpInfo.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(31, 55, 82);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1548, 88);
        pnlHeader.TabIndex = 0;
        // 
        // lblTitulo
        // 
        lblTitulo.Dock = DockStyle.Fill;
        lblTitulo.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(0, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(1548, 88);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "GESTIÓN DE ESTUDIANTES";
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // grpDatos
        // 
        grpDatos.BackColor = Color.FromArgb(245, 247, 250);
        grpDatos.Controls.Add(lblCodigo);
        grpDatos.Controls.Add(lblNombre);
        grpDatos.Controls.Add(lblCarrera);
        grpDatos.Controls.Add(lblCorreo);
        grpDatos.Controls.Add(lblTelefono);
        grpDatos.Controls.Add(txtCodigo);
        grpDatos.Controls.Add(txtNombre);
        grpDatos.Controls.Add(txtCarrera);
        grpDatos.Controls.Add(txtCorreo);
        grpDatos.Controls.Add(txtTelefono);
        grpDatos.Controls.Add(btnAgregar);
        grpDatos.Controls.Add(btnModificar);
        grpDatos.Controls.Add(btnEliminar);
        grpDatos.Controls.Add(btnLimpiar);
        grpDatos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        grpDatos.ForeColor = Color.FromArgb(31, 55, 82);
        grpDatos.Location = new Point(24, 105);
        grpDatos.Name = "grpDatos";
        grpDatos.Size = new Size(405, 795);
        grpDatos.TabIndex = 1;
        grpDatos.TabStop = false;
        grpDatos.Text = "  Datos del estudiante  ";
        // 
        // lblCodigo
        // 
        lblCodigo.Font = new Font("Segoe UI", 9.5F);
        lblCodigo.ForeColor = Color.FromArgb(32, 47, 67);
        lblCodigo.Location = new Point(24, 66);
        lblCodigo.Name = "lblCodigo";
        lblCodigo.Size = new Size(105, 24);
        lblCodigo.TabIndex = 0;
        lblCodigo.Text = "Código:";
        lblCodigo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblNombre
        // 
        lblNombre.Font = new Font("Segoe UI", 9.5F);
        lblNombre.ForeColor = Color.FromArgb(32, 47, 67);
        lblNombre.Location = new Point(24, 122);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(105, 24);
        lblNombre.TabIndex = 1;
        lblNombre.Text = "Nombre:";
        lblNombre.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCarrera
        // 
        lblCarrera.Font = new Font("Segoe UI", 9.5F);
        lblCarrera.ForeColor = Color.FromArgb(32, 47, 67);
        lblCarrera.Location = new Point(24, 178);
        lblCarrera.Name = "lblCarrera";
        lblCarrera.Size = new Size(105, 24);
        lblCarrera.TabIndex = 2;
        lblCarrera.Text = "Carrera:";
        lblCarrera.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCorreo
        // 
        lblCorreo.Font = new Font("Segoe UI", 9.5F);
        lblCorreo.ForeColor = Color.FromArgb(32, 47, 67);
        lblCorreo.Location = new Point(24, 234);
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Size = new Size(105, 24);
        lblCorreo.TabIndex = 3;
        lblCorreo.Text = "Correo:";
        lblCorreo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTelefono
        // 
        lblTelefono.Font = new Font("Segoe UI", 9.5F);
        lblTelefono.ForeColor = Color.FromArgb(32, 47, 67);
        lblTelefono.Location = new Point(24, 290);
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Size = new Size(105, 24);
        lblTelefono.TabIndex = 4;
        lblTelefono.Text = "Teléfono:";
        lblTelefono.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtCodigo
        // 
        txtCodigo.BorderStyle = BorderStyle.FixedSingle;
        txtCodigo.Enabled = false;
        txtCodigo.Font = new Font("Segoe UI", 9.5F);
        txtCodigo.Location = new Point(135, 64);
        txtCodigo.Name = "txtCodigo";
        txtCodigo.Size = new Size(235, 24);
        txtCodigo.TabIndex = 5;
        // 
        // txtNombre
        // 
        txtNombre.BorderStyle = BorderStyle.FixedSingle;
        txtNombre.Font = new Font("Segoe UI", 9.5F);
        txtNombre.Location = new Point(135, 120);
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(235, 24);
        txtNombre.TabIndex = 6;
        // 
        // txtCarrera
        // 
        txtCarrera.BorderStyle = BorderStyle.FixedSingle;
        txtCarrera.Font = new Font("Segoe UI", 9.5F);
        txtCarrera.Location = new Point(135, 176);
        txtCarrera.Name = "txtCarrera";
        txtCarrera.Size = new Size(235, 24);
        txtCarrera.TabIndex = 7;
        // 
        // txtCorreo
        // 
        txtCorreo.BorderStyle = BorderStyle.FixedSingle;
        txtCorreo.Font = new Font("Segoe UI", 9.5F);
        txtCorreo.Location = new Point(135, 232);
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(235, 24);
        txtCorreo.TabIndex = 8;
        // 
        // txtTelefono
        // 
        txtTelefono.BorderStyle = BorderStyle.FixedSingle;
        txtTelefono.Font = new Font("Segoe UI", 9.5F);
        txtTelefono.Location = new Point(135, 288);
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Size = new Size(235, 24);
        txtTelefono.TabIndex = 9;
        // 
        // btnAgregar
        // 
        btnAgregar.BackColor = Color.FromArgb(41, 98, 144);
        btnAgregar.Cursor = Cursors.Hand;
        btnAgregar.FlatAppearance.BorderSize = 0;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnAgregar.ForeColor = Color.White;
        btnAgregar.Location = new Point(24, 365);
        btnAgregar.Name = "btnAgregar";
        btnAgregar.Size = new Size(346, 70);
        btnAgregar.TabIndex = 10;
        btnAgregar.Text = "+   Agregar";
        btnAgregar.UseVisualStyleBackColor = false;
        btnAgregar.Click += btnAgregar_Click;
        // 
        // btnModificar
        // 
        btnModificar.BackColor = Color.FromArgb(46, 110, 120);
        btnModificar.Cursor = Cursors.Hand;
        btnModificar.FlatAppearance.BorderSize = 0;
        btnModificar.FlatStyle = FlatStyle.Flat;
        btnModificar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnModificar.ForeColor = Color.White;
        btnModificar.Location = new Point(24, 450);
        btnModificar.Name = "btnModificar";
        btnModificar.Size = new Size(346, 70);
        btnModificar.TabIndex = 11;
        btnModificar.Text = "✎   Modificar";
        btnModificar.UseVisualStyleBackColor = false;
        btnModificar.Click += btnModificar_Click;
        // 
        // btnEliminar
        // 
        btnEliminar.BackColor = Color.FromArgb(180, 58, 58);
        btnEliminar.Cursor = Cursors.Hand;
        btnEliminar.FlatAppearance.BorderSize = 0;
        btnEliminar.FlatStyle = FlatStyle.Flat;
        btnEliminar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnEliminar.ForeColor = Color.White;
        btnEliminar.Location = new Point(24, 535);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Size = new Size(346, 70);
        btnEliminar.TabIndex = 12;
        btnEliminar.Text = "−   Eliminar";
        btnEliminar.UseVisualStyleBackColor = false;
        btnEliminar.Click += btnEliminar_Click;
        // 
        // btnLimpiar
        // 
        btnLimpiar.BackColor = Color.FromArgb(90, 100, 110);
        btnLimpiar.Cursor = Cursors.Hand;
        btnLimpiar.FlatAppearance.BorderSize = 0;
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnLimpiar.ForeColor = Color.White;
        btnLimpiar.Location = new Point(24, 620);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(346, 70);
        btnLimpiar.TabIndex = 13;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = false;
        btnLimpiar.Click += btnLimpiar_Click;
        // 
        // dgvEstudiantes
        // 
        dgvEstudiantes.AllowUserToAddRows = false;
        dgvEstudiantes.AllowUserToDeleteRows = false;
        dgvEstudiantes.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(247, 250, 253);
        dgvEstudiantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvEstudiantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvEstudiantes.BackgroundColor = Color.White;
        dgvEstudiantes.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(31, 55, 82);
        dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvEstudiantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvEstudiantes.ColumnHeadersHeight = 44;
        dgvEstudiantes.Columns.AddRange(new DataGridViewColumn[] { colNro, colCodigo, colNombre, colCarrera, colCorreo, colTelefono });
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(190, 215, 235);
        dataGridViewCellStyle3.SelectionForeColor = Color.Black;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvEstudiantes.DefaultCellStyle = dataGridViewCellStyle3;
        dgvEstudiantes.EnableHeadersVisualStyles = false;
        dgvEstudiantes.GridColor = Color.FromArgb(215, 224, 234);
        dgvEstudiantes.Location = new Point(450, 105);
        dgvEstudiantes.MultiSelect = false;
        dgvEstudiantes.Name = "dgvEstudiantes";
        dgvEstudiantes.ReadOnly = true;
        dgvEstudiantes.RowHeadersVisible = false;
        dgvEstudiantes.RowTemplate.Height = 36;
        dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEstudiantes.Size = new Size(1072, 295);
        dgvEstudiantes.TabIndex = 2;
        dgvEstudiantes.CellDoubleClick += dgvEstudiantes_CellDoubleClick;
        // 
        // colNro
        // 
        colNro.FillWeight = 35F;
        colNro.HeaderText = "N°";
        colNro.Name = "colNro";
        colNro.ReadOnly = true;
        // 
        // colCodigo
        // 
        colCodigo.FillWeight = 75F;
        colCodigo.HeaderText = "Código";
        colCodigo.Name = "colCodigo";
        colCodigo.ReadOnly = true;
        // 
        // colNombre
        // 
        colNombre.FillWeight = 145F;
        colNombre.HeaderText = "Nombre";
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        // 
        // colCarrera
        // 
        colCarrera.FillWeight = 120F;
        colCarrera.HeaderText = "Carrera";
        colCarrera.Name = "colCarrera";
        colCarrera.ReadOnly = true;
        // 
        // colCorreo
        // 
        colCorreo.FillWeight = 150F;
        colCorreo.HeaderText = "Correo";
        colCorreo.Name = "colCorreo";
        colCorreo.ReadOnly = true;
        // 
        // colTelefono
        // 
        colTelefono.FillWeight = 85F;
        colTelefono.HeaderText = "Teléfono";
        colTelefono.Name = "colTelefono";
        colTelefono.ReadOnly = true;
        // 
        // grpCadenas
        // 
        grpCadenas.BackColor = Color.FromArgb(245, 247, 250);
        grpCadenas.Controls.Add(lblCadenaEntrada);
        grpCadenas.Controls.Add(lblLongitud);
        grpCadenas.Controls.Add(lblMayusculas);
        grpCadenas.Controls.Add(lblMinusculas);
        grpCadenas.Controls.Add(lblPrimeraLetra);
        grpCadenas.Controls.Add(lblUltimaLetra);
        grpCadenas.Controls.Add(lblSubcadena);
        grpCadenas.Controls.Add(lblInvertido);
        grpCadenas.Controls.Add(lblSinEspacios);
        grpCadenas.Controls.Add(lblReemplazo);
        grpCadenas.Controls.Add(lblVocales);
        grpCadenas.Controls.Add(txtCadenaEntrada);
        grpCadenas.Controls.Add(txtLongitud);
        grpCadenas.Controls.Add(txtMayusculas);
        grpCadenas.Controls.Add(txtMinusculas);
        grpCadenas.Controls.Add(txtPrimeraLetra);
        grpCadenas.Controls.Add(txtUltimaLetra);
        grpCadenas.Controls.Add(txtSubcadena);
        grpCadenas.Controls.Add(txtInvertido);
        grpCadenas.Controls.Add(txtSinEspacios);
        grpCadenas.Controls.Add(txtReemplazo);
        grpCadenas.Controls.Add(txtVocales);
        grpCadenas.Controls.Add(btnProcesar);
        grpCadenas.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        grpCadenas.ForeColor = Color.FromArgb(31, 55, 82);
        grpCadenas.Location = new Point(450, 420);
        grpCadenas.Name = "grpCadenas";
        grpCadenas.Size = new Size(650, 480);
        grpCadenas.TabIndex = 3;
        grpCadenas.TabStop = false;
        grpCadenas.Text = "  Cadenas de caracteres  ";
        // 
        // lblCadenaEntrada
        // 
        lblCadenaEntrada.Font = new Font("Segoe UI", 9.5F);
        lblCadenaEntrada.ForeColor = Color.FromArgb(32, 47, 67);
        lblCadenaEntrada.Location = new Point(20, 38);
        lblCadenaEntrada.Name = "lblCadenaEntrada";
        lblCadenaEntrada.Size = new Size(195, 24);
        lblCadenaEntrada.TabIndex = 0;
        lblCadenaEntrada.Text = "Texto de entrada:";
        lblCadenaEntrada.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblLongitud
        // 
        lblLongitud.Font = new Font("Segoe UI", 9.5F);
        lblLongitud.ForeColor = Color.FromArgb(32, 47, 67);
        lblLongitud.Location = new Point(20, 71);
        lblLongitud.Name = "lblLongitud";
        lblLongitud.Size = new Size(195, 24);
        lblLongitud.TabIndex = 1;
        lblLongitud.Text = "Longitud:";
        lblLongitud.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblMayusculas
        // 
        lblMayusculas.Font = new Font("Segoe UI", 9.5F);
        lblMayusculas.ForeColor = Color.FromArgb(32, 47, 67);
        lblMayusculas.Location = new Point(20, 104);
        lblMayusculas.Name = "lblMayusculas";
        lblMayusculas.Size = new Size(195, 24);
        lblMayusculas.TabIndex = 2;
        lblMayusculas.Text = "Mayúsculas:";
        lblMayusculas.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblMinusculas
        // 
        lblMinusculas.Font = new Font("Segoe UI", 9.5F);
        lblMinusculas.ForeColor = Color.FromArgb(32, 47, 67);
        lblMinusculas.Location = new Point(20, 137);
        lblMinusculas.Name = "lblMinusculas";
        lblMinusculas.Size = new Size(195, 24);
        lblMinusculas.TabIndex = 3;
        lblMinusculas.Text = "Minúsculas:";
        lblMinusculas.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblPrimeraLetra
        // 
        lblPrimeraLetra.Font = new Font("Segoe UI", 9.5F);
        lblPrimeraLetra.ForeColor = Color.FromArgb(32, 47, 67);
        lblPrimeraLetra.Location = new Point(20, 170);
        lblPrimeraLetra.Name = "lblPrimeraLetra";
        lblPrimeraLetra.Size = new Size(195, 24);
        lblPrimeraLetra.TabIndex = 4;
        lblPrimeraLetra.Text = "Primera letra:";
        lblPrimeraLetra.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblUltimaLetra
        // 
        lblUltimaLetra.Font = new Font("Segoe UI", 9.5F);
        lblUltimaLetra.ForeColor = Color.FromArgb(32, 47, 67);
        lblUltimaLetra.Location = new Point(20, 203);
        lblUltimaLetra.Name = "lblUltimaLetra";
        lblUltimaLetra.Size = new Size(195, 24);
        lblUltimaLetra.TabIndex = 5;
        lblUltimaLetra.Text = "Última letra:";
        lblUltimaLetra.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSubcadena
        // 
        lblSubcadena.Font = new Font("Segoe UI", 9.5F);
        lblSubcadena.ForeColor = Color.FromArgb(32, 47, 67);
        lblSubcadena.Location = new Point(20, 236);
        lblSubcadena.Name = "lblSubcadena";
        lblSubcadena.Size = new Size(195, 24);
        lblSubcadena.TabIndex = 6;
        lblSubcadena.Text = "Subcadena (1-5):";
        lblSubcadena.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblInvertido
        // 
        lblInvertido.Font = new Font("Segoe UI", 9.5F);
        lblInvertido.ForeColor = Color.FromArgb(32, 47, 67);
        lblInvertido.Location = new Point(20, 269);
        lblInvertido.Name = "lblInvertido";
        lblInvertido.Size = new Size(195, 24);
        lblInvertido.TabIndex = 7;
        lblInvertido.Text = "Texto invertido:";
        lblInvertido.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSinEspacios
        // 
        lblSinEspacios.Font = new Font("Segoe UI", 9.5F);
        lblSinEspacios.ForeColor = Color.FromArgb(32, 47, 67);
        lblSinEspacios.Location = new Point(20, 302);
        lblSinEspacios.Name = "lblSinEspacios";
        lblSinEspacios.Size = new Size(195, 24);
        lblSinEspacios.TabIndex = 8;
        lblSinEspacios.Text = "Sin espacios:";
        lblSinEspacios.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblReemplazo
        // 
        lblReemplazo.Font = new Font("Segoe UI", 9.5F);
        lblReemplazo.ForeColor = Color.FromArgb(32, 47, 67);
        lblReemplazo.Location = new Point(20, 335);
        lblReemplazo.Name = "lblReemplazo";
        lblReemplazo.Size = new Size(195, 24);
        lblReemplazo.TabIndex = 9;
        lblReemplazo.Text = "Reemplazar mundo → C#:";
        lblReemplazo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblVocales
        // 
        lblVocales.Font = new Font("Segoe UI", 9.5F);
        lblVocales.ForeColor = Color.FromArgb(32, 47, 67);
        lblVocales.Location = new Point(20, 368);
        lblVocales.Name = "lblVocales";
        lblVocales.Size = new Size(195, 24);
        lblVocales.TabIndex = 10;
        lblVocales.Text = "Cantidad de vocales:";
        lblVocales.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtCadenaEntrada
        // 
        txtCadenaEntrada.BorderStyle = BorderStyle.FixedSingle;
        txtCadenaEntrada.Font = new Font("Segoe UI", 9.5F);
        txtCadenaEntrada.Location = new Point(220, 38);
        txtCadenaEntrada.Name = "txtCadenaEntrada";
        txtCadenaEntrada.Size = new Size(400, 24);
        txtCadenaEntrada.TabIndex = 11;
        // 
        // txtLongitud
        // 
        txtLongitud.BackColor = Color.FromArgb(248, 250, 252);
        txtLongitud.BorderStyle = BorderStyle.FixedSingle;
        txtLongitud.Font = new Font("Segoe UI", 9.5F);
        txtLongitud.Location = new Point(220, 71);
        txtLongitud.Name = "txtLongitud";
        txtLongitud.ReadOnly = true;
        txtLongitud.Size = new Size(400, 24);
        txtLongitud.TabIndex = 12;
        // 
        // txtMayusculas
        // 
        txtMayusculas.BackColor = Color.FromArgb(248, 250, 252);
        txtMayusculas.BorderStyle = BorderStyle.FixedSingle;
        txtMayusculas.Font = new Font("Segoe UI", 9.5F);
        txtMayusculas.Location = new Point(220, 104);
        txtMayusculas.Name = "txtMayusculas";
        txtMayusculas.ReadOnly = true;
        txtMayusculas.Size = new Size(400, 24);
        txtMayusculas.TabIndex = 13;
        // 
        // txtMinusculas
        // 
        txtMinusculas.BackColor = Color.FromArgb(248, 250, 252);
        txtMinusculas.BorderStyle = BorderStyle.FixedSingle;
        txtMinusculas.Font = new Font("Segoe UI", 9.5F);
        txtMinusculas.Location = new Point(220, 137);
        txtMinusculas.Name = "txtMinusculas";
        txtMinusculas.ReadOnly = true;
        txtMinusculas.Size = new Size(400, 24);
        txtMinusculas.TabIndex = 14;
        // 
        // txtPrimeraLetra
        // 
        txtPrimeraLetra.BackColor = Color.FromArgb(248, 250, 252);
        txtPrimeraLetra.BorderStyle = BorderStyle.FixedSingle;
        txtPrimeraLetra.Font = new Font("Segoe UI", 9.5F);
        txtPrimeraLetra.Location = new Point(220, 170);
        txtPrimeraLetra.Name = "txtPrimeraLetra";
        txtPrimeraLetra.ReadOnly = true;
        txtPrimeraLetra.Size = new Size(400, 24);
        txtPrimeraLetra.TabIndex = 15;
        // 
        // txtUltimaLetra
        // 
        txtUltimaLetra.BackColor = Color.FromArgb(248, 250, 252);
        txtUltimaLetra.BorderStyle = BorderStyle.FixedSingle;
        txtUltimaLetra.Font = new Font("Segoe UI", 9.5F);
        txtUltimaLetra.Location = new Point(220, 203);
        txtUltimaLetra.Name = "txtUltimaLetra";
        txtUltimaLetra.ReadOnly = true;
        txtUltimaLetra.Size = new Size(400, 24);
        txtUltimaLetra.TabIndex = 16;
        // 
        // txtSubcadena
        // 
        txtSubcadena.BackColor = Color.FromArgb(248, 250, 252);
        txtSubcadena.BorderStyle = BorderStyle.FixedSingle;
        txtSubcadena.Font = new Font("Segoe UI", 9.5F);
        txtSubcadena.Location = new Point(220, 236);
        txtSubcadena.Name = "txtSubcadena";
        txtSubcadena.ReadOnly = true;
        txtSubcadena.Size = new Size(400, 24);
        txtSubcadena.TabIndex = 17;
        // 
        // txtInvertido
        // 
        txtInvertido.BackColor = Color.FromArgb(248, 250, 252);
        txtInvertido.BorderStyle = BorderStyle.FixedSingle;
        txtInvertido.Font = new Font("Segoe UI", 9.5F);
        txtInvertido.Location = new Point(220, 269);
        txtInvertido.Name = "txtInvertido";
        txtInvertido.ReadOnly = true;
        txtInvertido.Size = new Size(400, 24);
        txtInvertido.TabIndex = 18;
        // 
        // txtSinEspacios
        // 
        txtSinEspacios.BackColor = Color.FromArgb(248, 250, 252);
        txtSinEspacios.BorderStyle = BorderStyle.FixedSingle;
        txtSinEspacios.Font = new Font("Segoe UI", 9.5F);
        txtSinEspacios.Location = new Point(220, 302);
        txtSinEspacios.Name = "txtSinEspacios";
        txtSinEspacios.ReadOnly = true;
        txtSinEspacios.Size = new Size(400, 24);
        txtSinEspacios.TabIndex = 19;
        // 
        // txtReemplazo
        // 
        txtReemplazo.BackColor = Color.FromArgb(248, 250, 252);
        txtReemplazo.BorderStyle = BorderStyle.FixedSingle;
        txtReemplazo.Font = new Font("Segoe UI", 9.5F);
        txtReemplazo.Location = new Point(220, 335);
        txtReemplazo.Name = "txtReemplazo";
        txtReemplazo.ReadOnly = true;
        txtReemplazo.Size = new Size(400, 24);
        txtReemplazo.TabIndex = 20;
        // 
        // txtVocales
        // 
        txtVocales.BackColor = Color.FromArgb(248, 250, 252);
        txtVocales.BorderStyle = BorderStyle.FixedSingle;
        txtVocales.Font = new Font("Segoe UI", 9.5F);
        txtVocales.Location = new Point(220, 368);
        txtVocales.Name = "txtVocales";
        txtVocales.ReadOnly = true;
        txtVocales.Size = new Size(400, 24);
        txtVocales.TabIndex = 21;
        // 
        // btnProcesar
        // 
        btnProcesar.BackColor = Color.FromArgb(41, 98, 144);
        btnProcesar.Cursor = Cursors.Hand;
        btnProcesar.FlatAppearance.BorderSize = 0;
        btnProcesar.FlatStyle = FlatStyle.Flat;
        btnProcesar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        btnProcesar.ForeColor = Color.White;
        btnProcesar.Location = new Point(220, 405);
        btnProcesar.Name = "btnProcesar";
        btnProcesar.Size = new Size(400, 48);
        btnProcesar.TabIndex = 22;
        btnProcesar.Text = "⚙   Procesar cadena";
        btnProcesar.UseVisualStyleBackColor = false;
        btnProcesar.Click += btnProcesar_Click;
        // 
        // grpBuscar
        // 
        grpBuscar.BackColor = Color.FromArgb(245, 247, 250);
        grpBuscar.Controls.Add(lblBuscarNombre);
        grpBuscar.Controls.Add(txtBuscar);
        grpBuscar.Controls.Add(btnBuscar);
        grpBuscar.Controls.Add(lstResultados);
        grpBuscar.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        grpBuscar.ForeColor = Color.FromArgb(31, 55, 82);
        grpBuscar.Location = new Point(1120, 420);
        grpBuscar.Name = "grpBuscar";
        grpBuscar.Size = new Size(402, 255);
        grpBuscar.TabIndex = 4;
        grpBuscar.TabStop = false;
        grpBuscar.Text = "  Buscar por nombre  ";
        // 
        // lblBuscarNombre
        // 
        lblBuscarNombre.Font = new Font("Segoe UI", 9.5F);
        lblBuscarNombre.ForeColor = Color.FromArgb(32, 47, 67);
        lblBuscarNombre.Location = new Point(20, 42);
        lblBuscarNombre.Name = "lblBuscarNombre";
        lblBuscarNombre.Size = new Size(150, 24);
        lblBuscarNombre.TabIndex = 0;
        lblBuscarNombre.Text = "Ingrese nombre:";
        // 
        // txtBuscar
        // 
        txtBuscar.BorderStyle = BorderStyle.FixedSingle;
        txtBuscar.Font = new Font("Segoe UI", 9.5F);
        txtBuscar.Location = new Point(20, 72);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Size = new Size(260, 24);
        txtBuscar.TabIndex = 1;
        // 
        // btnBuscar
        // 
        btnBuscar.BackColor = Color.FromArgb(41, 98, 144);
        btnBuscar.FlatAppearance.BorderSize = 0;
        btnBuscar.FlatStyle = FlatStyle.Flat;
        btnBuscar.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        btnBuscar.ForeColor = Color.White;
        btnBuscar.Location = new Point(290, 70);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(90, 31);
        btnBuscar.TabIndex = 2;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = false;
        btnBuscar.Click += btnBuscar_Click;
        // 
        // lstResultados
        // 
        lstResultados.BorderStyle = BorderStyle.FixedSingle;
        lstResultados.Font = new Font("Segoe UI", 9F);
        lstResultados.FormattingEnabled = true;
        lstResultados.ItemHeight = 15;
        lstResultados.Location = new Point(20, 115);
        lstResultados.Name = "lstResultados";
        lstResultados.Size = new Size(360, 107);
        lstResultados.TabIndex = 3;
        // 
        // grpInfo
        // 
        grpInfo.BackColor = Color.FromArgb(245, 247, 250);
        grpInfo.Controls.Add(lblTotalTexto);
        grpInfo.Controls.Add(lblCarreraTexto);
        grpInfo.Controls.Add(lblMedicinaTexto);
        grpInfo.Controls.Add(lblTotalValor);
        grpInfo.Controls.Add(lblCarreraValor);
        grpInfo.Controls.Add(lblMedicinaValor);
        grpInfo.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        grpInfo.ForeColor = Color.FromArgb(31, 55, 82);
        grpInfo.Location = new Point(1120, 695);
        grpInfo.Name = "grpInfo";
        grpInfo.Size = new Size(402, 205);
        grpInfo.TabIndex = 5;
        grpInfo.TabStop = false;
        grpInfo.Text = "  Información  ";
        // 
        // lblTotalTexto
        // 
        lblTotalTexto.Font = new Font("Segoe UI", 9.5F);
        lblTotalTexto.ForeColor = Color.FromArgb(32, 47, 67);
        lblTotalTexto.Location = new Point(20, 45);
        lblTotalTexto.Name = "lblTotalTexto";
        lblTotalTexto.Size = new Size(200, 25);
        lblTotalTexto.TabIndex = 0;
        lblTotalTexto.Text = "Total de estudiantes:";
        // 
        // lblCarreraTexto
        // 
        lblCarreraTexto.Font = new Font("Segoe UI", 9.5F);
        lblCarreraTexto.ForeColor = Color.FromArgb(32, 47, 67);
        lblCarreraTexto.Location = new Point(20, 95);
        lblCarreraTexto.Name = "lblCarreraTexto";
        lblCarreraTexto.Size = new Size(200, 25);
        lblCarreraTexto.TabIndex = 1;
        lblCarreraTexto.Text = "Carrera más común:";
        // 
        // lblMedicinaTexto
        // 
        lblMedicinaTexto.Font = new Font("Segoe UI", 9.5F);
        lblMedicinaTexto.ForeColor = Color.FromArgb(32, 47, 67);
        lblMedicinaTexto.Location = new Point(20, 145);
        lblMedicinaTexto.Name = "lblMedicinaTexto";
        lblMedicinaTexto.Size = new Size(200, 25);
        lblMedicinaTexto.TabIndex = 2;
        lblMedicinaTexto.Text = "Estudiantes Medicina:";
        // 
        // lblTotalValor
        // 
        lblTotalValor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblTotalValor.Location = new Point(300, 45);
        lblTotalValor.Name = "lblTotalValor";
        lblTotalValor.Size = new Size(60, 25);
        lblTotalValor.TabIndex = 3;
        lblTotalValor.Text = "0";
        lblTotalValor.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCarreraValor
        // 
        lblCarreraValor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblCarreraValor.Location = new Point(245, 95);
        lblCarreraValor.Name = "lblCarreraValor";
        lblCarreraValor.Size = new Size(115, 25);
        lblCarreraValor.TabIndex = 4;
        lblCarreraValor.Text = "-";
        lblCarreraValor.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblMedicinaValor
        // 
        lblMedicinaValor.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblMedicinaValor.Location = new Point(300, 145);
        lblMedicinaValor.Name = "lblMedicinaValor";
        lblMedicinaValor.Size = new Size(60, 25);
        lblMedicinaValor.TabIndex = 5;
        lblMedicinaValor.Text = "0";
        lblMedicinaValor.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // FrmGestionEstudiantes
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(224, 230, 237);
        ClientSize = new Size(1548, 930);
        Controls.Add(grpInfo);
        Controls.Add(grpBuscar);
        Controls.Add(grpCadenas);
        Controls.Add(dgvEstudiantes);
        Controls.Add(grpDatos);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9.5F);
        MinimumSize = new Size(1280, 800);
        Name = "FrmGestionEstudiantes";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Manejo de Cadenas y Arreglos Paralelos";
        Load += FrmGestionEstudiantes_Load;
        pnlHeader.ResumeLayout(false);
        grpDatos.ResumeLayout(false);
        grpDatos.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
        grpCadenas.ResumeLayout(false);
        grpCadenas.PerformLayout();
        grpBuscar.ResumeLayout(false);
        grpBuscar.PerformLayout();
        grpInfo.ResumeLayout(false);
        ResumeLayout(false);
    }
}
