#nullable enable
namespace GestionEstudiante;

partial class FormAsistencia
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private Label lblAula = null!;
    private ComboBox cmbAula = null!;
    private Label lblFecha = null!;
    private DateTimePicker dtpFecha = null!;
    private Button btnGuardar = null!;
    private DataGridView dgvAsistencia = null!;
    private Label lblNota = null!;
    private Label lblPresentes = null!;
    private Label lblFaltas = null!;
    private Label lblTardanzas = null!;
    private Button btnMatrizSemanal = null!;

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
        lblAula = new Label();
        cmbAula = new ComboBox();
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        btnGuardar = new Button();
        dgvAsistencia = new DataGridView();
        lblNota = new Label();
        lblPresentes = new Label();
        lblFaltas = new Label();
        lblTardanzas = new Label();
        btnMatrizSemanal = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvAsistencia).BeginInit();
        SuspendLayout();

        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(700, 42);
        lblTitulo.Text = "Asistencia";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 72);
        lblDescripcion.Size = new Size(900, 28);
        lblDescripcion.Text = "Registra y corrige la asistencia de cada estudiante por fecha.";

        // lblAula
        lblAula.Name = "lblAula";
        lblAula.Location = new Point(28, 101);
        lblAula.Size = new Size(420, 22);
        lblAula.Text = "Aula";

        // cmbAula
        cmbAula.Name = "cmbAula";
        cmbAula.Location = new Point(28, 126);
        cmbAula.Size = new Size(420, 32);
        cmbAula.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbAula.TabIndex = 0;
        cmbAula.SelectedIndexChanged += FiltroCambiado;

        // lblFecha
        lblFecha.Name = "lblFecha";
        lblFecha.Location = new Point(462, 101);
        lblFecha.Size = new Size(210, 22);
        lblFecha.Text = "Fecha";

        // dtpFecha
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Location = new Point(462, 126);
        dtpFecha.Size = new Size(210, 32);
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.TabIndex = 1;
        dtpFecha.ValueChanged += FiltroCambiado;

        // btnGuardar
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Location = new Point(690, 121);
        btnGuardar.Size = new Size(185, 40);
        btnGuardar.Text = "Guardar asistencia";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.TabIndex = 2;
        btnGuardar.Enabled = false;
        btnGuardar.Click += btnGuardar_Click;

        // dgvAsistencia
        dgvAsistencia.Name = "dgvAsistencia";
        dgvAsistencia.Location = new Point(28, 186);
        dgvAsistencia.Size = new Size(1020, 430);
        dgvAsistencia.AllowUserToAddRows = false;
        dgvAsistencia.AllowUserToDeleteRows = false;
        dgvAsistencia.AutoGenerateColumns = false;
        dgvAsistencia.RowHeadersVisible = false;
        dgvAsistencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAsistencia.MultiSelect = false;
        dgvAsistencia.BackgroundColor = Color.White;
        dgvAsistencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAsistencia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAsistencia.TabIndex = 3;
        dgvAsistencia.CellContentClick += dgvAsistencia_CellContentClick;
        dgvAsistencia.CellValueChanged += dgvAsistencia_CellValueChanged;
        dgvAsistencia.CurrentCellDirtyStateChanged += dgvAsistencia_CurrentCellDirtyStateChanged;

        // lblNota
        lblNota.Name = "lblNota";
        lblNota.Location = new Point(28, 634);
        lblNota.Size = new Size(900, 28);
        lblNota.Text = "Una fecha sin registro se muestra como Sin registro y no se cuenta como falta.";
        lblNota.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        // lblPresentes
        lblPresentes.Name = "lblPresentes";
        lblPresentes.Location = new Point(28, 684);
        lblPresentes.Size = new Size(105, 28);
        lblPresentes.Text = "0 presentes";
        lblPresentes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        // lblFaltas
        lblFaltas.Name = "lblFaltas";
        lblFaltas.Location = new Point(139, 684);
        lblFaltas.Size = new Size(82, 28);
        lblFaltas.Text = "0 faltas";
        lblFaltas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        // lblTardanzas
        lblTardanzas.Name = "lblTardanzas";
        lblTardanzas.Location = new Point(226, 684);
        lblTardanzas.Size = new Size(104, 28);
        lblTardanzas.Text = "0 tardanzas";
        lblTardanzas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        // btnMatrizSemanal
        btnMatrizSemanal.Name = "btnMatrizSemanal";
        btnMatrizSemanal.Location = new Point(339, 674);
        btnMatrizSemanal.Size = new Size(185, 40);
        btnMatrizSemanal.Text = "Ver matriz semanal";
        btnMatrizSemanal.UseVisualStyleBackColor = true;
        btnMatrizSemanal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnMatrizSemanal.TabIndex = 4;
        btnMatrizSemanal.Enabled = false;
        btnMatrizSemanal.Click += btnMatrizSemanal_Click;

        // FormAsistencia
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 245, 245);
        ClientSize = new Size(1100, 750);
        Font = new Font("Segoe UI", 10F);
        Name = "FormAsistencia";
        Text = "Asistencia";

        Controls.AddRange(new Control[]
        {
            lblTitulo,
            lblDescripcion,
            lblAula,
            cmbAula,
            lblFecha,
            dtpFecha,
            btnGuardar,
            dgvAsistencia,
            lblNota,
            lblPresentes,
            lblFaltas,
            lblTardanzas,
            btnMatrizSemanal
        });
        ((System.ComponentModel.ISupportInitialize)dgvAsistencia).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
