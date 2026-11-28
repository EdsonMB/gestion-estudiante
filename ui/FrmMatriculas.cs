using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmMatriculas : Form
{
    private readonly EstudianteService servicioEstudiantes = null!;
    private readonly AulaService servicioAulas = null!;
    private readonly MatriculaService servicioMatriculas = null!;
    private bool cargando;
    public FrmMatriculas()
    {
        InitializeComponent();
        cboAula.Items.Add(new Opcion(0, Constantes.TodasLasAulas));
        cboAula.SelectedIndex = 0;
        cboEstado.Items.AddRange(new object[]
        {
            Constantes.MatriculasActivasYCanceladas,
            Constantes.MatriculasSoloActivas,
            Constantes.MatriculasSoloCanceladas
        });
        cboEstado.SelectedIndex = 0;
    }
    public FrmMatriculas(EstudianteService servicioEstudiantes, AulaService servicioAulas,
        MatriculaService servicioMatriculas) : this()
    {
        this.servicioEstudiantes = servicioEstudiantes;
        this.servicioAulas = servicioAulas;
        this.servicioMatriculas = servicioMatriculas;
        cboAula.SelectedIndexChanged += FiltroCambiado;
        cboEstado.SelectedIndexChanged += FiltroCambiado;
        dgvMatriculas.CellContentClick += Cancelar;
    }

    private void FiltroCambiado(object? sender, EventArgs e)
    {
        if (!cargando)
        {
            Listar();
        }
    }

    public void ActualizarListado()
    {
        int id = (cboAula.SelectedItem as Opcion)?.Id ?? 0;
        cargando = true;
        cboAula.Items.Clear();
        cboAula.Items.Add(new Opcion(0, Constantes.TodasLasAulas));
        int indice = 0;
        foreach (Aula aula in servicioAulas.Listar())
        {
            int posicion = cboAula.Items.Add(new Opcion(aula.id, aula.Descripcion));
            if (aula.id == id)
            {
                indice = posicion;
            }
        }
        cboAula.SelectedIndex = indice;
        cargando = false;
        Listar();
    }
    private void Listar()
    {
        int aulaId = (cboAula.SelectedItem as Opcion)?.Id ?? 0;
        int filtroEstado = cboEstado.SelectedIndex;
        dgvMatriculas.Rows.Clear();
        Dictionary<int, Aula> aulasPorId = new Dictionary<int, Aula>();
        foreach (Aula aula in servicioAulas.Listar())
        {
            aulasPorId.Add(aula.id, aula);
        }

        foreach (Matricula matricula in servicioMatriculas.Listar())
        {
            if (aulaId != 0 && matricula.aulaId != aulaId)
            {
                continue;
            }
            if (filtroEstado != 0 && matricula.Activa != (filtroEstado == 1))
            {
                continue;
            }

            Aula aula = aulasPorId[matricula.aulaId];
            int fila = dgvMatriculas.Rows.Add(matricula.Codigo, servicioEstudiantes.NombreEstudiante(matricula.estudianteId),
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
        try { servicioMatriculas.Cancelar(matricula.id); Listar(); }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using FrmMatricula formulario = new FrmMatricula(servicioEstudiantes, servicioAulas, servicioMatriculas);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarListado();
    }
}
