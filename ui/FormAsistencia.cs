using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FormAsistencia : Form
{
    private readonly EscolarService servicio = null!;
    private bool cargando;
    private bool modificado;
    private int aulaActual;
    private DateTime fechaActual = DateTime.Today;
    public FormAsistencia() { InitializeComponent(); }
    public FormAsistencia(EscolarService servicio) : this()
    {
        this.servicio = servicio;
        cargando = true;
        dtpFecha.MaxDate = DateTime.Today;
        dtpFecha.Value = DateTime.Today;
        cargando = false;
        cmbAula.SelectedIndexChanged += (_, _) => CambiarFiltro();
        dgvAsistencia.CellValueChanged += (_, _) => { if (!cargando) { modificado = true; ActualizarTotales(); } };
        dgvAsistencia.CurrentCellDirtyStateChanged += (_, _) =>
        {
            if (dgvAsistencia.IsCurrentCellDirty) dgvAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit);
        };
        btnMatrizSemanal.Click += (_, _) =>
        {
            if (aulaActual == 0 || !ConfirmarSalida()) return;
            using FrmMatrizSemanal matriz = new FrmMatrizSemanal(servicio, aulaActual, fechaActual);
            matriz.ShowDialog(this);
        };
    }
    public void ActualizarListado()
    {
        cargando = true;
        cmbAula.Items.Clear();
        foreach (Aula aula in servicio.Aulas.Listar()) cmbAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        int indice = cmbAula.Items.Cast<Opcion>().ToList().FindIndex(x => x.Id == aulaActual);
        cmbAula.SelectedIndex = indice >= 0 ? indice : cmbAula.Items.Count > 0 ? 0 : -1;
        aulaActual = (cmbAula.SelectedItem as Opcion)?.Id ?? 0;
        fechaActual = dtpFecha.Value.Date;
        cargando = false;
        CargarDia();
    }
    private void CambiarFiltro()
    {
        if (cargando || servicio == null) return;
        if (!ConfirmarSalida())
        {
            cargando = true;
            cmbAula.SelectedIndex = cmbAula.Items.Cast<Opcion>().ToList().FindIndex(x => x.Id == aulaActual);
            dtpFecha.Value = fechaActual;
            cargando = false;
            return;
        }
        aulaActual = (cmbAula.SelectedItem as Opcion)?.Id ?? 0;
        fechaActual = dtpFecha.Value.Date;
        CargarDia();
    }
    private void CargarDia()
    {
        cargando = true;
        dgvAsistencia.Rows.Clear();
        foreach (Matricula matricula in servicio.Asistencias.MatriculasDelDia(aulaActual, fechaActual))
        {
            Estudiante estudiante = servicio.Estudiantes.Single(x => x.id == matricula.estudianteId);
            Asistencia? asistencia = servicio.Asistencias.Listar().SingleOrDefault(x => x.matriculaId == matricula.id && x.fecha.Date == fechaActual);
            int fila = dgvAsistencia.Rows.Add(estudiante.codigo, servicio.NombreEstudiante(estudiante.id),
                asistencia?.estado.ToString() ?? "Sin registro", asistencia?.observacion ?? "");
            dgvAsistencia.Rows[fila].Tag = matricula.id;
        }
        cargando = false;
        modificado = false;
        btnGuardar.Enabled = dgvAsistencia.Rows.Count > 0;
        btnMatrizSemanal.Enabled = aulaActual != 0;
        lblNota.Text = dgvAsistencia.Rows.Count == 0 ? "No hay matrículas para el aula y fecha seleccionados." :
            "Edita el estado y la observación. Sin registro no se cuenta como falta. Guarda los cambios antes de salir.";
        ActualizarTotales();
    }
    private void ActualizarTotales()
    {
        var filas = dgvAsistencia.Rows.Cast<DataGridViewRow>().ToList();
        lblPresentes.Text = filas.Count(x => (string?)x.Cells[2].Value == "Presente") + " presentes";
        lblFaltas.Text = filas.Count(x => (string?)x.Cells[2].Value == "Falta") + " faltas";
        lblTardanzas.Text = filas.Count(x => (string?)x.Cells[2].Value == "Tardanza") + " tardanzas";
    }
    public bool ConfirmarSalida()
    {
        dgvAsistencia.EndEdit();
        if (!modificado) return true;
        DialogResult respuesta = MessageBox.Show(this, "Hay cambios de asistencia sin guardar. ¿Desea guardarlos?",
            "Asistencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (respuesta == DialogResult.Cancel) return false;
        if (respuesta == DialogResult.Yes) return Guardar();
        CargarDia();
        return true;
    }
    private bool Guardar()
    {
        try
        {
            dgvAsistencia.EndEdit();
            List<Asistencia> registros = new();
            foreach (DataGridViewRow fila in dgvAsistencia.Rows)
            {
                string estado = (string?)fila.Cells[2].Value ?? "Sin registro";
                if (estado == "Sin registro") continue;
                registros.Add(new Asistencia { matriculaId = (int)fila.Tag!, estado = Enum.Parse<EstadoAsistencia>(estado),
                    observacion = fila.Cells[3].Value?.ToString() ?? "" });
            }
            servicio.Asistencias.GuardarDia(aulaActual, fechaActual, registros);
            modificado = false;
            lblNota.Text = "Asistencia guardada. Puedes corregirla y volver a guardar.";
            return true;
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    private void dtpFecha_ValueChanged(object? sender, EventArgs e) => CambiarFiltro();
    private void btnGuardar_Click(object? sender, EventArgs e) => Guardar();
    private void dgvAsistencia_CellContentClick(object? sender, DataGridViewCellEventArgs e) { }
}
