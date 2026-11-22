using GestionEstudiante.model;
using GestionEstudiante.repository;
using GestionEstudiante.service.GestionEstudiante.service;

namespace GestionEstudiante;

public partial class FormAsistencia : Form
{
    private readonly GestorBaseDatos gestor;
    private readonly AsistenciaService servicio;
    public FormAsistencia(GestorBaseDatos gestor, AsistenciaService servicio)
    {
        this.gestor = gestor;
        this.servicio = servicio;
        InitializeComponent();
    }
    private void FrmAsistencia_Load(object sender, EventArgs e)
    {

        cmbAula.DropDownStyle = ComboBoxStyle.DropDownList;
        RefrescarTodo();
        CargarAulas();
        CargarGrilla();
    }
    private void Filtro_Changed(object sender, EventArgs e)
    {
        CargarGrilla();
    }
    private void CargarAulas()
    {
        cmbAula.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbAula.Items.Clear();

        cmbAula.Items.Add("Aula A");
        cmbAula.Items.Add("Aula B");

        if (gestor != null && gestor.Datos != null && gestor.Datos.estudiantes != null)
        {
            for (int i = 0; i < gestor.Datos.estudiantes.Count; i++)
            {
                Estudiante est = gestor.Datos.estudiantes[i];
                if (est != null && est.activo && !string.IsNullOrWhiteSpace(est.aula))
                {
                    string aulaLimpia = est.aula.Trim();
                    if (!cmbAula.Items.Contains(aulaLimpia))
                    {
                        cmbAula.Items.Add(aulaLimpia);
                    }
                }
            }
        }

        if (cmbAula.Items.Count > 0 && cmbAula.SelectedIndex < 0)
        {
            cmbAula.SelectedIndex = 0;
        }

    }
    private void CargarGrilla()
    {
        dgvAsistencia.Rows.Clear();

        int presentes = 0;
        int faltas = 0;
        int tardanzas = 0;

        if (gestor == null || gestor.Datos == null || gestor.Datos.estudiantes == null)
        {
            ActualizarResumen(presentes, faltas, tardanzas);
            return;
        }

        string aulaSeleccionada = cmbAula.SelectedItem != null ? cmbAula.SelectedItem.ToString().Trim() : "";
        DateTime fechaSeleccionada = dtpFecha.Value.Date;
        List<Asistencia> registros = servicio.Listar();

        for (int i = 0; i < gestor.Datos.estudiantes.Count; i++)
        {
            Estudiante est = gestor.Datos.estudiantes[i];
            if (est == null || !est.activo)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(aulaSeleccionada)
                && !string.Equals((est.aula ?? "").Trim(), aulaSeleccionada, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            string estado = ObtenerEstado(registros, est.codigo, fechaSeleccionada);

            if (string.Equals(estado, "Presente", StringComparison.OrdinalIgnoreCase))
            {
                presentes++;
            }
            else if (string.Equals(estado, "Falta", StringComparison.OrdinalIgnoreCase))
            {
                faltas++;
            }
            else if (string.Equals(estado, "Tardanza", StringComparison.OrdinalIgnoreCase))
            {
                tardanzas++;
            }

            int filaIndex = dgvAsistencia.Rows.Add(
                est.codigo,
                est.nombre + " " + est.apellidos,
                estado,
                "Marcar"
            );
            dgvAsistencia.Rows[filaIndex].Tag = est;
        }

        ActualizarResumen(presentes, faltas, tardanzas);
    }
    
    private void ActualizarResumen(int presentes, int faltas, int tardanzas)
    {
        lblPresentes.Text = presentes + " presentes";
        lblFaltas.Text = faltas + " faltas";
        lblTardanzas.Text = tardanzas + " tardanzas";
    }
    private static string ObtenerEstado(List<Asistencia> registros, string codigoEstudiante, DateTime fecha)
    {
        if (registros == null)
        {
            return "Sin registro";
        }

        for (int i = 0; i < registros.Count; i++)
        {
            Asistencia reg = registros[i];
            if (reg != null
                && string.Equals(reg.codigoEstudiante, codigoEstudiante, StringComparison.OrdinalIgnoreCase)
                && reg.fecha.Date == fecha.Date)
            {
                return string.IsNullOrWhiteSpace(reg.estado) ? "Sin registro" : reg.estado;
            }
        }
        return "Sin registro";
    }
    private void dtpFecha_ValueChanged(object? sender, EventArgs e)
    {
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        DateTime fechaSeleccionada = dtpFecha.Value.Date;

        try
        {
            for (int i = 0; i < dgvAsistencia.Rows.Count; i++)
            {
                DataGridViewRow fila = dgvAsistencia.Rows[i];
                if (fila.IsNewRow)
                {
                    continue;
                }

                Estudiante est = fila.Tag as Estudiante;
                string codigo = est != null
                    ? est.codigo
                    : (fila.Cells[0].Value != null ? fila.Cells[0].Value.ToString() : "");

                string estado = fila.Cells[2].Value != null
                    ? fila.Cells[2].Value.ToString().Trim()
                    : "Sin registro";

                if (string.IsNullOrWhiteSpace(estado) || estado == "Sin registro")
                {
                    continue;
                }

                Asistencia item = new Asistencia();
                item.codigoEstudiante = codigo;
                item.fecha = fechaSeleccionada;
                item.estado = estado;

                servicio.Guardar(item);
            }

            MessageBox.Show("Asistencia guardada correctamente.", "Asistencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarGrilla();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvAsistencia_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow fila = dgvAsistencia.Rows[e.RowIndex];
        string estadoActual = fila.Cells[2].Value != null ? fila.Cells[2].Value.ToString() : "Sin registro";

        string siguienteEstado;
        if (estadoActual == "Sin registro" || estadoActual == "Tardanza")
        {
            siguienteEstado = "Presente";
        }
        else if (estadoActual == "Presente")
        {
            siguienteEstado = "Falta";
        }
        else
        {
            siguienteEstado = "Tardanza";
        }

        fila.Cells[2].Value = siguienteEstado;
        RecalcularResumen();
    }
    private void RecalcularResumen()
    {
        int presentes = 0;
        int faltas = 0;
        int tardanzas = 0;

        for (int i = 0; i < dgvAsistencia.Rows.Count; i++)
        {
            DataGridViewRow fila = dgvAsistencia.Rows[i];
            if (fila.IsNewRow)
            {
                continue;
            }

            string estado = fila.Cells[2].Value != null ? fila.Cells[2].Value.ToString() : "";
            if (estado == "Presente") presentes++;
            else if (estado == "Falta") faltas++;
            else if (estado == "Tardanza") tardanzas++;
        }

        ActualizarResumen(presentes, faltas, tardanzas);
    }
    private void RefrescarTodo()
    {
        string aulaPrevia = cmbAula.SelectedItem != null ? cmbAula.SelectedItem.ToString() : "";

        CargarAulas();

        if (!string.IsNullOrEmpty(aulaPrevia) && cmbAula.Items.Contains(aulaPrevia))
        {
            cmbAula.SelectedItem = aulaPrevia;
        }

        CargarGrilla();
    }
   

    private void FrmAsistencia_Activated(object sender, EventArgs e)
    {
        RefrescarTodo();
    }

    private void cmbAula_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void lblNota_Click(object sender, EventArgs e)
    {

    }

    private void lblPresentes_Click(object sender, EventArgs e)
    {

    }

    private void lblFaltas_Click(object sender, EventArgs e)
    {

    }

    private void lblTardanzas_Click(object sender, EventArgs e)
    {

    }

    private void btnMatrizSemanal_Click(object sender, EventArgs e)
    {

    }
}
