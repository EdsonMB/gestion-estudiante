using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;
namespace GestionEstudiante;

public partial class FrmAulas : Form
{
    private readonly EscolarService servicio = null!;
    public FrmAulas() { InitializeComponent(); }
    public FrmAulas(EscolarService servicio) : this()
    {
        this.servicio = servicio;
        numAnio.Value = Constantes.AnioEscolar;
        cboNivel.SelectedIndex = 0;
        cboNivel.SelectedIndexChanged += (_, _) => ActualizarListado();
        numAnio.ValueChanged += (_, _) => ActualizarListado();
    }
    public void ActualizarListado()
    {
        dgvAulas.Rows.Clear();
        foreach (Aula aula in servicio.Aulas.Listar().Where(x => x.anio == numAnio.Value &&
            (cboNivel.SelectedIndex == 0 || x.nivel == cboNivel.Text)))
        {
            int ocupados = servicio.Aulas.Ocupados(aula.id);
            int fila = dgvAulas.Rows.Add(aula.nivel, aula.grado, aula.seccion, aula.anio,
                ocupados + " / " + aula.capacidad, aula.activo ? (aula.capacidad - ocupados) + " libres" : "Inactiva",
                "Editar", aula.activo ? "Desactivar" : "Activar");
            dgvAulas.Rows[fila].Tag = aula;
        }
        lblResumen.Text = dgvAulas.Rows.Count + " aulas · La capacidad no puede ser menor que la ocupación actual.";
    }
    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using FrmAula formulario = new FrmAula(servicio.Aulas, null, (int)numAnio.Value);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarListado();
    }
    private void dgvAulas_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 6) return;
        Aula aula = (Aula)dgvAulas.Rows[e.RowIndex].Tag!;
        try
        {
            if (e.ColumnIndex == 6)
            {
                using FrmAula formulario = new FrmAula(servicio.Aulas, aula, aula.anio);
                formulario.ShowDialog(this);
            }
            else if (MessageBox.Show(this, $"¿Desea {(aula.activo ? "desactivar" : "activar")} {aula.Descripcion}?",
                "Estado del aula", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                servicio.Aulas.CambiarEstado(aula.id);
            ActualizarListado();
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
