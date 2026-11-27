#nullable enable
namespace GestionEstudiante;
partial class FrmMatrizSemanal
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private DataGridView dgvMatriz = null!;
    private Button btnCerrar = null!;
    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        lblTitulo = new Label();
        dgvMatriz = new DataGridView();
        btnCerrar = new Button();
        SuspendLayout();
        lblTitulo.Location = new Point(24, 24);
        lblTitulo.Size = new Size(1000, 40);
        lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        dgvMatriz.Location = new Point(24, 80);
        dgvMatriz.Size = new Size(1030, 400);
        dgvMatriz.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvMatriz.ReadOnly = true;
        dgvMatriz.AllowUserToAddRows = false;
        dgvMatriz.AllowUserToDeleteRows = false;
        dgvMatriz.RowHeadersVisible = false;
        dgvMatriz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMatriz.BackgroundColor = Color.White;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Location = new Point(914, 500);
        btnCerrar.Size = new Size(140, 40);
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.DialogResult = DialogResult.Cancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1080, 560);
        MinimumSize = new Size(800, 450);
        Font = new Font("Segoe UI", 10F);
        Text = "Matriz semanal de asistencia";
        StartPosition = FormStartPosition.CenterParent;
        CancelButton = btnCerrar;
        Controls.AddRange(new Control[] { lblTitulo, dgvMatriz, btnCerrar });
        ResumeLayout(false);
    }
}
