using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;
namespace GestionEstudiante;

public partial class FrmMatricula : Form
{
    private readonly AulaService servicioAulas = null!;
    private readonly MatriculaService servicioMatriculas = null!;
    public FrmMatricula() { InitializeComponent(); }
    public FrmMatricula(EstudianteService servicioEstudiantes, AulaService servicioAulas,
        MatriculaService servicioMatriculas) : this()
    {
        this.servicioAulas = servicioAulas;
        this.servicioMatriculas = servicioMatriculas;
        foreach (Estudiante estudiante in servicioEstudiantes.Listar())
        {
            if (estudiante.activo)
            {
                string nombre = estudiante.nombre + " " + estudiante.apellidos;
                cboEstudiante.Items.Add(new Opcion(estudiante.id, estudiante.codigo + " · " + nombre));
            }
        }
        foreach (Aula aula in servicioAulas.Listar())
        {
            if (aula.activo)
            {
                cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
            }
        }

        dtpFecha.MaxDate = DateTime.Today;
        cboAula.SelectedIndexChanged += SeleccionCambiada;
        cboEstudiante.SelectedIndexChanged += SeleccionCambiada;
        btnGuardar.Click += Guardar;
        btnCambiarSeleccion.Enabled = true;
        btnCambiarSeleccion.Click += btnCambiarSeleccion_Click;
        lblNota.Text = "Selecciona un estudiante y un aula con cupos. Solo se permite una matrícula activa por año.";
    }

    private void SeleccionCambiada(object? sender, EventArgs e)
    {
        if (cboAula.SelectedItem is Opcion opcion)
        {
            foreach (Aula aula in servicioAulas.Listar())
            {
                if (aula.id == opcion.Id)
                {
                    int libres = servicioAulas.Disponibles(aula);
                    lblDisponibilidad.Text = libres + " cupos disponibles de " + aula.capacidad;
                    btnGuardar.Enabled = libres > 0 && cboEstudiante.SelectedItem != null;
                    return;
                }
            }
        }

        lblDisponibilidad.Text = "Selecciona un aula";
        btnGuardar.Enabled = false;
    }

    private void btnCambiarSeleccion_Click(object? sender, EventArgs e)
    {
        cboAula.SelectedIndex = -1;
        cboAula.Focus();
    }

    private void Guardar(object? sender, EventArgs e)
    {
        if (cboEstudiante.SelectedItem is not Opcion estudiante || cboAula.SelectedItem is not Opcion aula) return;
        try
        {
            servicioMatriculas.Registrar(estudiante.Id, aula.Id, dtpFecha.Value);
            DialogResult = DialogResult.OK;
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo matricular", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
