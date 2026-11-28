using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FormAsistencia : Form
{
    private const int SinAula = -1;
    private readonly EscolarService servicio = null!;
    private bool cargando;
    private bool modificado;
    private int aulaActual;
    private DateTime fechaActual = DateTime.Today;
    public FormAsistencia()
    {
        InitializeComponent();
        colEstado.Items.AddRange(new object[]
        {
            Constantes.AsistenciaSinRegistro,
            Constantes.AsistenciaPresente,
            Constantes.AsistenciaFalta,
            Constantes.AsistenciaTardanza
        });
    }
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
            if (aulaActual <= 0 || !ConfirmarSalida()) return;
            using FrmMatrizSemanal matriz = new FrmMatrizSemanal(servicio, aulaActual, fechaActual);
            matriz.ShowDialog(this);
        };
    }
    public void ActualizarListado()
    {
        cargando = true;
        cmbAula.Items.Clear();
        foreach (Aula aula in servicio.Aulas.Listar()) cmbAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        if (cmbAula.Items.Count == 0 || servicio.Asistencias.Listar().Any(x => x.matriculaId == 0))
            cmbAula.Items.Add(new Opcion(SinAula, Constantes.AsistenciaPorEstudiante));
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
        colObservacion.Visible = aulaActual != SinAula;
        if (aulaActual == SinAula) CargarEstudiantesSinAula();
        else CargarMatriculas();

        cargando = false;
        modificado = false;
        btnGuardar.Enabled = dgvAsistencia.Rows.Count > 0;
        btnMatrizSemanal.Enabled = aulaActual > 0;
        if (aulaActual == SinAula)
        {
            lblNota.Text = dgvAsistencia.Rows.Count == 0 ? "No hay estudiantes para la fecha seleccionada." :
                "Edita el estado por estudiante y guarda los cambios. Sin registro no cuenta como falta.";
        }
        else
        {
            lblNota.Text = dgvAsistencia.Rows.Count == 0 ? "No hay matrículas para el aula y fecha seleccionados." :
                "Edita el estado y la observación. Sin registro no se cuenta como falta. Guarda los cambios antes de salir.";
        }
        ActualizarTotales();
    }

    private void CargarEstudiantesSinAula()
    {
        List<Asistencia> registros = servicio.Asistencias.MarcasSinAula(fechaActual);
        foreach (Estudiante estudiante in servicio.Asistencias.EstudiantesSinAula(fechaActual))
        {
            Asistencia? asistencia = registros.Find(x => x.codigoEstudiante.Equals(
                estudiante.codigo, StringComparison.OrdinalIgnoreCase));
            int fila = dgvAsistencia.Rows.Add(estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos,
                asistencia?.estado.ToString() ?? Constantes.AsistenciaSinRegistro, "");
            dgvAsistencia.Rows[fila].Tag = estudiante.codigo;
        }
    }

    private void CargarMatriculas()
    {
        List<Estudiante> estudiantes = servicio.Estudiantes;
        List<Asistencia> asistencias = servicio.Asistencias.Listar();
        foreach (Matricula matricula in servicio.Asistencias.MatriculasDelDia(aulaActual, fechaActual))
        {
            Estudiante estudiante = estudiantes.Single(x => x.id == matricula.estudianteId);
            Asistencia? asistencia = asistencias.SingleOrDefault(x => x.matriculaId == matricula.id && x.fecha.Date == fechaActual);
            int fila = dgvAsistencia.Rows.Add(estudiante.codigo, servicio.NombreEstudiante(estudiante.id),
                asistencia?.estado.ToString() ?? Constantes.AsistenciaSinRegistro, asistencia?.observacion ?? "");
            dgvAsistencia.Rows[fila].Tag = matricula.id;
        }
    }
    private void ActualizarTotales()
    {
        var filas = dgvAsistencia.Rows.Cast<DataGridViewRow>().ToList();
        lblPresentes.Text = filas.Count(x => (string?)x.Cells[colEstado.Index].Value == Constantes.AsistenciaPresente) + " presentes";
        lblFaltas.Text = filas.Count(x => (string?)x.Cells[colEstado.Index].Value == Constantes.AsistenciaFalta) + " faltas";
        lblTardanzas.Text = filas.Count(x => (string?)x.Cells[colEstado.Index].Value == Constantes.AsistenciaTardanza) + " tardanzas";
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
                string estado = (string?)fila.Cells[colEstado.Index].Value ?? Constantes.AsistenciaSinRegistro;
                if (estado == Constantes.AsistenciaSinRegistro) continue;
                Asistencia registro = new Asistencia { estado = Enum.Parse<EstadoAsistencia>(estado) };
                if (fila.Tag is string codigo)
                {
                    registro.codigoEstudiante = codigo;
                }
                else
                {
                    registro.matriculaId = (int)fila.Tag!;
                    registro.observacion = fila.Cells[colObservacion.Index].Value?.ToString() ?? "";
                }
                registros.Add(registro);
            }
            if (aulaActual == SinAula) servicio.Asistencias.GuardarDiaSinAula(fechaActual, registros);
            else servicio.Asistencias.GuardarDia(aulaActual, fechaActual, registros);
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
