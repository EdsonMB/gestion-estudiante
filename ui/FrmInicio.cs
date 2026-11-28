using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;
public partial class FrmInicio : Form
{
    private FrmAulas? aulas;
    private FrmReportes? reportes;
    private readonly EscolarService escolar = null!;
    private FrmGestionEstudiantes? estudiantes;
    private FrmArchivos? archivos;
    private FrmMatriculas? matriculas;
    private FormAsistencia? asistencia;
    private readonly EstudianteService servicio = null!;
    private readonly ArchivoService servicioArchivos = null!;
    public FrmInicio()
    {
        InitializeComponent();
    }

    public FrmInicio(EstudianteService servicio, ArchivoService servicioArchivos, EscolarService escolar)
    {
        this.escolar = escolar;
        this.servicio = servicio;
        this.servicioArchivos = servicioArchivos;
        InitializeComponent();
        ActualizarResumen();
        FormClosing += (_, e) => { if (!PuedeCambiar()) e.Cancel = true; };
    }

    private void ActualizarResumen()
    {
        lblCentro.Text = Constantes.NombreCentroEducativo;
        lblAnio.Text = "AÑO ESCOLAR " + Constantes.AnioEscolar;
        lblOperador.Text = Constantes.AnioEscolar + " · Operador local";
        int activos = 0;
        foreach (var estudiante in servicio.Listar())
            if (estudiante.activo)
            {
                activos++;
            }

        lblValor0.Text = activos.ToString();
        var actuales = escolar.Aulas.Listar().Where(x => x.activo && x.anio == Constantes.AnioEscolar).ToList();
        lblValor1.Text = actuales.Sum(x => escolar.Aulas.Ocupados(x.id)).ToString();
        lblValor2.Text = actuales.Sum(x => escolar.Aulas.Disponibles(x)).ToString();
        lblValor3.Text = escolar.Asistencias.Listar().Count(x => x.fecha.Year == Constantes.AnioEscolar).ToString();
        Label[] nombres = { lblAula0, lblAula1, lblAula2 };
        Label[] cupos = { lblCupo0, lblCupo1, lblCupo2 };
        ProgressBar[] barras = { barraAula0, barraAula1, barraAula2 };
        for (int i = 0; i < nombres.Length; i++)
        {
            var aula = actuales.ElementAtOrDefault(i);
            nombres[i].Text = aula?.Descripcion ?? "Sin aula";
            cupos[i].Text = aula == null ? "—" : escolar.Aulas.Ocupados(aula.id) + " / " + aula.capacidad;
            barras[i].Value = aula == null ? 0 : (int)(100m * escolar.Aulas.Ocupados(aula.id) / aula.capacidad);
        }
        lblArchivo.Text = "Guardado automático en archivos TXT.\n" + servicioArchivos.Ruta;
    }

    private bool PuedeCambiar() => asistencia == null || !asistencia.Visible || asistencia.ConfirmarSalida();

    private void OcultarPantallas()
    {
        aulas?.Hide();
        reportes?.Hide();
        panelInicio.Hide();
        matriculas?.Hide();
        asistencia?.Hide();
        if (estudiantes != null)
        {
            estudiantes.Hide();
        }

        if (archivos != null)
        {
            archivos.Hide();
        }
    }

    private void btnInicio_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        OcultarPantallas();
        lblCabecera.Text = "Administración académica";
        ActualizarResumen();
        panelInicio.Show();
        panelInicio.BringToFront();
    }

    private void btnEstudiantes_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        // Se crea una sola vez para conservar sus registros al cambiar de sección.
        if (estudiantes == null)
        {
            estudiantes = new FrmGestionEstudiantes(servicio);
            IntegrarFormulario(estudiantes);
        }

        estudiantes.ActualizarListado();
        MostrarFormulario(estudiantes, "Estudiantes");
    }

    private void btnAulas_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        if (aulas == null)
        {
            aulas = new FrmAulas(escolar);
            IntegrarFormulario(aulas);
        }

        aulas.ActualizarListado();
        MostrarFormulario(aulas, "Aulas");
    }

    private void btnMatriculas_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        if (matriculas == null)
        {
            matriculas = new FrmMatriculas(escolar);
            IntegrarFormulario(matriculas);
        }

        matriculas.ActualizarListado();
        MostrarFormulario(matriculas, "Matrículas");
    }

    private void btnAsistencia_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        if (asistencia == null)
        {
            asistencia = new FormAsistencia(escolar);
            IntegrarFormulario(asistencia);
        }

        asistencia.ActualizarListado();
        MostrarFormulario(asistencia, "Asistencia");
    }

    private void IntegrarFormulario(Form formulario)
    {
        formulario.TopLevel = false;
        formulario.FormBorderStyle = FormBorderStyle.None;
        formulario.Dock = DockStyle.Fill;
        panelContenido.Controls.Add(formulario);
    }

    private void MostrarFormulario(Form formulario, string titulo)
    {
        OcultarPantallas();
        lblCabecera.Text = titulo;
        formulario.Show();
        formulario.BringToFront();
    }

    private void btnReportes_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        if (reportes == null)
        {
            reportes = new FrmReportes(escolar);
            IntegrarFormulario(reportes);
        }

        reportes.ActualizarListado();
        MostrarFormulario(reportes, "Reportes");
    }

    private void btnArchivos_Click(object? sender, EventArgs e)
    {
        if (!PuedeCambiar()) return;
        if (archivos == null)
        {
            archivos = new FrmArchivos(servicioArchivos);
            IntegrarFormulario(archivos);
        }

        archivos.ActualizarInformacion();
        MostrarFormulario(archivos, "Archivos");
    }

    private void btnRegistrarAcceso_Click(object? sender, EventArgs e)
    {
        using FrmEstudiante formulario = new FrmEstudiante(servicio, null);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarResumen();
    }

    private void btnNuevaMatriculaAcceso_Click(object? sender, EventArgs e)
    {
        using FrmMatricula formulario = new FrmMatricula(escolar);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarResumen();
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
