using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;
public partial class FrmInicio : Form
{
    private FrmGestionEstudiantes? estudiantes;
    private FrmArchivos? archivos;
    private FrmMatriculas? matriculas;
    private FormAsistencia? asistencia;
    private readonly EstudianteService servicio = null!;
    private readonly ArchivoService servicioArchivos = null!;
    private readonly AsistenciaService servicioAsistencia = null!;
    public FrmInicio()
    {
        InitializeComponent();
    }

    public FrmInicio(EstudianteService servicio, ArchivoService servicioArchivos, AsistenciaService servicioAsistencia)
    {
        this.servicio = servicio;
        this.servicioArchivos = servicioArchivos;
        this.servicioAsistencia = servicioAsistencia;
        InitializeComponent();
        ActualizarResumen();
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
        lblArchivo.Text = "Guardado automático en archivos TXT.\n" + servicioArchivos.Ruta;
    }

    private void OcultarPantallas()
    {
        panelInicio.Hide();
        panelModulo.Hide();
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
        OcultarPantallas();
        lblCabecera.Text = "Administración académica";
        ActualizarResumen();
        panelInicio.Show();
        panelInicio.BringToFront();
    }

    private void btnEstudiantes_Click(object? sender, EventArgs e)
    {
        OcultarPantallas();
        lblCabecera.Text = "Estudiantes";
        // Se crea una sola vez para conservar sus registros al cambiar de sección.
        if (estudiantes == null)
        {
            estudiantes = new FrmGestionEstudiantes(servicio);
            estudiantes.TopLevel = false;
            estudiantes.FormBorderStyle = FormBorderStyle.None;
            estudiantes.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(estudiantes);
        }

        estudiantes.ActualizarListado();
        estudiantes.Show();
        estudiantes.BringToFront();
    }

    private void MostrarModulo(string titulo)
    {
        OcultarPantallas();
        lblCabecera.Text = titulo;
        lblModulo.Text = titulo;
        panelModulo.Show();
        panelModulo.BringToFront();
    }

    private void btnAulas_Click(object? sender, EventArgs e)
    {
        MostrarModulo("Aulas");
    }

    private void btnMatriculas_Click(object? sender, EventArgs e)
    {
        if (matriculas == null)
        {
            matriculas = new FrmMatriculas();
            IntegrarFormulario(matriculas);
        }

        MostrarFormulario(matriculas, "Matrículas");
    }

    private void btnAsistencia_Click(object? sender, EventArgs e)
    {
        if (asistencia == null)
        {
            asistencia = new FormAsistencia(servicio, servicioAsistencia);
            IntegrarFormulario(asistencia);
        }

        asistencia.ActualizarAlMostrar();
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
        MostrarModulo("Reportes");
    }

    private void btnArchivos_Click(object? sender, EventArgs e)
    {
        OcultarPantallas();
        lblCabecera.Text = "Archivos";
        if (archivos == null)
        {
            archivos = new FrmArchivos(servicioArchivos);
            archivos.TopLevel = false;
            archivos.FormBorderStyle = FormBorderStyle.None;
            archivos.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(archivos);
        }

        archivos.ActualizarInformacion();
        archivos.Show();
        archivos.BringToFront();
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
