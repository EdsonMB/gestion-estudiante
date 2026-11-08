using GestionEstudiante.model;
using GestionEstudiante.service;

namespace GestionEstudiante;

public partial class FrmEstudiante : Form
{
    private readonly EstudianteService servicio = null!;
    private readonly Estudiante? original;
    public FrmEstudiante()
    {
        InitializeComponent();
    }
    public FrmEstudiante(EstudianteService servicio, Estudiante? original)
    {
        this.servicio = servicio;
        this.original = original;
        InitializeComponent();
        if (original == null) { txtCodigo.Text = "Se asignará al guardar"; return; }
        lblTitulo.Text = "Editar estudiante";
        btnGuardar.Text = "Guardar cambios";
        txtNombres.Text = original.nombre;
        txtApellidos.Text = original.apellidos;
        txtApoderado.Text = original.apoderado;
        txtTelefono.Text = original.telefono;
        txtCorreo.Text = original.correo;
        txtCodigo.Text = original.codigo;
        lblNota.Text = "Se conservarán el código, la fecha de registro y el estado del estudiante.";
    }
    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        // Editamos un objeto nuevo para no alterar los datos si falla el guardado.
        Estudiante estudiante = new Estudiante();
        if (original != null)
        {
            estudiante.id = original.id;
            estudiante.activo = original.activo;
        }
        estudiante.nombre = txtNombres.Text;
        estudiante.apellidos = txtApellidos.Text;
        estudiante.apoderado = txtApoderado.Text;
        estudiante.telefono = txtTelefono.Text;
        estudiante.correo = txtCorreo.Text;
        try
        {
            servicio.Guardar(estudiante);
            MessageBox.Show(this, "Estudiante guardado correctamente.", "Datos guardados");
            DialogResult = DialogResult.OK;
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
