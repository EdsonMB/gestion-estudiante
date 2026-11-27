using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmMatricula : Form
{
    private readonly EscolarService servicio = null!;
    public FrmMatricula() { InitializeComponent(); }
    public FrmMatricula(EscolarService servicio) : this()
    {
        this.servicio = servicio;
        foreach (Estudiante estudiante in servicio.Estudiantes.Where(x => x.activo))
            cboEstudiante.Items.Add(new Opcion(estudiante.id, estudiante.codigo + " · " + servicio.NombreEstudiante(estudiante.id)));
        foreach (Aula aula in servicio.Aulas.Listar().Where(x => x.activo))
            cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        dtpFecha.MaxDate = DateTime.Today;
        cboAula.SelectedIndexChanged += (_, _) => ActualizarCupos();
        cboEstudiante.SelectedIndexChanged += (_, _) => ActualizarCupos();
        btnGuardar.Click += Guardar;
        btnCambiarSeleccion.Enabled = true;
        btnCambiarSeleccion.Click += (_, _) => { cboAula.SelectedIndex = -1; cboAula.Focus(); };
        lblNota.Text = "Selecciona un estudiante y un aula con cupos. Solo se permite una matrícula activa por año.";
    }
    private void ActualizarCupos()
    {
        Aula? aula = servicio.Aulas.Listar().SingleOrDefault(x => x.id == (cboAula.SelectedItem as Opcion)?.Id);
        int libres = aula == null ? 0 : servicio.Aulas.Disponibles(aula);
        lblDisponibilidad.Text = aula == null ? "Selecciona un aula" : libres + " cupos disponibles de " + aula.capacidad;
        btnGuardar.Enabled = libres > 0 && cboEstudiante.SelectedItem != null;
    }
    private void Guardar(object? sender, EventArgs e)
    {
        if (cboEstudiante.SelectedItem is not Opcion estudiante || cboAula.SelectedItem is not Opcion aula) return;
        try
        {
            servicio.Matriculas.Registrar(estudiante.Id, aula.Id, dtpFecha.Value);
            DialogResult = DialogResult.OK;
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo matricular", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
