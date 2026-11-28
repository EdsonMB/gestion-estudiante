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
        cboEstado.TabIndex = 1;

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
        dgvMatriculas.TabIndex = 3;

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
