using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmMatriculas : Form
{
    private readonly EscolarService servicio = null!;
    private bool cargando;
    public FrmMatriculas() { InitializeComponent(); }
    public FrmMatriculas(EscolarService servicio) : this()
    {
        this.servicio = servicio;
        cboAula.SelectedIndexChanged += (_, _) => { if (!cargando) Listar(); };
        cboEstado.SelectedIndexChanged += (_, _) => { if (!cargando) Listar(); };
        dgvMatriculas.CellContentClick += Cancelar;
    }
    public void ActualizarListado()
    {
        int id = (cboAula.SelectedItem as Opcion)?.Id ?? 0;
        cargando = true;
        cboAula.Items.Clear();
        cboAula.Items.Add(new Opcion(0, Constantes.FiltrosAulas[0]));
        foreach (Aula aula in servicio.Aulas.Listar()) cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
        cboAula.SelectedIndex = Math.Max(0, cboAula.Items.Cast<Opcion>().ToList().FindIndex(x => x.Id == id));
        cargando = false;
        Listar();
    }
    private void Listar()
    {
        int aulaId = (cboAula.SelectedItem as Opcion)?.Id ?? 0;
        dgvMatriculas.Rows.Clear();
        foreach (Matricula matricula in servicio.Matriculas.Listar().Where(x => (aulaId == 0 || x.aulaId == aulaId) &&
            (cboEstado.SelectedIndex == 0 || (cboEstado.SelectedIndex == 1 ? x.Activa : !x.Activa))))
        {
            Aula aula = servicio.Aulas.Listar().Single(x => x.id == matricula.aulaId);
            int fila = dgvMatriculas.Rows.Add(matricula.Codigo, servicio.NombreEstudiante(matricula.estudianteId),
                aula.Descripcion, matricula.fecha.ToString("dd/MM/yyyy"), matricula.Activa ? "Activa" : "Cancelada", matricula.Activa ? "Cancelar" : "—");
            dgvMatriculas.Rows[fila].Tag = matricula;
        }
        lblResultados.Text = dgvMatriculas.Rows.Count + " matrículas · La cancelación libera el cupo y conserva el historial.";
    }
    private void Cancelar(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colCancelar.Index) return;
        Matricula matricula = (Matricula)dgvMatriculas.Rows[e.RowIndex].Tag!;
        if (!matricula.Activa || MessageBox.Show(this, "¿Cancelar esta matrícula? Se conservará su historial.",
            "Cancelar matrícula", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try { servicio.Matriculas.Cancelar(matricula.id); Listar(); }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using FrmMatricula formulario = new FrmMatricula(servicio);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarListado();
    }
}
