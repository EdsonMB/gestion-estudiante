using GestionEstudiante.model;
using GestionEstudiante.service;
using GestionEstudiante.util;

namespace GestionEstudiante;
public partial class FrmInicio : Form
{
    private FrmAulas? aulas;
    private FrmReportes? reportes;
    private FrmGestionEstudiantes? estudiantes;
    private FrmArchivos? archivos;
    private FrmMatriculas? matriculas;
    private FormAsistencia? asistencia;
    private readonly EstudianteService servicio = null!;
    private readonly ArchivoService servicioArchivos = null!;
    private readonly AulaService servicioAulas = null!;
    private readonly MatriculaService servicioMatriculas = null!;
    private readonly AsistenciaService servicioAsistencia = null!;
    public FrmInicio()
    {
        InitializeComponent();
        lblCentro.Text = Constantes.NombreCentroEducativo;
        lblAnio.Text = "AÑO ESCOLAR " + Constantes.AnioEscolar;
        lblOperador.Text = Constantes.AnioEscolar + " · Operador local";
        lblDetalle1.Text = "Año escolar " + Constantes.AnioEscolar;
    }

    public FrmInicio(EstudianteService servicio, ArchivoService servicioArchivos, AulaService servicioAulas,
        MatriculaService servicioMatriculas, AsistenciaService servicioAsistencia) : this()
    {
        this.servicio = servicio;
        this.servicioArchivos = servicioArchivos;
        this.servicioAulas = servicioAulas;
        this.servicioMatriculas = servicioMatriculas;
        this.servicioAsistencia = servicioAsistencia;
        ActualizarResumen();
        FormClosing += InicioCerrando;
    }

    private void ActualizarResumen()
    {
        int activos = 0;
        foreach (Estudiante estudiante in servicio.Listar())
        {
            if (estudiante.activo)
            {
                activos++;
            }
        }

        int matriculados = 0;
        int cuposDisponibles = 0;
        int indice = 0;
        Label[] nombres = { lblAula0, lblAula1, lblAula2 };
        Label[] cupos = { lblCupo0, lblCupo1, lblCupo2 };
        ProgressBar[] barras = { barraAula0, barraAula1, barraAula2 };
        foreach (Aula aula in servicioAulas.Listar())
        {
            if (!aula.activo || aula.anio != Constantes.AnioEscolar)
            {
                continue;
            }

            int ocupados = servicioAulas.Ocupados(aula.id);
            matriculados += ocupados;
            cuposDisponibles += servicioAulas.Disponibles(aula);
            if (indice < nombres.Length)
            {
                nombres[indice].Text = aula.Descripcion;
                cupos[indice].Text = ocupados + " / " + aula.capacidad;
                barras[indice].Value = (int)(100m * ocupados / aula.capacidad);
                indice++;
            }
        }

        for (int i = indice; i < nombres.Length; i++)
        {
            nombres[i].Text = "Sin aula";
            cupos[i].Text = "—";
            barras[i].Value = 0;
        }

        int totalAsistencias = 0;
        foreach (Asistencia registro in servicioAsistencia.Listar())
        {
            if (registro.fecha.Year == Constantes.AnioEscolar)
            {
                totalAsistencias++;
            }
        }

        lblValor0.Text = activos.ToString();
        lblValor1.Text = matriculados.ToString();
        lblValor2.Text = cuposDisponibles.ToString();
        lblValor3.Text = totalAsistencias.ToString();
        lblArchivo.Text = "Guardado automático en archivos TXT.\n" + servicioArchivos.Ruta;
    }

    private void InicioCerrando(object? sender, FormClosingEventArgs e)
    {
        if (!PuedeCambiar())
        {
            e.Cancel = true;
        }
    }

    private bool PuedeCambiar()
    {
        return asistencia == null || !asistencia.Visible || asistencia.ConfirmarSalida();
    }

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
            aulas = new FrmAulas(servicioAulas);
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
            matriculas = new FrmMatriculas(servicio, servicioAulas, servicioMatriculas);
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
            asistencia = new FormAsistencia(servicio, servicioAsistencia, servicioAulas, servicioMatriculas);
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
        if (!PuedeCambiar()) return;
        if (reportes == null)
        {
            reportes = new FrmReportes(servicio, servicioAulas, servicioMatriculas, servicioAsistencia);
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
        using FrmMatricula formulario = new FrmMatricula(servicio, servicioAulas, servicioMatriculas);
        if (formulario.ShowDialog(this) == DialogResult.OK) ActualizarResumen();
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
