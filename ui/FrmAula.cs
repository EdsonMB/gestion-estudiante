using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmAula : Form
{
    private readonly AulaService servicio = null!;
    private readonly Aula? original;
    public FrmAula()
    {
        InitializeComponent();
        numGrado.Minimum = Constantes.GradoMinimoPrimariaYSecundaria;
        numGrado.Maximum = Constantes.GradoMaximoPrimaria;
        txtSeccion.MaxLength = Constantes.LongitudMaximaSeccion;
        numAnio.Minimum = Constantes.AnioEscolarMinimo;
        numAnio.Maximum = Constantes.AnioEscolarMaximo;
        numCapacidad.Minimum = Constantes.CapacidadMinimaAula;
        numCapacidad.Maximum = Constantes.CapacidadMaximaAula;

        cboNivel.Items.AddRange(new object[]
        {
            Constantes.NivelInicial,
            Constantes.NivelPrimaria,
            Constantes.NivelSecundaria
        });
    }
    public FrmAula(AulaService servicio, Aula? aula, int anio) : this()
    {
        this.servicio = servicio;
        original = aula;
        Text = lblTitulo.Text = aula == null ? "Nueva aula" : "Editar aula";
        cboNivel.SelectedIndexChanged += NivelCambiado;
        cboNivel.SelectedItem = aula?.nivel ?? Constantes.NivelPrimaria;
        numGrado.Value = aula?.grado ?? Constantes.GradoMinimoPrimariaYSecundaria;
        txtSeccion.Text = aula?.seccion ?? "A";
        numAnio.Value = anio;
        numCapacidad.Value = aula?.capacidad ?? 30;
    }
    private void NivelCambiado(object? sender, EventArgs e)
    {
        numGrado.Minimum = cboNivel.Text == Constantes.NivelInicial
            ? Constantes.GradoMinimoInicial : Constantes.GradoMinimoPrimariaYSecundaria;
        numGrado.Maximum = cboNivel.Text == Constantes.NivelPrimaria
            ? Constantes.GradoMaximoPrimaria : Constantes.GradoMaximoInicialYSecundaria;
    }
    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        try
        {
            servicio.Guardar(new Aula { id = original?.id ?? 0, nivel = cboNivel.Text,
                grado = (int)numGrado.Value, seccion = txtSeccion.Text, anio = (int)numAnio.Value,
                capacidad = (int)numCapacidad.Value, activo = original?.activo ?? true });
            DialogResult = DialogResult.OK;
        }
        catch (Exception error) { MessageBox.Show(this, error.Message, "Revise los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
