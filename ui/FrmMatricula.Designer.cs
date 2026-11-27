#nullable enable
namespace GestionEstudiante;

partial class FrmMatricula
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitulo = null!;
    private Label lblEstudiante = null!;
    private ComboBox cboEstudiante = null!;
    private Label lblAula = null!;
    private ComboBox cboAula = null!;
    private Label lblFecha = null!;
    private DateTimePicker dtpFecha = null!;
    private Label lblCupos = null!;
    private Label lblDisponibilidad = null!;
    private Label lblNota = null!;
    private Button btnCambiarSeleccion = null!;
    private Button btnCancelar = null!;
    private Button btnGuardar = null!;

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
        lblEstudiante = new Label();
        cboEstudiante = new ComboBox();
        lblAula = new Label();
        cboAula = new ComboBox();
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        lblCupos = new Label();
        lblDisponibilidad = new Label();
        lblNota = new Label();
        btnCambiarSeleccion = new Button();
        btnCancelar = new Button();
        btnGuardar = new Button();
        SuspendLayout();

        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Location = new Point(28, 24);
        lblTitulo.Size = new Size(700, 42);
        lblTitulo.Text = "Nueva matrícula";
        lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

        // lblEstudiante
        lblEstudiante.Name = "lblEstudiante";
        lblEstudiante.Location = new Point(28, 100);
        lblEstudiante.Size = new Size(470, 28);
        lblEstudiante.Text = "Estudiante *";

        // cboEstudiante
        cboEstudiante.Name = "cboEstudiante";
        cboEstudiante.Location = new Point(28, 136);
        cboEstudiante.Size = new Size(470, 32);
        cboEstudiante.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEstudiante.TabIndex = 0;

        // lblAula
        lblAula.Name = "lblAula";
        lblAula.Location = new Point(530, 100);
        lblAula.Size = new Size(470, 28);
        lblAula.Text = "Aula *";

        // cboAula
        cboAula.Name = "cboAula";
        cboAula.Location = new Point(530, 136);
        cboAula.Size = new Size(470, 32);
        cboAula.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAula.TabIndex = 1;

        // lblFecha
        lblFecha.Name = "lblFecha";
        lblFecha.Location = new Point(28, 200);
        lblFecha.Size = new Size(470, 28);
        lblFecha.Text = "Fecha de matrícula *";

        // dtpFecha
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Location = new Point(28, 236);
        dtpFecha.Size = new Size(470, 34);
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.TabIndex = 2;

        // lblCupos
        lblCupos.Name = "lblCupos";
        lblCupos.Location = new Point(530, 200);
        lblCupos.Size = new Size(470, 28);
        lblCupos.Text = "Disponibilidad";

        // lblDisponibilidad
        lblDisponibilidad.Name = "lblDisponibilidad";
        lblDisponibilidad.Location = new Point(530, 236);
        lblDisponibilidad.Size = new Size(470, 34);
        lblDisponibilidad.Text = "Selecciona un aula";

        // lblNota
        lblNota.Name = "lblNota";
        lblNota.Location = new Point(28, 414);
        lblNota.Size = new Size(960, 35);
        lblNota.Text = "Selecciona un estudiante y un aula con cupos.";

        // btnCambiarSeleccion
        btnCambiarSeleccion.Name = "btnCambiarSeleccion";
        btnCambiarSeleccion.Location = new Point(484, 492);
        btnCambiarSeleccion.Size = new Size(150, 42);
        btnCambiarSeleccion.Text = "Cambiar selección";
        btnCambiarSeleccion.UseVisualStyleBackColor = true;
        btnCambiarSeleccion.TabIndex = 3;
        btnCambiarSeleccion.Enabled = false;

        // btnCancelar
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Location = new Point(650, 492);
        btnCancelar.Size = new Size(150, 42);
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.TabIndex = 4;
        btnCancelar.DialogResult = DialogResult.Cancel;

        // btnGuardar
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Location = new Point(816, 492);
        btnGuardar.Size = new Size(185, 42);
        btnGuardar.Text = "Registrar matrícula";
        btnGuardar.UseVisualStyleBackColor = true;
        btnGuardar.TabIndex = 5;
        btnGuardar.Enabled = false;

        // FrmMatricula
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 245, 245);
        ClientSize = new Size(1030, 570);
        Font = new Font("Segoe UI", 10F);
        Name = "FrmMatricula";
        Text = "Nueva matrícula";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
        Controls.AddRange(new Control[]
        {
            lblTitulo,
            lblEstudiante,
            cboEstudiante,
            lblAula,
            cboAula,
            lblFecha,
            dtpFecha,
            lblCupos,
            lblDisponibilidad,
            lblNota,
            btnCambiarSeleccion,
            btnCancelar,
            btnGuardar
        });
        ResumeLayout(false);
        PerformLayout();
    }
}
