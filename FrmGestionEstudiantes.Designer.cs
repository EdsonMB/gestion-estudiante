#nullable enable
namespace GestionEstudiante;

partial class FrmGestionEstudiantes
{
    private System.ComponentModel.IContainer? components = null;
    private Button btnAgregar = null!;
    private Button btnLimpiar = null!;
    private Button limpiarFiltro = null!;
    private Button btnModificar = null!;
    private Button btnEliminar = null!;
    private TableLayoutPanel pnlTotal = null!;
    private Label lblTotalTitulo = null!;
    private Label lblTotalValor = null!;
    private TableLayoutPanel pnlCupos = null!;
    private Label lblCuposTitulo = null!;
    private Label lblCuposValor = null!;
    private TableLayoutPanel pnlCarrera = null!;
    private Label lblCarreraTitulo = null!;
    private Label lblCarreraValor = null!;
    private Label lblCampoCodigo = null!;
    private TextBox txtCodigo = null!;
    private Label lblCampoNombre = null!;
    private TextBox txtNombre = null!;
    private Label lblCampoCarrera = null!;
    private ComboBox cboCarrera = null!;
    private Label lblCampoCorreo = null!;
    private TextBox txtCorreo = null!;
    private Label lblCampoTelefono = null!;
    private TextBox txtTelefono = null!;
    private Label lblAyuda1 = null!;
    private Label lblAyuda2 = null!;
    private DataGridViewTextBoxColumn colCodigo = null!;
    private DataGridViewTextBoxColumn colNombre = null!;
    private DataGridViewTextBoxColumn colCarrera = null!;
    private DataGridViewTextBoxColumn colCorreo = null!;
    private DataGridViewTextBoxColumn colTelefono = null!;
    private DataGridViewTextBoxColumn colFecha = null!;
    private TableLayoutPanel root = null!;
    private Label header = null!;
    private TableLayoutPanel indicadores = null!;
    private TableLayoutPanel contenido = null!;
    private TableLayoutPanel editor = null!;
    private Label lblModo = null!;
    private TableLayoutPanel listado = null!;
    private TableLayoutPanel busqueda = null!;
    private TextBox txtBuscar = null!;
    private DataGridView dgvEstudiantes = null!;
    private FlowLayoutPanel acciones = null!;
    private Label lblResultados = null!;

