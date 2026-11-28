using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;
namespace GestionEstudiante;

public partial class FrmReportes : Form
{
    private readonly EscolarService servicio = null!;
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
    public FrmReportes(EscolarService servicio) : this()
    {
        this.servicio = servicio;
        numAnio.Value = Constantes.AnioEscolar;
        dtpDesde.Value = new DateTime(Constantes.AnioEscolar, 1, 1);
        dtpHasta.Value = DateTime.Today;
        cboTipo.SelectedIndex = 0;
        cboTipo.SelectedIndexChanged += (_, _) => Generar();
        cboAula.SelectedIndexChanged += (_, _) => Generar();
        numAnio.ValueChanged += (_, _) => CambiarAnio();
        dtpDesde.ValueChanged += (_, _) => Generar();
        dtpHasta.ValueChanged += (_, _) => Generar();
    }
    private void CambiarAnio()
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
        foreach (Aula aula in servicio.Aulas.Listar().Where(x => x.anio == numAnio.Value))
            cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        cboAula.SelectedIndex = Math.Max(0, cboAula.Items.Cast<Opcion>().ToList().FindIndex(x => x.Id == id));
        cargando = false;
        Generar();
    }
    private void Generar()
    {
        if (cargando) return;
        bool asistencia = cboTipo.SelectedIndex == 1;
        dtpDesde.Enabled = dtpHasta.Enabled = asistencia;
        reporte = asistencia ? new ReporteAsistencia(servicio) : new ReporteOcupacion(servicio);
        dgvReporte.Columns.Clear();
        resultado = null;
        btnExportar.Enabled = false;
        try
        {
            resultado = reporte.Generar(new FiltroReporte((int)numAnio.Value,
                (cboAula.SelectedItem as Opcion)?.Id ?? 0, dtpDesde.Value, dtpHasta.Value));
            foreach (string columna in resultado.Columnas) dgvReporte.Columns.Add(columna, columna);
            foreach (string[] fila in resultado.Filas) dgvReporte.Rows.Add(fila.Cast<object>().ToArray());
            lblResumen.Text = resultado.Filas.Count == 0 ? "No hay datos para los filtros seleccionados." : asistencia
                ? "Asistencia = (presentes + tardanzas) / registros. Las fechas sin registro no cuentan como faltas."
                : $"{resultado.Filas.Count} aulas · {resultado.Filas.Sum(x => int.Parse(x[2]))} matrículas activas · {resultado.Filas.Sum(x => int.Parse(x[3]))} cupos disponibles.";
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
