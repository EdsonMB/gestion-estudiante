using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;
public partial class FrmGestionEstudiantes : Form
{
    private readonly EstudianteService servicio = null!;
    private DataGridViewButtonColumn colEditar = null!;
    private DataGridViewButtonColumn colCambiarEstado = null!;

    public FrmGestionEstudiantes()
    {
        InitializeComponent();
        InicializarColumnas();
        cboEstado.Items.AddRange(new object[]
        {
            Constantes.TodosLosEstados,
            Constantes.EstudiantesActivos,
            Constantes.EstudiantesInactivos
        });
        cboEstado.SelectedIndex = 0;
    }

    public FrmGestionEstudiantes(EstudianteService servicio) : this()
    {
        this.servicio = servicio;
        cboEstado.SelectedIndexChanged += FiltroCambiado;
    }

    private void InicializarColumnas()
    {
        colEditar = new DataGridViewButtonColumn { Name = "colEditar", HeaderText = "Editar" };
        colCambiarEstado = new DataGridViewButtonColumn { Name = "colCambiarEstado", HeaderText = "Cambiar estado" };
        dgvEstudiantes.Columns.AddRange(
            new DataGridViewTextBoxColumn { Name = "colCodigo", HeaderText = "Código" },
            new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Estudiante" },
            new DataGridViewTextBoxColumn { Name = "colApoderado", HeaderText = "Apoderado" },
            new DataGridViewTextBoxColumn { Name = "colTelefono", HeaderText = "Teléfono" },
            new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado" },
            colEditar, colCambiarEstado);
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
        List<Estudiante> estudiantes = servicio.Buscar(txtBuscar.Text, cboEstado.SelectedIndex);
        foreach (Estudiante estudiante in estudiantes)
        {
            string estado = estudiante.activo ? "Activo" : "Inactivo";
            string accion = estudiante.activo ? "Dar de baja" : "Activar";
            int fila = dgvEstudiantes.Rows.Add(estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos,
                estudiante.apoderado, estudiante.telefono, estado, "Editar", accion);
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

        if (e.ColumnIndex == colCambiarEstado.Index)
        {
            CambiarEstado(estudiante);
        }
    }

    private void CambiarEstado(Estudiante estudiante)
    {
        bool activar = !estudiante.activo;
        string accion = activar ? "Activar" : "Dar de baja";
        string titulo = activar ? "Confirmar activación" : "Confirmar baja";
        DialogResult respuesta = MessageBox.Show(this,
            $"¿{accion} a {estudiante.nombre} {estudiante.apellidos}?",
            titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (respuesta != DialogResult.Yes)
        {
            return;
        }

        try
        {
            if (activar)
            {
                servicio.Activar(estudiante.id);
            }
            else
            {
                servicio.DarDeBaja(estudiante);
            }

            ActualizarListado();
            string mensaje = activar ? "El estudiante quedó activo." : "El estudiante quedó inactivo. Sus datos se conservaron.";
            string resultado = activar ? "Activación realizada" : "Baja realizada";
            MessageBox.Show(this, mensaje, resultado);
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "No se pudo guardar");
        }
    }
}
