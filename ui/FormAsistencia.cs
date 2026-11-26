using GestionEstudiante.model;
using GestionEstudiante.service;

namespace GestionEstudiante;

public partial class FormAsistencia : Form
{
    private readonly EstudianteService servicioEstudiantes = null!;
    private readonly AsistenciaService servicioAsistencia = null!;
    private List<Estudiante>? estudiantesListado;
    private DateTime fechaListado;
    private bool restaurandoFecha;

    public FormAsistencia()
    {
        InitializeComponent();
    }

    public FormAsistencia(EstudianteService servicioEstudiantes, AsistenciaService servicioAsistencia) : this()
    {
        this.servicioEstudiantes = servicioEstudiantes;
        this.servicioAsistencia = servicioAsistencia;
    }

    public void ActualizarAlMostrar()
    {
        if (!btnGuardar.Enabled || estudiantesListado != servicioEstudiantes.Listar())
        {
            ActualizarListado();
        }
    }

    private void ActualizarListado()
    {
        fechaListado = dtpFecha.Value.Date;
        dgvAsistencia.Rows.Clear();
        List<Asistencia> registros = servicioAsistencia.Listar();

        estudiantesListado = servicioEstudiantes.Listar();
        foreach (Estudiante estudiante in estudiantesListado)
        {
            if (!estudiante.activo)
            {
                continue;
            }

            Asistencia? registro = registros.Find(item => item.fecha == fechaListado
                && string.Equals(item.codigoEstudiante, estudiante.codigo, StringComparison.OrdinalIgnoreCase));
            string estado = registro?.estado ?? Asistencia.SinRegistro;
            int indice = dgvAsistencia.Rows.Add(estudiante.codigo,
                estudiante.nombre + " " + estudiante.apellidos, estado, "Marcar");
            DataGridViewRow fila = dgvAsistencia.Rows[indice];
            fila.Tag = estudiante;
            fila.Cells[colEstado.Index].Tag = estado;
        }

        dgvAsistencia.ClearSelection();
        ActualizarResumen();
    }

    private void dtpFecha_ValueChanged(object? sender, EventArgs e)
    {
        if (restaurandoFecha || servicioAsistencia == null || dtpFecha.Value.Date == fechaListado)
        {
            return;
        }

        if (btnGuardar.Enabled && MessageBox.Show(this,
            "Hay cambios sin guardar. ¿Desea descartarlos y cambiar de fecha?",
            "Cambiar fecha", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            restaurandoFecha = true;
            dtpFecha.Value = fechaListado;
            restaurandoFecha = false;
            return;
        }

        ActualizarListado();
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        List<Asistencia> cambios = new List<Asistencia>();
        foreach (DataGridViewRow fila in dgvAsistencia.Rows)
        {
            DataGridViewCell celda = fila.Cells[colEstado.Index];
            if (fila.Tag is Estudiante estudiante && celda.Value is string estado && !Equals(celda.Tag, estado))
            {
                cambios.Add(new Asistencia
                {
                    codigoEstudiante = estudiante.codigo,
                    fecha = fechaListado,
                    estado = estado
                });
            }
        }

        try
        {
            servicioAsistencia.Guardar(cambios);
            ActualizarListado();
            MessageBox.Show(this, "Asistencia guardada correctamente.", "Asistencia");
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvAsistencia_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colAcciones.Index)
        {
            return;
        }

        DataGridViewCell celda = dgvAsistencia.Rows[e.RowIndex].Cells[colEstado.Index];
        celda.Value = celda.Value switch
        {
            Asistencia.Presente => Asistencia.Falta,
            Asistencia.Falta => Asistencia.Tardanza,
            _ => Asistencia.Presente
        };
        ActualizarResumen();
    }

    private void ActualizarResumen()
    {
        int presentes = 0;
        int faltas = 0;
        int tardanzas = 0;
        bool hayCambios = false;
        foreach (DataGridViewRow fila in dgvAsistencia.Rows)
        {
            DataGridViewCell celda = fila.Cells[colEstado.Index];
            switch (celda.Value)
            {
                case Asistencia.Presente: presentes++; break;
                case Asistencia.Falta: faltas++; break;
                case Asistencia.Tardanza: tardanzas++; break;
            }

            hayCambios |= !Equals(celda.Tag, celda.Value);
        }

        lblPresentes.Text = presentes + " presentes";
        lblFaltas.Text = faltas + " faltas";
        lblTardanzas.Text = tardanzas + " tardanzas";
        btnGuardar.Enabled = hayCambios;
    }
}
