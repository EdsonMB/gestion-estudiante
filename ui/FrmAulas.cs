using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;
namespace GestionEstudiante;

public partial class FrmAulas : Form
{
    private readonly AulaService servicio = null!;
    public FrmAulas()
    {
        InitializeComponent();
        cboNivel.Items.AddRange(new object[]
        {
            Constantes.TodosLosNiveles,
            Constantes.NivelInicial,
            Constantes.NivelPrimaria,
            Constantes.NivelSecundaria
        });
    }
    public FrmAulas(AulaService servicio) : this()
    {
        this.servicio = servicio;
        numAnio.Value = Constantes.AnioEscolar;
        cboNivel.SelectedIndex = 0;
        cboNivel.SelectedIndexChanged += FiltroCambiado;
        numAnio.ValueChanged += FiltroCambiado;
    }

    private void FiltroCambiado(object? sender, EventArgs e)
    {
        ActualizarListado();
    }

    public void ActualizarListado()
    {
        dgvAulas.Rows.Clear();
        foreach (Aula aula in servicio.Listar())
        {
            if (aula.anio != numAnio.Value || (cboNivel.SelectedIndex != 0 && aula.nivel != cboNivel.Text))
            {
                continue;
            }

            int ocupados = servicio.Ocupados(aula.id);
            int fila = dgvAulas.Rows.Add(aula.nivel, aula.grado, aula.seccion, aula.anio,
                ocupados + " / " + aula.capacidad, aula.activo ? (aula.capacidad - ocupados) + " libres" : "Inactiva",
                "Editar", aula.activo ? "Desactivar" : "Activar");
            dgvAulas.Rows[fila].Tag = aula;
        }
        lblResumen.Text = dgvAulas.Rows.Count + " aulas · La capacidad no puede ser menor que la ocupación actual.";
    }
    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using FrmAula formulario = new FrmAula(servicio, null, (int)numAnio.Value);
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
                using FrmAula formulario = new FrmAula(servicio, aula, aula.anio);
                formulario.ShowDialog(this);
            }
            else if (MessageBox.Show(this, $"¿Desea {(aula.activo ? "desactivar" : "activar")} {aula.Descripcion}?",
                "Estado del aula", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                servicio.CambiarEstado(aula.id);
            ActualizarListado();
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
