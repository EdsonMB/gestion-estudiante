#nullable enable
using GestionEstudiante.util;
namespace GestionEstudiante;
partial class FrmAula
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblNivel = null!;
    private ComboBox cboNivel = null!;
    private Label lblGrado = null!;
    private NumericUpDown numGrado = null!;
    private Label lblSeccion = null!;
    private TextBox txtSeccion = null!;
    private Label lblAnio = null!;
    private NumericUpDown numAnio = null!;
    private Label lblCapacidad = null!;
    private NumericUpDown numCapacidad = null!;
    private Button btnCancelar = null!;
    private Button btnGuardar = null!;
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
        lblTitulo.Size = new Size(560, 44);
        lblTitulo.Text = "Nueva aula";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblNivel = new Label();
        lblNivel.Name = "lblNivel";
        lblNivel.Location = new Point(28, 94);
        lblNivel.Size = new Size(260, 24);
        lblNivel.Text = "Nivel *";
        cboNivel = new ComboBox();
        cboNivel.Name = "cboNivel";
        cboNivel.Location = new Point(28, 124);
        cboNivel.Size = new Size(270, 32);
        cboNivel.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNivel.Items.AddRange(Constantes.Niveles.ToArray());
        lblGrado = new Label();
        lblGrado.Name = "lblGrado";
        lblGrado.Location = new Point(330, 94);
        lblGrado.Size = new Size(260, 24);
        lblGrado.Text = "Grado *";
        numGrado = new NumericUpDown();
        numGrado.Name = "numGrado";
        numGrado.Location = new Point(330, 124);
        numGrado.Size = new Size(270, 32);
        numGrado.Minimum = 1;
        numGrado.Maximum = 6;
        lblSeccion = new Label();
        lblSeccion.Name = "lblSeccion";
        lblSeccion.Location = new Point(28, 180);
        lblSeccion.Size = new Size(260, 24);
        lblSeccion.Text = "Sección *";
        txtSeccion = new TextBox();
        txtSeccion.Name = "txtSeccion";
        txtSeccion.Location = new Point(28, 210);
        txtSeccion.Size = new Size(270, 32);
        txtSeccion.MaxLength = 10;
        lblAnio = new Label();
        lblAnio.Name = "lblAnio";
        lblAnio.Location = new Point(330, 180);
        lblAnio.Size = new Size(260, 24);
        lblAnio.Text = "Año escolar *";
        numAnio = new NumericUpDown();
        numAnio.Name = "numAnio";
        numAnio.Location = new Point(330, 210);
        numAnio.Size = new Size(270, 32);
        numAnio.Minimum = 2000;
        numAnio.Maximum = 2100;
        lblCapacidad = new Label();
        lblCapacidad.Name = "lblCapacidad";
        lblCapacidad.Location = new Point(28, 270);
        lblCapacidad.Size = new Size(260, 24);
        lblCapacidad.Text = "Capacidad *";
        numCapacidad = new NumericUpDown();
        numCapacidad.Name = "numCapacidad";
        numCapacidad.Location = new Point(28, 300);
        numCapacidad.Size = new Size(270, 32);
        numCapacidad.Minimum = 1;
        numCapacidad.Maximum = 1000;
        btnCancelar = new Button();
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Location = new Point(330, 370);
        btnCancelar.Size = new Size(125, 40);
        btnCancelar.Text = "Cancelar";
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnGuardar = new Button();
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Location = new Point(475, 370);
        btnGuardar.Size = new Size(125, 40);
        btnGuardar.Text = "Guardar";
        btnGuardar.Click += btnGuardar_Click;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(640, 440);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(245, 245, 245);
        Name = "FrmAula";
        AutoScroll = true;
        Controls.AddRange(new Control[] { lblTitulo, lblNivel, cboNivel, lblGrado, numGrado, lblSeccion, txtSeccion, lblAnio, numAnio, lblCapacidad, numCapacidad, btnCancelar, btnGuardar });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
        ResumeLayout(false);
        PerformLayout();
    }
}
