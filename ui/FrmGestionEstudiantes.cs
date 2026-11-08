using GestionEstudiante.model;
using GestionEstudiante.service;

namespace GestionEstudiante;
public partial class FrmGestionEstudiantes : Form
{
    private readonly EstudianteService servicio = null!;
    public FrmGestionEstudiantes()
    {
        InitializeComponent();
    }

    public FrmGestionEstudiantes(EstudianteService servicio)
    {
        this.servicio = servicio;
        InitializeComponent();
    }

    private void FrmGestionEstudiantes_Load(object? sender, EventArgs e)
    {
        ActualizarListado();
    }

    private void FiltroCambiado(object? sender, EventArgs e)
    {
        ActualizarListado();
    }

    public void ActualizarListado()
    {
        dgvEstudiantes.Rows.Clear();
        Estudiante[] estudiantes = servicio.Buscar(txtBuscar.Text, cboEstado.SelectedIndex);
        foreach (Estudiante estudiante in estudiantes)
        {
            int fila = dgvEstudiantes.Rows.Add(estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos, estudiante.apoderado, estudiante.telefono, estudiante.activo ? "Activo" : "Inactivo", "Editar", estudiante.activo ? "Dar de baja" : "");
            dgvEstudiantes.Rows[fila].Tag = estudiante;
        }

        if (dgvEstudiantes.Rows.Count == 0)
        {
            lblResultados.Text = "Sin resultados";
        }
        else
        {
            lblResultados.Text = dgvEstudiantes.Rows.Count + " estudiantes en la lista";
        }

        dgvEstudiantes.ClearSelection();
    }

    private void btnNuevo_Click(object? sender, EventArgs e)
    {
        using FrmEstudiante formulario = new FrmEstudiante(servicio, null);
        DialogResult resultadoFormulario = formulario.ShowDialog(this);
        if (resultadoFormulario == DialogResult.OK)
        {
            ActualizarListado();
        }
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        ActualizarListado();
    }

    private void dgvEstudiantes_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        Estudiante estudiante = (Estudiante)dgvEstudiantes.Rows[e.RowIndex].Tag!;
        if (e.ColumnIndex == colEditar.Index)
        {
            using FrmEstudiante formulario = new FrmEstudiante(servicio, estudiante);
            DialogResult resultadoFormulario = formulario.ShowDialog(this);
            if (resultadoFormulario == DialogResult.OK)
            {
                ActualizarListado();
            }
        }

        if (e.ColumnIndex == colBaja.Index && estudiante.activo)
        {
            DialogResult respuesta = MessageBox.Show(this,
                "¿Dar de baja a " + estudiante.nombre + " " + estudiante.apellidos + "?",
                "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                servicio.DarDeBaja(estudiante);
                ActualizarListado();
                MessageBox.Show(this, "El estudiante quedó inactivo. Sus datos se conservaron.", "Baja realizada");
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "No se pudo guardar");
            }
        }
    }
}
