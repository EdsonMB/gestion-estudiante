using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;
namespace GestionEstudiante;

public partial class FrmReportes : Form
{
    private readonly AulaService servicioAulas = null!;
    private readonly ReporteOcupacion reporteOcupacion = null!;
    private readonly ReporteAsistencia reporteAsistencia = null!;
    private Reporte reporte = null!;
    private ResultadoReporte? resultado;
    private bool cargando;
    public FrmReportes()
    {
        InitializeComponent();
        cboTipo.Items.AddRange(new object[]
        {
            Constantes.ReporteOcupacion,
            Constantes.ReporteAsistencia
        });
    }
    public FrmReportes(EstudianteService servicioEstudiantes, AulaService servicioAulas,
        MatriculaService servicioMatriculas, AsistenciaService servicioAsistencia) : this()
    {
        this.servicioAulas = servicioAulas;
        reporteOcupacion = new ReporteOcupacion(servicioAulas);
        reporteAsistencia = new ReporteAsistencia(servicioEstudiantes, servicioMatriculas, servicioAsistencia);
        numAnio.Value = Constantes.AnioEscolar;
        dtpDesde.Value = new DateTime(Constantes.AnioEscolar, 1, 1);
        dtpHasta.Value = DateTime.Today;
        cboTipo.SelectedIndex = 0;
        cboTipo.SelectedIndexChanged += FiltroCambiado;
        cboAula.SelectedIndexChanged += FiltroCambiado;
        numAnio.ValueChanged += AnioCambiado;
        dtpDesde.ValueChanged += FiltroCambiado;
        dtpHasta.ValueChanged += FiltroCambiado;
    }

    private void FiltroCambiado(object? sender, EventArgs e)
    {
        Generar();
    }

    private void AnioCambiado(object? sender, EventArgs e)
    {
        cargando = true;
        int anio = (int)numAnio.Value;
        dtpDesde.Value = new DateTime(anio, 1, 1);
        dtpHasta.Value = anio == DateTime.Today.Year ? DateTime.Today : new DateTime(anio, 12, 31);
        cargando = false;
        ActualizarListado();
    }
    public void ActualizarListado()
    {
        cargando = true;
        int id = (cboAula.SelectedItem as Opcion)?.Id ?? 0;
        cboAula.Items.Clear();
        cboAula.Items.Add(new Opcion(0, Constantes.TodasLasAulas));
        int indice = 0;
        foreach (Aula aula in servicioAulas.Listar())
        {
            if (aula.anio == numAnio.Value)
            {
                int posicion = cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
                if (aula.id == id)
                {
                    indice = posicion;
                }
            }
        }
        cboAula.SelectedIndex = indice;
        cargando = false;
        Generar();
    }
    private void Generar()
    {
        if (cargando) return;
        bool asistencia = cboTipo.SelectedIndex == 1;
        dtpDesde.Enabled = dtpHasta.Enabled = asistencia;
        reporte = asistencia ? reporteAsistencia : reporteOcupacion;
        dgvReporte.Columns.Clear();
        resultado = null;
        btnExportar.Enabled = false;
        try
        {
            resultado = reporte.Generar(new FiltroReporte((int)numAnio.Value,
                (cboAula.SelectedItem as Opcion)?.Id ?? 0, dtpDesde.Value, dtpHasta.Value));
            foreach (string columna in resultado.Columnas)
            {
                dgvReporte.Columns.Add(columna, columna);
            }

            int matriculados = 0;
            int cuposDisponibles = 0;
            foreach (string[] fila in resultado.Filas)
            {
                dgvReporte.Rows.Add(fila);
                if (!asistencia)
                {
                    matriculados += int.Parse(fila[2]);
                    cuposDisponibles += int.Parse(fila[3]);
                }
            }

            if (resultado.Filas.Count == 0)
            {
                lblResumen.Text = "No hay datos para los filtros seleccionados.";
            }
            else if (asistencia)
            {
                lblResumen.Text = "Asistencia = (presentes + tardanzas) / registros. Las fechas sin registro no cuentan como faltas.";
            }
            else
            {
                lblResumen.Text = $"{resultado.Filas.Count} aulas · {matriculados} matrículas activas · {cuposDisponibles} cupos disponibles.";
            }
            btnExportar.Enabled = resultado.Filas.Count > 0;
        }
        catch (ArgumentException error) { lblResumen.Text = error.Message; }
    }
    private void btnExportar_Click(object? sender, EventArgs e)
    {
        if (resultado == null) return;
        using SaveFileDialog dialogo = new SaveFileDialog { Filter = "Archivo TXT (*.txt)|*.txt", DefaultExt = "txt", FileName = "reporte.txt", AddExtension = true };
        if (dialogo.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            reporte.Exportar(dialogo.FileName, resultado);
            MessageBox.Show(this, "Reporte TXT exportado con campos separados por |.", "Reportes");
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo exportar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
