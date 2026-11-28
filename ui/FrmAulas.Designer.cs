#nullable enable
namespace GestionEstudiante;
partial class FrmAulas
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblDescripcion = null!;
    private Button btnNueva = null!;
    private Label lblNivel = null!;
    private ComboBox cboNivel = null!;
    private Label lblAnio = null!;
    private NumericUpDown numAnio = null!;
    private DataGridView dgvAulas = null!;
    private Label lblResumen = null!;
    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        SuspendLayout();
        lblTitulo = new Label();
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(700, 44);
        lblTitulo.Text = "Aulas";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblDescripcion = new Label();
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(28, 76);
        lblDescripcion.Size = new Size(950, 32);
        lblDescripcion.Text = "Administra las aulas y sus cupos por año escolar.";
        btnNueva = new Button();
        btnNueva.Name = "btnNueva";
        btnNueva.Location = new Point(865, 24);
        btnNueva.Size = new Size(180, 40);
        btnNueva.Text = "+ Nueva aula";
        btnNueva.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNueva.Click += btnNueva_Click;
        lblNivel = new Label();
        lblNivel.Name = "lblNivel";
        lblNivel.Location = new Point(28, 120);
        lblNivel.Size = new Size(180, 24);
        lblNivel.Text = "Nivel";
        cboNivel = new ComboBox();
        cboNivel.Name = "cboNivel";
        cboNivel.Location = new Point(28, 150);
        cboNivel.Size = new Size(300, 32);
        cboNivel.DropDownStyle = ComboBoxStyle.DropDownList;
        lblAnio = new Label();
        lblAnio.Name = "lblAnio";
        lblAnio.Location = new Point(350, 120);
        lblAnio.Size = new Size(160, 24);
        lblAnio.Text = "Año escolar";
        numAnio = new NumericUpDown();
        numAnio.Name = "numAnio";
        numAnio.Location = new Point(350, 150);
        numAnio.Size = new Size(160, 32);
        numAnio.Minimum = 2000;
        numAnio.Maximum = 2100;
        dgvAulas = new DataGridView();
        dgvAulas.Name = "dgvAulas";
        dgvAulas.Location = new Point(28, 205);
        dgvAulas.Size = new Size(1020, 450);
        dgvAulas.AllowUserToAddRows = false;
        dgvAulas.AllowUserToDeleteRows = false;
        dgvAulas.ReadOnly = true;
        dgvAulas.RowHeadersVisible = false;
        dgvAulas.MultiSelect = false;
        dgvAulas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAulas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAulas.BackgroundColor = Color.White;
        dgvAulas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAulas.RowTemplate.Height = 36;
        dgvAulas.CellContentClick += dgvAulas_CellContentClick;
        lblResumen = new Label();
        lblResumen.Name = "lblResumen";
        lblResumen.Location = new Point(28, 678);
        lblResumen.Size = new Size(1020, 40);
        lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAulas.Columns.Add("nivel", "Nivel");
        dgvAulas.Columns.Add("grado", "Grado");
        dgvAulas.Columns.Add("seccion", "Sección");
        dgvAulas.Columns.Add("anio", "Año");
        dgvAulas.Columns.Add("ocupacion", "Ocupación");
        dgvAulas.Columns.Add("estado", "Disponibilidad");
        dgvAulas.Columns.Add(new DataGridViewButtonColumn { Name = "editar", HeaderText = "Editar" });
        dgvAulas.Columns.Add(new DataGridViewButtonColumn { Name = "estadoAula", HeaderText = "Estado" });
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 750);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(245, 245, 245);
        Name = "FrmAulas";
        AutoScroll = true;
        Controls.AddRange(new Control[] { lblTitulo, lblDescripcion, btnNueva, lblNivel, cboNivel, lblAnio, numAnio, dgvAulas, lblResumen });
        ResumeLayout(false);
        PerformLayout();
    }
}
