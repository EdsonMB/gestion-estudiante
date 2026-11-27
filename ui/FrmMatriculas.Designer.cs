#nullable enable
namespace GestionEstudiante;

partial class FrmMatriculas
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private Label lblAula = null!;
    private ComboBox cboAula = null!;
    private Label lblEstado = null!;
    private ComboBox cboEstado = null!;
    private Button btnNueva = null!;
    private DataGridView dgvMatriculas = null!;
    private DataGridViewTextBoxColumn colCodigo = null!;
    private DataGridViewTextBoxColumn colEstudiante = null!;
    private DataGridViewTextBoxColumn colAula = null!;
    private DataGridViewTextBoxColumn colFecha = null!;
    private DataGridViewTextBoxColumn colEstado = null!;
    private DataGridViewButtonColumn colCancelar = null!;
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
        lblAula = new Label();
        cboAula = new ComboBox();
        lblEstado = new Label();
        cboEstado = new ComboBox();
        btnNueva = new Button();
        dgvMatriculas = new DataGridView();
        colCodigo = new DataGridViewTextBoxColumn();
        colEstudiante = new DataGridViewTextBoxColumn();
        colAula = new DataGridViewTextBoxColumn();
        colFecha = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        colCancelar = new DataGridViewButtonColumn();
        lblResultados = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvMatriculas).BeginInit();
        SuspendLayout();

        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(700, 42);
        lblTitulo.Text = "Matrículas";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 72);
        lblDescripcion.Size = new Size(900, 28);
        lblDescripcion.Text = "Revisa las matrículas activas y conserva el historial de cancelaciones.";

        // lblAula
        lblAula.Name = "lblAula";
        lblAula.Location = new Point(28, 101);
        lblAula.Size = new Size(220, 22);
        lblAula.Text = "Aula";

        // cboAula
        cboAula.Name = "cboAula";
        cboAula.Location = new Point(28, 126);
        cboAula.Size = new Size(210, 32);
        cboAula.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAula.TabIndex = 0;
        cboAula.Items.Add("Todas las aulas");
        cboAula.SelectedIndex = 0;

        // lblEstado
        lblEstado.Name = "lblEstado";
        lblEstado.Location = new Point(254, 101);
        lblEstado.Size = new Size(220, 22);
        lblEstado.Text = "Estado";

        // cboEstado
        cboEstado.Name = "cboEstado";
        cboEstado.Location = new Point(254, 126);
        cboEstado.Size = new Size(210, 32);
        cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEstado.Items.AddRange(new object[] { "Activas y canceladas", "Solo activas", "Solo canceladas" });
        cboEstado.TabIndex = 1;
        cboEstado.SelectedIndex = 0;

        // btnNueva
        btnNueva.Name = "btnNueva";
        btnNueva.Location = new Point(482, 121);
        btnNueva.Size = new Size(185, 40);
        btnNueva.Text = "+ Nueva matrícula";
        btnNueva.UseVisualStyleBackColor = true;
        btnNueva.TabIndex = 2;
        btnNueva.Click += btnNueva_Click;

        // dgvMatriculas
        dgvMatriculas.Name = "dgvMatriculas";
        dgvMatriculas.Location = new Point(28, 186);
        dgvMatriculas.Size = new Size(1020, 470);
        dgvMatriculas.ReadOnly = true;
        dgvMatriculas.AllowUserToAddRows = false;
        dgvMatriculas.AllowUserToDeleteRows = false;
        dgvMatriculas.AutoGenerateColumns = false;
        dgvMatriculas.RowHeadersVisible = false;
        dgvMatriculas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMatriculas.MultiSelect = false;
        dgvMatriculas.BackgroundColor = Color.White;
        dgvMatriculas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMatriculas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvMatriculas.Columns.AddRange(colCodigo, colEstudiante, colAula, colFecha, colEstado, colCancelar);
        dgvMatriculas.TabIndex = 3;

        // colCodigo
        colCodigo.Name = "colCodigo";
        colCodigo.HeaderText = "Código";

        // colEstudiante
        colEstudiante.Name = "colEstudiante";
        colEstudiante.HeaderText = "Estudiante";

        // colAula
        colAula.Name = "colAula";
        colAula.HeaderText = "Aula";

        // colFecha
        colFecha.Name = "colFecha";
        colFecha.HeaderText = "Fecha";

        // colEstado
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";

        // colCancelar
        colCancelar.Name = "colCancelar";
        colCancelar.HeaderText = "Cancelar";

        // lblResultados
        lblResultados.Name = "lblResultados";
        lblResultados.Location = new Point(28, 678);
        lblResultados.Size = new Size(900, 30);
        lblResultados.Text = "Sin matrículas.";
        lblResultados.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

        // FrmMatriculas
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 245, 245);
        ClientSize = new Size(1100, 750);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmMatriculas";
        Text = "Matrículas";
        Controls.AddRange(new Control[]
        {
            lblTitulo,
            lblDescripcion,
            lblAula,
            cboAula,
            lblEstado,
            cboEstado,
            btnNueva,
            dgvMatriculas,
            lblResultados
        });
        ((System.ComponentModel.ISupportInitialize)dgvMatriculas).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
