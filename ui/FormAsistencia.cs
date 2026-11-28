using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;

public partial class FormAsistencia : Form
{
    private readonly EstudianteService servicioEstudiantes = null!;
    private readonly AsistenciaService servicioAsistencia = null!;
    private readonly AulaService servicioAulas = null!;
    private readonly MatriculaService servicioMatriculas = null!;
    private DateTime fechaListado = DateTime.Today;
    private int aulaListado;
    private bool cargando;

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
        dtpFecha.MaxDate = DateTime.Today;
        dtpFecha.Value = DateTime.Today;
    }

    public FormAsistencia(EstudianteService servicioEstudiantes, AsistenciaService servicioAsistencia,
        AulaService servicioAulas, MatriculaService servicioMatriculas) : this()
    {
        this.servicioEstudiantes = servicioEstudiantes;
        this.servicioAsistencia = servicioAsistencia;
        this.servicioAulas = servicioAulas;
        this.servicioMatriculas = servicioMatriculas;
    }

    public void ActualizarAlMostrar()
    {
        cargando = true;
        cmbAula.Items.Clear();
        foreach (Aula aula in servicioAulas.Listar())
        {
            cmbAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        }
        if (cmbAula.Items.Count > 0)
        {
            cmbAula.SelectedIndex = Math.Max(0, BuscarIndiceAula(aulaListado));
        }
        aulaListado = (cmbAula.SelectedItem as Opcion)?.Id ?? 0;
        fechaListado = dtpFecha.Value.Date;
        ActualizarListado();
    }

    private int BuscarIndiceAula(int aulaId)
    {
        for (int indice = 0; indice < cmbAula.Items.Count; indice++)
        {
            if (cmbAula.Items[indice] is Opcion opcion && opcion.Id == aulaId)
            {
                return indice;
            }
        }

        return -1;
    }

    private void ActualizarListado()
    {
        cargando = true;
        dgvAsistencia.Rows.Clear();
        Dictionary<int, Asistencia> registrosPorMatricula = new Dictionary<int, Asistencia>();
        foreach (Asistencia registro in servicioAsistencia.Listar())
        {
            if (registro.fecha.Date == fechaListado)
            {
                registrosPorMatricula.Add(registro.matriculaId, registro);
            }
        }

        Dictionary<int, Estudiante> estudiantesPorId = new Dictionary<int, Estudiante>();
        foreach (Estudiante estudiante in servicioEstudiantes.Listar())
        {
            estudiantesPorId.Add(estudiante.id, estudiante);
        }

        foreach (Matricula matricula in servicioAsistencia.MatriculasDelDia(aulaListado, fechaListado))
        {
            Estudiante estudiante = estudiantesPorId[matricula.estudianteId];
            registrosPorMatricula.TryGetValue(matricula.id, out Asistencia? registro);
            AgregarFila(estudiante, matricula.id, registro);
        }

        dgvAsistencia.ClearSelection();
        btnMatrizSemanal.Enabled = aulaListado != 0;
        if (dgvAsistencia.Rows.Count == 0)
        {
            lblNota.Text = aulaListado == 0 ? "Registra un aula para tomar asistencia." :
                "No hay matrículas para el aula y fecha seleccionados.";
        }
        else
        {
            lblNota.Text = "Usa Marcar para cambiar el estado. Sin registro no cuenta como falta.";
        }
        cargando = false;
        ActualizarResumen();
    }

    private void AgregarFila(Estudiante estudiante, int matriculaId, Asistencia? registro)
    {
        string estado = registro?.estado ?? Constantes.AsistenciaSinRegistro;
        string observacion = registro?.observacion ?? "";
        int indice = dgvAsistencia.Rows.Add(estudiante.codigo, estudiante.nombre + " " + estudiante.apellidos, estado,
            observacion);
        DataGridViewRow fila = dgvAsistencia.Rows[indice];
        fila.Tag = matriculaId;
        fila.Cells[colEstado.Index].Tag = estado;
        fila.Cells[colObservacion.Index].Tag = observacion;
    }

    private void FiltroCambiado(object? sender, EventArgs e)
    {
        if (cargando || cmbAula.SelectedItem is not Opcion opcion)
        {
            return;
        }

        if (!ConfirmarSalida())
        {
            cargando = true;
            cmbAula.SelectedIndex = BuscarIndiceAula(aulaListado);
            dtpFecha.Value = fechaListado;
            cargando = false;
            return;
        }

        aulaListado = opcion.Id;
        fechaListado = dtpFecha.Value.Date;
        ActualizarListado();
    }

    public bool ConfirmarSalida()
    {
        dgvAsistencia.EndEdit();
        if (!btnGuardar.Enabled)
        {
            return true;
        }

        DialogResult respuesta = MessageBox.Show(this, "Hay cambios de asistencia sin guardar. ¿Desea guardarlos?",
            "Asistencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (respuesta == DialogResult.Cancel)
        {
            return false;
        }
        if (respuesta == DialogResult.Yes)
        {
            return Guardar();
        }
        ActualizarListado();
        return true;
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        Guardar();
    }

    private bool Guardar()
    {
        try
        {
            dgvAsistencia.EndEdit();
            List<Asistencia> cambios = new List<Asistencia>();
            List<Asistencia> bajas = new List<Asistencia>();
            foreach (DataGridViewRow fila in dgvAsistencia.Rows)
            {
                if (!FilaModificada(fila))
                {
                    continue;
                }

                int matriculaId = (int)fila.Tag!;
                string estado = (string)fila.Cells[colEstado.Index].Value;
                if (estado == Constantes.AsistenciaSinRegistro)
                {
                    bajas.Add(new Asistencia { matriculaId = matriculaId });
                    continue;
                }

                cambios.Add(new Asistencia
                {
                    matriculaId = matriculaId,
                    estado = estado,
                    observacion = fila.Cells[colObservacion.Index].Value?.ToString() ?? ""
                });
            }

            servicioAsistencia.Guardar(aulaListado, fechaListado, cambios, bajas);
            ActualizarListado();
            lblNota.Text = "Asistencia guardada. Puedes corregirla y volver a guardar.";
            return true;
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    private void dgvAsistencia_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colAcciones.Index)
        {
            return;
        }

        DataGridViewCell celda = dgvAsistencia.Rows[e.RowIndex].Cells[colEstado.Index];
        switch (celda.Value)
        {
            case Constantes.AsistenciaPresente:
                celda.Value = Constantes.AsistenciaFalta;
                break;
            case Constantes.AsistenciaFalta:
                celda.Value = Constantes.AsistenciaTardanza;
                break;
            default:
                celda.Value = Constantes.AsistenciaPresente;
                break;
        }
    }

    private void dgvAsistencia_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (!cargando && e.RowIndex >= 0)
        {
            ActualizarResumen();
        }
    }

    private void dgvAsistencia_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (dgvAsistencia.IsCurrentCellDirty && dgvAsistencia.CurrentCell is DataGridViewComboBoxCell)
        {
            dgvAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private bool FilaModificada(DataGridViewRow fila)
    {
        DataGridViewCell estado = fila.Cells[colEstado.Index];
        DataGridViewCell observacion = fila.Cells[colObservacion.Index];
        return !Equals(estado.Tag, estado.Value) ||
            (!Equals(estado.Value, Constantes.AsistenciaSinRegistro) &&
             !Equals(observacion.Tag, observacion.Value?.ToString() ?? ""));
    }

    private void ActualizarResumen()
    {
        int presentes = 0;
        int faltas = 0;
        int tardanzas = 0;
        bool hayCambios = false;
        foreach (DataGridViewRow fila in dgvAsistencia.Rows)
        {
            switch (fila.Cells[colEstado.Index].Value)
            {
                case Constantes.AsistenciaPresente: presentes++; break;
                case Constantes.AsistenciaFalta: faltas++; break;
                case Constantes.AsistenciaTardanza: tardanzas++; break;
            }
            if (FilaModificada(fila))
            {
                hayCambios = true;
            }
        }

        lblPresentes.Text = presentes + " presentes";
        lblFaltas.Text = faltas + " faltas";
        lblTardanzas.Text = tardanzas + " tardanzas";
        btnGuardar.Enabled = hayCambios;
    }

    private void btnMatrizSemanal_Click(object? sender, EventArgs e)
    {
        Aula? aula = null;
        foreach (Aula item in servicioAulas.Listar())
        {
            if (item.id == aulaListado)
            {
                aula = item;
                break;
            }
        }
        if (aula == null || !ConfirmarSalida())
        {
            return;
        }

        using FrmMatrizSemanal matriz = new FrmMatrizSemanal(aula, fechaListado, servicioEstudiantes,
            servicioMatriculas, servicioAsistencia);
        matriz.ShowDialog(this);
    }
}