    // Liberamos los componentes del formulario cuando ya no se necesitan.
    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    // Armamos la pantalla por secciones: encabezado, indicadores, editor y listado.
    // Los paneles distribuyen el espacio cuando cambia el tamaño de la ventana.
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        root = new TableLayoutPanel();
        header = new Label();
        indicadores = new TableLayoutPanel();
        pnlTotal = new TableLayoutPanel();
        lblTotalTitulo = new Label();
        lblTotalValor = new Label();
        pnlCupos = new TableLayoutPanel();
        lblCuposTitulo = new Label();
        lblCuposValor = new Label();
        pnlCarrera = new TableLayoutPanel();
        lblCarreraTitulo = new Label();
        lblCarreraValor = new Label();
        contenido = new TableLayoutPanel();
        editor = new TableLayoutPanel();
        lblModo = new Label();
        lblCampoCodigo = new Label();
        txtCodigo = new TextBox();
        lblCampoNombre = new Label();
        txtNombre = new TextBox();
        lblCampoCarrera = new Label();
        cboCarrera = new ComboBox();
        lblCampoCorreo = new Label();
        txtCorreo = new TextBox();
        lblCampoTelefono = new Label();
        txtTelefono = new TextBox();
        btnAgregar = new Button();
        btnLimpiar = new Button();
        lblAyuda1 = new Label();
        listado = new TableLayoutPanel();
        lblAyuda2 = new Label();
        busqueda = new TableLayoutPanel();
        txtBuscar = new TextBox();
        limpiarFiltro = new Button();
        dgvEstudiantes = new DataGridView();
        colCodigo = new DataGridViewTextBoxColumn();
        colNombre = new DataGridViewTextBoxColumn();
        colCarrera = new DataGridViewTextBoxColumn();
        colCorreo = new DataGridViewTextBoxColumn();
        colTelefono = new DataGridViewTextBoxColumn();
        colFecha = new DataGridViewTextBoxColumn();
        acciones = new FlowLayoutPanel();
        btnModificar = new Button();
        btnEliminar = new Button();
        lblResultados = new Label();
        root.SuspendLayout();
        indicadores.SuspendLayout();
        pnlTotal.SuspendLayout();
        pnlCupos.SuspendLayout();
        pnlCarrera.SuspendLayout();
        contenido.SuspendLayout();
        editor.SuspendLayout();
        listado.SuspendLayout();
        busqueda.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).BeginInit();
        acciones.SuspendLayout();
        SuspendLayout();
        // 
        // root
        // 
        root.ColumnCount = 1;
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        root.Controls.Add(header, 0, 0);
        root.Controls.Add(indicadores, 0, 1);
        root.Controls.Add(contenido, 0, 2);
        root.Dock = DockStyle.Fill;
        root.Location = new Point(0, 0);
        root.Name = "root";
        root.RowCount = 3;
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.Size = new Size(1320, 790);
        root.TabIndex = 0;
        // 
        // header
        // 
        header.BackColor = Color.FromArgb(31, 55, 82);
        header.Dock = DockStyle.Fill;
        header.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        header.ForeColor = Color.White;
        header.Location = new Point(0, 0);
        header.Margin = new Padding(0);
        header.Name = "header";
        header.Padding = new Padding(28, 0, 0, 0);
        header.Size = new Size(1320, 90);
        header.TabIndex = 0;
        header.Text = "Gestión de estudiantes";
        header.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // indicadores
        // 
        indicadores.ColumnCount = 3;
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        indicadores.Controls.Add(pnlTotal, 0, 0);
        indicadores.Controls.Add(pnlCupos, 1, 0);
        indicadores.Controls.Add(pnlCarrera, 2, 0);
        indicadores.Dock = DockStyle.Fill;
        indicadores.Location = new Point(3, 93);
        indicadores.Name = "indicadores";
        indicadores.Padding = new Padding(24, 16, 24, 8);
        indicadores.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        indicadores.Size = new Size(1314, 98);
        indicadores.TabIndex = 1;
        // 
        // pnlTotal
        // 
        pnlTotal.BackColor = Color.White;
        pnlTotal.ColumnCount = 2;
        pnlTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        pnlTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        pnlTotal.Controls.Add(lblTotalTitulo, 0, 0);
        pnlTotal.Controls.Add(lblTotalValor, 1, 0);
        pnlTotal.Dock = DockStyle.Fill;
        pnlTotal.Location = new Point(24, 16);
        pnlTotal.Margin = new Padding(0, 0, 12, 0);
        pnlTotal.Name = "pnlTotal";
        pnlTotal.Padding = new Padding(14);
        pnlTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        pnlTotal.Size = new Size(342, 74);
        pnlTotal.TabIndex = 0;
        // 
        // lblTotalTitulo
        // 
        lblTotalTitulo.Dock = DockStyle.Fill;
        lblTotalTitulo.Location = new Point(17, 14);
        lblTotalTitulo.Name = "lblTotalTitulo";
        lblTotalTitulo.Size = new Size(182, 46);
        lblTotalTitulo.TabIndex = 0;
        lblTotalTitulo.Text = "Estudiantes";
        lblTotalTitulo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTotalValor
        // 
        lblTotalValor.AutoEllipsis = true;
        lblTotalValor.Dock = DockStyle.Fill;
        lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalValor.Location = new Point(205, 14);
        lblTotalValor.Name = "lblTotalValor";
        lblTotalValor.Size = new Size(120, 46);
        lblTotalValor.TabIndex = 1;
        lblTotalValor.Text = "0";
        lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
        // 
        // pnlCupos
        // 
        pnlCupos.BackColor = Color.White;
        pnlCupos.ColumnCount = 2;
        pnlCupos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        pnlCupos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        pnlCupos.Controls.Add(lblCuposTitulo, 0, 0);
        pnlCupos.Controls.Add(lblCuposValor, 1, 0);
        pnlCupos.Dock = DockStyle.Fill;
        pnlCupos.Location = new Point(378, 16);
        pnlCupos.Margin = new Padding(0, 0, 12, 0);
        pnlCupos.Name = "pnlCupos";
        pnlCupos.Padding = new Padding(14);
        pnlCupos.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        pnlCupos.Size = new Size(342, 74);
        pnlCupos.TabIndex = 1;
        // 
        // lblCuposTitulo
        // 
        lblCuposTitulo.Dock = DockStyle.Fill;
        lblCuposTitulo.Location = new Point(17, 14);
        lblCuposTitulo.Name = "lblCuposTitulo";
        lblCuposTitulo.Size = new Size(182, 46);
        lblCuposTitulo.TabIndex = 0;
        lblCuposTitulo.Text = "Cupos libres";
        lblCuposTitulo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCuposValor
        // 
        lblCuposValor.AutoEllipsis = true;
        lblCuposValor.Dock = DockStyle.Fill;
        lblCuposValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCuposValor.Location = new Point(205, 14);
        lblCuposValor.Name = "lblCuposValor";
        lblCuposValor.Size = new Size(120, 46);
        lblCuposValor.TabIndex = 1;
        lblCuposValor.Text = "0";
        lblCuposValor.TextAlign = ContentAlignment.MiddleRight;
        // 
        // pnlCarrera
        // 
        pnlCarrera.BackColor = Color.White;
        pnlCarrera.ColumnCount = 2;
        pnlCarrera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        pnlCarrera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        pnlCarrera.Controls.Add(lblCarreraTitulo, 0, 0);
        pnlCarrera.Controls.Add(lblCarreraValor, 1, 0);
        pnlCarrera.Dock = DockStyle.Fill;
        pnlCarrera.Location = new Point(732, 16);
        pnlCarrera.Margin = new Padding(0, 0, 12, 0);
        pnlCarrera.Name = "pnlCarrera";
        pnlCarrera.Padding = new Padding(14);
        pnlCarrera.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        pnlCarrera.Size = new Size(546, 74);
        pnlCarrera.TabIndex = 2;
        // 
        // lblCarreraTitulo
        // 
        lblCarreraTitulo.Dock = DockStyle.Fill;
        lblCarreraTitulo.Location = new Point(17, 14);
        lblCarreraTitulo.Name = "lblCarreraTitulo";
        lblCarreraTitulo.Size = new Size(304, 46);
        lblCarreraTitulo.TabIndex = 0;
        lblCarreraTitulo.Text = "Carrera con más estudiantes";
        lblCarreraTitulo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCarreraValor
        // 
        lblCarreraValor.AutoEllipsis = true;
        lblCarreraValor.Dock = DockStyle.Fill;
        lblCarreraValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCarreraValor.Location = new Point(327, 14);
        lblCarreraValor.Name = "lblCarreraValor";
        lblCarreraValor.Size = new Size(202, 46);
        lblCarreraValor.TabIndex = 1;
        lblCarreraValor.Text = "0";
        lblCarreraValor.TextAlign = ContentAlignment.MiddleRight;
        // 
        // contenido
        // 
        contenido.ColumnCount = 2;
        contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 310F));
        contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        contenido.Controls.Add(editor, 0, 0);
        contenido.Controls.Add(listado, 1, 0);
        contenido.Dock = DockStyle.Fill;
        contenido.Location = new Point(3, 197);
        contenido.Name = "contenido";
        contenido.Padding = new Padding(24, 8, 24, 20);
        contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        contenido.Size = new Size(1314, 590);
        contenido.TabIndex = 2;
        // 
        // editor
        // 
        editor.AutoScroll = true;
        editor.BackColor = Color.White;
        editor.ColumnCount = 1;
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        editor.Controls.Add(lblModo, 0, 0);
        editor.Controls.Add(lblCampoCodigo, 0, 1);
        editor.Controls.Add(txtCodigo, 0, 2);
        editor.Controls.Add(lblCampoNombre, 0, 3);
        editor.Controls.Add(txtNombre, 0, 4);
        editor.Controls.Add(lblCampoCarrera, 0, 5);
        editor.Controls.Add(cboCarrera, 0, 6);
        editor.Controls.Add(lblCampoCorreo, 0, 7);
        editor.Controls.Add(txtCorreo, 0, 8);
        editor.Controls.Add(lblCampoTelefono, 0, 9);
        editor.Controls.Add(txtTelefono, 0, 10);
        editor.Controls.Add(btnAgregar, 0, 11);
        editor.Controls.Add(btnLimpiar, 0, 12);
        editor.Controls.Add(lblAyuda1, 0, 13);
        editor.Dock = DockStyle.Fill;
        editor.Location = new Point(24, 8);
        editor.Margin = new Padding(0, 0, 20, 0);
        editor.Name = "editor";
        editor.Padding = new Padding(20);
        editor.RowCount = 15;
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle());
        editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        editor.Size = new Size(290, 562);
        editor.TabIndex = 0;
        // 
        // lblModo
        // 
        lblModo.AutoSize = true;
        lblModo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblModo.Location = new Point(20, 20);
        lblModo.Margin = new Padding(0, 0, 0, 12);
        lblModo.Name = "lblModo";
        lblModo.Size = new Size(169, 25);
        lblModo.TabIndex = 0;
        lblModo.Text = "Nuevo estudiante";
        // 
        // lblCampoCodigo
        // 
        lblCampoCodigo.AutoSize = true;
        lblCampoCodigo.Location = new Point(20, 63);
        lblCampoCodigo.Margin = new Padding(0, 6, 0, 4);
        lblCampoCodigo.Name = "lblCampoCodigo";
        lblCampoCodigo.Size = new Size(53, 19);
        lblCampoCodigo.TabIndex = 1;
        lblCampoCodigo.Text = "Código";
        // 
        // txtCodigo
        // 
        txtCodigo.Dock = DockStyle.Top;
        txtCodigo.Location = new Point(20, 86);
        txtCodigo.Margin = new Padding(0, 0, 16, 8);
        txtCodigo.Name = "txtCodigo";
        txtCodigo.PlaceholderText = "Se genera al guardar";
        txtCodigo.ReadOnly = true;
        txtCodigo.Size = new Size(234, 25);
        txtCodigo.TabIndex = 2;
        txtCodigo.TabStop = false;
        // 
        // lblCampoNombre
        // 
        lblCampoNombre.AutoSize = true;
        lblCampoNombre.Location = new Point(20, 125);
        lblCampoNombre.Margin = new Padding(0, 6, 0, 4);
        lblCampoNombre.Name = "lblCampoNombre";
        lblCampoNombre.Size = new Size(130, 19);
        lblCampoNombre.TabIndex = 3;
        lblCampoNombre.Text = "Nombre completo *";
        // 
        // txtNombre
        // 
        txtNombre.CharacterCasing = CharacterCasing.Upper;
        txtNombre.Dock = DockStyle.Top;
        txtNombre.Location = new Point(20, 148);
        txtNombre.Margin = new Padding(0, 0, 16, 8);
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(234, 25);
        txtNombre.TabIndex = 4;
        // 
        // lblCampoCarrera
        // 
        lblCampoCarrera.AutoSize = true;
        lblCampoCarrera.Location = new Point(20, 187);
        lblCampoCarrera.Margin = new Padding(0, 6, 0, 4);
        lblCampoCarrera.Name = "lblCampoCarrera";
        lblCampoCarrera.Size = new Size(64, 19);
        lblCampoCarrera.TabIndex = 5;
        lblCampoCarrera.Text = "Carrera *";
        // 
        // cboCarrera
        // 
        cboCarrera.Dock = DockStyle.Top;
        cboCarrera.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCarrera.DropDownWidth = 360;
        cboCarrera.Items.AddRange(new object[] { "ADMINISTRACIÓN", "CONTABILIDAD", "DERECHO", "INGENIERÍA DE SISTEMAS", "INGENIERÍA INDUSTRIAL" });
        cboCarrera.Location = new Point(20, 210);
        cboCarrera.Margin = new Padding(0, 0, 16, 8);
        cboCarrera.Name = "cboCarrera";
        cboCarrera.Size = new Size(234, 25);
        cboCarrera.TabIndex = 6;
        // 
        // lblCampoCorreo
        // 
        lblCampoCorreo.AutoSize = true;
        lblCampoCorreo.Location = new Point(20, 249);
        lblCampoCorreo.Margin = new Padding(0, 6, 0, 4);
        lblCampoCorreo.Name = "lblCampoCorreo";
        lblCampoCorreo.Size = new Size(121, 19);
        lblCampoCorreo.TabIndex = 7;
        lblCampoCorreo.Text = "Correo electrónico";
        // 
        // txtCorreo
        // 
        txtCorreo.CharacterCasing = CharacterCasing.Upper;
        txtCorreo.Dock = DockStyle.Top;
        txtCorreo.Location = new Point(20, 272);
        txtCorreo.Margin = new Padding(0, 0, 16, 8);
        txtCorreo.Name = "txtCorreo";
        txtCorreo.Size = new Size(234, 25);
        txtCorreo.TabIndex = 8;
        // 
        // lblCampoTelefono
        // 
        lblCampoTelefono.AutoSize = true;
        lblCampoTelefono.Location = new Point(20, 311);
        lblCampoTelefono.Margin = new Padding(0, 6, 0, 4);
        lblCampoTelefono.Name = "lblCampoTelefono";
        lblCampoTelefono.Size = new Size(60, 19);
        lblCampoTelefono.TabIndex = 9;
        lblCampoTelefono.Text = "Teléfono";
        // 
        // txtTelefono
        // 
        txtTelefono.Dock = DockStyle.Top;
        txtTelefono.Location = new Point(20, 334);
        txtTelefono.Margin = new Padding(0, 0, 16, 8);
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Size = new Size(234, 25);
        txtTelefono.TabIndex = 10;
        // 
        // btnAgregar
        // 
        btnAgregar.AutoSize = true;
        btnAgregar.BackColor = Color.FromArgb(41, 98, 144);
        btnAgregar.Cursor = Cursors.Hand;
        btnAgregar.Dock = DockStyle.Top;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.ForeColor = Color.White;
        btnAgregar.Location = new Point(20, 371);
        btnAgregar.Margin = new Padding(0, 4, 10, 4);
        btnAgregar.MinimumSize = new Size(110, 36);
        btnAgregar.Name = "btnAgregar";
        btnAgregar.Padding = new Padding(8, 2, 8, 2);
        btnAgregar.Size = new Size(240, 36);
        btnAgregar.TabIndex = 11;
        btnAgregar.Text = "Agregar estudiante";
        btnAgregar.UseVisualStyleBackColor = false;
        btnAgregar.Click += btnAgregar_Click;
        // 
        // btnLimpiar
        // 
        btnLimpiar.AutoSize = true;
        btnLimpiar.BackColor = Color.White;
        btnLimpiar.Cursor = Cursors.Hand;
        btnLimpiar.Dock = DockStyle.Top;
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.ForeColor = Color.FromArgb(31, 55, 82);
        btnLimpiar.Location = new Point(20, 415);
        btnLimpiar.Margin = new Padding(0, 4, 10, 4);
        btnLimpiar.MinimumSize = new Size(110, 36);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Padding = new Padding(8, 2, 8, 2);
        btnLimpiar.Size = new Size(240, 36);
        btnLimpiar.TabIndex = 12;
        btnLimpiar.Text = "Limpiar / Cancelar";
        btnLimpiar.UseVisualStyleBackColor = false;
        btnLimpiar.Click += btnLimpiar_Click;
        // 
        // lblAyuda1
        // 
        lblAyuda1.AutoSize = true;
        lblAyuda1.ForeColor = Color.DimGray;
        lblAyuda1.Location = new Point(20, 465);
        lblAyuda1.Margin = new Padding(0, 10, 0, 0);
        lblAyuda1.Name = "lblAyuda1";
        lblAyuda1.Size = new Size(145, 19);
        lblAyuda1.TabIndex = 13;
        lblAyuda1.Text = "* Campos obligatorios";
        // 
        // listado
        // 
        listado.BackColor = Color.White;
        listado.ColumnCount = 1;
        listado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        listado.Controls.Add(lblAyuda2, 0, 0);
        listado.Controls.Add(busqueda, 0, 1);
        listado.Controls.Add(dgvEstudiantes, 0, 2);
        listado.Controls.Add(acciones, 0, 3);
        listado.Controls.Add(lblResultados, 0, 4);
        listado.Dock = DockStyle.Fill;
        listado.Location = new Point(334, 8);
        listado.Margin = new Padding(0);
        listado.Name = "listado";
        listado.Padding = new Padding(16);
        listado.RowCount = 5;
        listado.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        listado.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        listado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        listado.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        listado.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        listado.Size = new Size(956, 562);
        listado.TabIndex = 1;
        // 
        // lblAyuda2
        // 
        lblAyuda2.Dock = DockStyle.Fill;
        lblAyuda2.Location = new Point(19, 16);
        lblAyuda2.Name = "lblAyuda2";
        lblAyuda2.Size = new Size(918, 52);
        lblAyuda2.TabIndex = 0;
        lblAyuda2.Text = "Estudiantes\nHaga doble clic en una fila para editar sus datos.";
        // 
        // busqueda
        // 
        busqueda.ColumnCount = 2;
        busqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        busqueda.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        busqueda.Controls.Add(txtBuscar, 0, 0);
        busqueda.Controls.Add(limpiarFiltro, 1, 0);
        busqueda.Dock = DockStyle.Fill;
        busqueda.Location = new Point(19, 71);
        busqueda.Name = "busqueda";
        busqueda.RowCount = 1;
        busqueda.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        busqueda.Size = new Size(918, 46);
        busqueda.TabIndex = 1;
        // 
        // txtBuscar
        // 
        txtBuscar.Dock = DockStyle.Fill;
        txtBuscar.Location = new Point(0, 6);
        txtBuscar.Margin = new Padding(0, 6, 12, 6);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.PlaceholderText = "Buscar por nombre o código…";
        txtBuscar.Size = new Size(776, 25);
        txtBuscar.TabIndex = 0;
        txtBuscar.TextChanged += txtBuscar_TextChanged;
        // 
        // limpiarFiltro
        // 
        limpiarFiltro.BackColor = Color.White;
        limpiarFiltro.Cursor = Cursors.Hand;
        limpiarFiltro.FlatStyle = FlatStyle.Flat;
        limpiarFiltro.ForeColor = Color.FromArgb(31, 55, 82);
        limpiarFiltro.Location = new Point(788, 4);
        limpiarFiltro.Margin = new Padding(0, 4, 10, 4);
        limpiarFiltro.MinimumSize = new Size(110, 36);
        limpiarFiltro.Name = "limpiarFiltro";
        limpiarFiltro.Padding = new Padding(8, 2, 8, 2);
        limpiarFiltro.Size = new Size(120, 36);
        limpiarFiltro.TabIndex = 1;
        limpiarFiltro.Text = "Limpiar filtro";
        limpiarFiltro.UseVisualStyleBackColor = false;
        limpiarFiltro.Click += btnLimpiarFiltro_Click;
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
        dataGridViewCellStyle2.BackColor = Color.FromArgb(244, 247, 250);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 55, 82);
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvEstudiantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvEstudiantes.ColumnHeadersHeight = 40;
        dgvEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvEstudiantes.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colNombre, colCarrera, colCorreo, colTelefono, colFecha });
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(224, 237, 249);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(31, 55, 82);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvEstudiantes.DefaultCellStyle = dataGridViewCellStyle3;
        dgvEstudiantes.Dock = DockStyle.Fill;
        dgvEstudiantes.EnableHeadersVisualStyles = false;
        dgvEstudiantes.Location = new Point(19, 123);
        dgvEstudiantes.MultiSelect = false;
        dgvEstudiantes.Name = "dgvEstudiantes";
        dgvEstudiantes.ReadOnly = true;
        dgvEstudiantes.RowHeadersVisible = false;
        dgvEstudiantes.RowTemplate.Height = 38;
        dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEstudiantes.Size = new Size(918, 318);
        dgvEstudiantes.TabIndex = 2;
        dgvEstudiantes.CellDoubleClick += dgvEstudiantes_CellDoubleClick;
        // 
        // colCodigo
        // 
        colCodigo.FillWeight = 90F;
        colCodigo.HeaderText = "Código";
        colCodigo.MinimumWidth = 90;
        colCodigo.Name = "colCodigo";
        colCodigo.ReadOnly = true;
        colCodigo.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colNombre
        // 
        colNombre.FillWeight = 150F;
        colNombre.HeaderText = "Nombre";
        colNombre.MinimumWidth = 150;
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colCarrera
        // 
        colCarrera.FillWeight = 120F;
        colCarrera.HeaderText = "Carrera";
        colCarrera.MinimumWidth = 120;
        colCarrera.Name = "colCarrera";
        colCarrera.ReadOnly = true;
        colCarrera.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colCorreo
        // 
        colCorreo.FillWeight = 160F;
        colCorreo.HeaderText = "Correo";
        colCorreo.MinimumWidth = 160;
        colCorreo.Name = "colCorreo";
        colCorreo.ReadOnly = true;
        colCorreo.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colTelefono
        // 
        colTelefono.HeaderText = "Teléfono";
        colTelefono.MinimumWidth = 100;
        colTelefono.Name = "colTelefono";
        colTelefono.ReadOnly = true;
        colTelefono.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colFecha
        // 
        colFecha.FillWeight = 130F;
        colFecha.HeaderText = "Fecha de creación";
        colFecha.MinimumWidth = 130;
        colFecha.Name = "colFecha";
        colFecha.ReadOnly = true;
        colFecha.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // acciones
        // 
        acciones.Controls.Add(btnModificar);
        acciones.Controls.Add(btnEliminar);
        acciones.Dock = DockStyle.Fill;
        acciones.Location = new Point(19, 447);
        acciones.Name = "acciones";
        acciones.Size = new Size(918, 48);
        acciones.TabIndex = 3;
        // 
        // btnModificar
        // 
        btnModificar.AutoSize = true;
        btnModificar.BackColor = Color.FromArgb(41, 98, 144);
        btnModificar.Cursor = Cursors.Hand;
        btnModificar.Enabled = false;
        btnModificar.FlatStyle = FlatStyle.Flat;
        btnModificar.ForeColor = Color.White;
        btnModificar.Location = new Point(0, 4);
        btnModificar.Margin = new Padding(0, 4, 10, 4);
        btnModificar.MinimumSize = new Size(110, 36);
        btnModificar.Name = "btnModificar";
        btnModificar.Padding = new Padding(8, 2, 8, 2);
        btnModificar.Size = new Size(141, 36);
        btnModificar.TabIndex = 0;
        btnModificar.Text = "Guardar cambios";
        btnModificar.UseVisualStyleBackColor = false;
        btnModificar.Click += btnModificar_Click;
        // 
        // btnEliminar
        // 
        btnEliminar.AutoSize = true;
        btnEliminar.BackColor = Color.White;
        btnEliminar.Cursor = Cursors.Hand;
        btnEliminar.Enabled = false;
        btnEliminar.FlatStyle = FlatStyle.Flat;
        btnEliminar.ForeColor = Color.Firebrick;
        btnEliminar.Location = new Point(151, 4);
        btnEliminar.Margin = new Padding(0, 4, 10, 4);
        btnEliminar.MinimumSize = new Size(110, 36);
        btnEliminar.Name = "btnEliminar";
        btnEliminar.Padding = new Padding(8, 2, 8, 2);
        btnEliminar.Size = new Size(166, 36);
        btnEliminar.TabIndex = 1;
        btnEliminar.Text = "Eliminar seleccionado";
        btnEliminar.UseVisualStyleBackColor = false;
        btnEliminar.Click += btnEliminar_Click;
        // 
        // lblResultados
        // 
        lblResultados.Dock = DockStyle.Fill;
        lblResultados.ForeColor = Color.DimGray;
        lblResultados.Location = new Point(19, 498);
        lblResultados.Name = "lblResultados";
        lblResultados.Size = new Size(918, 48);
        lblResultados.TabIndex = 4;
        // 
        // FrmGestionEstudiantes
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(237, 242, 247);
        ClientSize = new Size(1320, 790);
        Controls.Add(root);
        Font = new Font("Segoe UI", 10F);
        MinimumSize = new Size(1050, 720);
        Name = "FrmGestionEstudiantes";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Gestión de estudiantes";
        Load += FrmGestionEstudiantes_Load;
        root.ResumeLayout(false);
        indicadores.ResumeLayout(false);
        pnlTotal.ResumeLayout(false);
        pnlCupos.ResumeLayout(false);
        pnlCarrera.ResumeLayout(false);
        contenido.ResumeLayout(false);
        editor.ResumeLayout(false);
        editor.PerformLayout();
        listado.ResumeLayout(false);
        busqueda.ResumeLayout(false);
        busqueda.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEstudiantes).EndInit();
        acciones.ResumeLayout(false);
        acciones.PerformLayout();
        ResumeLayout(false);
    }

}
