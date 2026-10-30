namespace GestionEstudiante;

partial class FrmInicioVisual
{
    private System.ComponentModel.IContainer? components = null;
    private Panel panelMenu = null!;
    private Label lblMarca = null!;
    private Label lblCentro = null!;
    private Label lblAnio = null!;
    private Button btnInicio = null!;
    private Button btnEstudiantes = null!;
    private Button btnAulas = null!;
    private Button btnMatriculas = null!;
    private Button btnAsistencia = null!;
    private Button btnReportes = null!;
    private Button btnArchivos = null!;
    private Button btnSalir = null!;
    private Panel panelCabecera = null!;
    private Label lblCabecera = null!;
    private Label lblOperador = null!;
    private Panel panelContenido = null!;
    private Panel panelInicio = null!;
    private Label lblResumen = null!;
    private Label lblDescripcion = null!;
    private Button btnNuevaMatricula = null!;
    private GroupBox grupoIndicador0 = null!;
    private Label lblValor0 = null!;
    private Label lblDetalle0 = null!;
    private GroupBox grupoIndicador1 = null!;
    private Label lblValor1 = null!;
    private Label lblDetalle1 = null!;
    private GroupBox grupoIndicador2 = null!;
    private Label lblValor2 = null!;
    private Label lblDetalle2 = null!;
    private GroupBox grupoIndicador3 = null!;
    private Label lblValor3 = null!;
    private Label lblDetalle3 = null!;
    private GroupBox grupoOcupacion = null!;
    private Label lblAula0 = null!;
    private ProgressBar barraAula0 = null!;
    private Label lblCupo0 = null!;
    private Label lblAula1 = null!;
    private ProgressBar barraAula1 = null!;
    private Label lblCupo1 = null!;
    private Label lblAula2 = null!;
    private ProgressBar barraAula2 = null!;
    private Label lblCupo2 = null!;
    private GroupBox grupoAcciones = null!;
    private Label lblAcciones = null!;
    private Button btnRegistrar = null!;
    private Button btnTomarAsistencia = null!;
    private Button btnConsultarReportes = null!;
    private GroupBox grupoArchivo = null!;
    private Label lblArchivo = null!;
    private Label lblPie = null!;
    private Panel panelModulo = null!;
    private Label lblModulo = null!;
    private Label lblPendiente = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelMenu = new Panel();
        lblMarca = new Label();
        lblCentro = new Label();
        lblAnio = new Label();
        btnInicio = new Button();
        btnEstudiantes = new Button();
        btnAulas = new Button();
        btnMatriculas = new Button();
        btnAsistencia = new Button();
        btnReportes = new Button();
        btnArchivos = new Button();
        btnSalir = new Button();
        panelCabecera = new Panel();
        lblCabecera = new Label();
        lblOperador = new Label();
        panelContenido = new Panel();
        panelInicio = new Panel();
        lblResumen = new Label();
        lblDescripcion = new Label();
        btnNuevaMatricula = new Button();
        grupoIndicador0 = new GroupBox();
        lblValor0 = new Label();
        lblDetalle0 = new Label();
        grupoIndicador1 = new GroupBox();
        lblValor1 = new Label();
        lblDetalle1 = new Label();
        grupoIndicador2 = new GroupBox();
        lblValor2 = new Label();
        lblDetalle2 = new Label();
        grupoIndicador3 = new GroupBox();
        lblValor3 = new Label();
        lblDetalle3 = new Label();
        grupoOcupacion = new GroupBox();
        lblAula0 = new Label();
        barraAula0 = new ProgressBar();
        lblCupo0 = new Label();
        lblAula1 = new Label();
        barraAula1 = new ProgressBar();
        lblCupo1 = new Label();
        lblAula2 = new Label();
        barraAula2 = new ProgressBar();
        lblCupo2 = new Label();
        grupoAcciones = new GroupBox();
        lblAcciones = new Label();
        btnRegistrar = new Button();
        btnTomarAsistencia = new Button();
        btnConsultarReportes = new Button();
        grupoArchivo = new GroupBox();
        lblArchivo = new Label();
        lblPie = new Label();
        panelModulo = new Panel();
        lblModulo = new Label();
        lblPendiente = new Label();
        SuspendLayout();

        // panelMenu
        panelMenu.Name = "panelMenu";
        panelMenu.Location = new Point(0, 0);
        panelMenu.Size = new Size(220, 800);
        panelMenu.BackColor = Color.White;
        panelMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        panelMenu.BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(panelMenu);

        // lblMarca
        lblMarca.Name = "lblMarca";
        lblMarca.Location = new Point(18, 24);
        lblMarca.Size = new Size(184, 30);
        lblMarca.Text = "Gestión Escolar";
        lblMarca.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        panelMenu.Controls.Add(lblMarca);

        // lblCentro
        lblCentro.Name = "lblCentro";
        lblCentro.Location = new Point(18, 62);
        lblCentro.Size = new Size(184, 25);
        lblCentro.Text = "Centro educativo";
        lblCentro.ForeColor = Color.DimGray;
        panelMenu.Controls.Add(lblCentro);

        // lblAnio
        lblAnio.Name = "lblAnio";
        lblAnio.Location = new Point(18, 98);
        lblAnio.Size = new Size(184, 25);
        lblAnio.Text = "AÑO ESCOLAR 2026";
        lblAnio.ForeColor = Color.DimGray;
        panelMenu.Controls.Add(lblAnio);

        // btnInicio
        btnInicio.Name = "btnInicio";
        btnInicio.Location = new Point(18, 150);
        btnInicio.Size = new Size(182, 40);
        btnInicio.Text = "Inicio";
        btnInicio.TabIndex = 0;
        panelMenu.Controls.Add(btnInicio);

        // btnEstudiantes
        btnEstudiantes.Name = "btnEstudiantes";
        btnEstudiantes.Location = new Point(18, 201);
        btnEstudiantes.Size = new Size(182, 40);
        btnEstudiantes.Text = "Estudiantes";
        btnEstudiantes.TabIndex = 1;
        panelMenu.Controls.Add(btnEstudiantes);

        // btnAulas
        btnAulas.Name = "btnAulas";
        btnAulas.Location = new Point(18, 252);
        btnAulas.Size = new Size(182, 40);
        btnAulas.Text = "Aulas";
        btnAulas.TabIndex = 2;
        panelMenu.Controls.Add(btnAulas);

        // btnMatriculas
        btnMatriculas.Name = "btnMatriculas";
        btnMatriculas.Location = new Point(18, 303);
        btnMatriculas.Size = new Size(182, 40);
        btnMatriculas.Text = "Matrículas";
        btnMatriculas.TabIndex = 3;
        panelMenu.Controls.Add(btnMatriculas);

        // btnAsistencia
        btnAsistencia.Name = "btnAsistencia";
        btnAsistencia.Location = new Point(18, 354);
        btnAsistencia.Size = new Size(182, 40);
        btnAsistencia.Text = "Asistencia";
        btnAsistencia.TabIndex = 4;
        panelMenu.Controls.Add(btnAsistencia);

        // btnReportes
        btnReportes.Name = "btnReportes";
        btnReportes.Location = new Point(18, 405);
        btnReportes.Size = new Size(182, 40);
        btnReportes.Text = "Reportes";
        btnReportes.TabIndex = 5;
        panelMenu.Controls.Add(btnReportes);

        // btnArchivos
        btnArchivos.Name = "btnArchivos";
        btnArchivos.Location = new Point(18, 456);
        btnArchivos.Size = new Size(182, 40);
        btnArchivos.Text = "Archivos y respaldo";
        btnArchivos.TabIndex = 6;
        panelMenu.Controls.Add(btnArchivos);

        // btnSalir
        btnSalir.Name = "btnSalir";
        btnSalir.Location = new Point(18, 727);
        btnSalir.Size = new Size(182, 38);
        btnSalir.Text = "Cerrar sesión";
        btnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSalir.TabIndex = 7;
        panelMenu.Controls.Add(btnSalir);

        // panelCabecera
        panelCabecera.Name = "panelCabecera";
        panelCabecera.Location = new Point(220, 0);
        panelCabecera.Size = new Size(1100, 72);
        panelCabecera.BackColor = Color.White;
        panelCabecera.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        Controls.Add(panelCabecera);

        // lblCabecera
        lblCabecera.Name = "lblCabecera";
        lblCabecera.Location = new Point(24, 24);
        lblCabecera.Size = new Size(500, 26);
        lblCabecera.Text = "Administración académica";
        lblCabecera.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        panelCabecera.Controls.Add(lblCabecera);

        // lblOperador
        lblOperador.Name = "lblOperador";
        lblOperador.Location = new Point(850, 26);
        lblOperador.Size = new Size(225, 24);
        lblOperador.Text = "2026  ·  Operador local";
        lblOperador.TextAlign = ContentAlignment.MiddleRight;
        lblOperador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        panelCabecera.Controls.Add(lblOperador);

        // panelContenido
        panelContenido.Name = "panelContenido";
        panelContenido.Location = new Point(220, 72);
        panelContenido.Size = new Size(1100, 728);
        panelContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelContenido.AutoScroll = true;
        Controls.Add(panelContenido);

        // panelInicio
        panelInicio.Name = "panelInicio";
        panelInicio.Location = new Point(0, 0);
        panelInicio.Size = new Size(1100, 728);
        panelInicio.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelInicio.AutoScroll = true;
        panelContenido.Controls.Add(panelInicio);

        // lblResumen
        lblResumen.Name = "lblResumen";
        lblResumen.Location = new Point(24, 25);
        lblResumen.Size = new Size(740, 35);
        lblResumen.Text = "Resumen del centro educativo";
        lblResumen.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        panelInicio.Controls.Add(lblResumen);

        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Location = new Point(24, 66);
        lblDescripcion.Size = new Size(740, 24);
        lblDescripcion.Text = "Consulta el estado general y accede a las funciones del sistema.";
        panelInicio.Controls.Add(lblDescripcion);

        // btnNuevaMatricula
        btnNuevaMatricula.Name = "btnNuevaMatricula";
        btnNuevaMatricula.Location = new Point(900, 28);
        btnNuevaMatricula.Size = new Size(175, 37);
        btnNuevaMatricula.Text = "Nueva matrícula";
        panelInicio.Controls.Add(btnNuevaMatricula);

        // grupoIndicador0
        grupoIndicador0.Name = "grupoIndicador0";
        grupoIndicador0.Location = new Point(24, 111);
        grupoIndicador0.Size = new Size(250, 126);
        grupoIndicador0.Text = "Estudiantes activos";
        grupoIndicador0.BackColor = Color.White;
        panelInicio.Controls.Add(grupoIndicador0);

        // lblValor0
        lblValor0.Name = "lblValor0";
        lblValor0.Location = new Point(16, 32);
        lblValor0.Size = new Size(215, 38);
        lblValor0.Text = "—";
        lblValor0.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        grupoIndicador0.Controls.Add(lblValor0);

        // lblDetalle0
        lblDetalle0.Name = "lblDetalle0";
        lblDetalle0.Location = new Point(16, 83);
        lblDetalle0.Size = new Size(215, 25);
        lblDetalle0.Text = "Registro de estudiantes";
        lblDetalle0.ForeColor = Color.DimGray;
        grupoIndicador0.Controls.Add(lblDetalle0);

        // grupoIndicador1
        grupoIndicador1.Name = "grupoIndicador1";
        grupoIndicador1.Location = new Point(291, 111);
        grupoIndicador1.Size = new Size(250, 126);
        grupoIndicador1.Text = "Matrículas activas";
        grupoIndicador1.BackColor = Color.White;
        panelInicio.Controls.Add(grupoIndicador1);

        // lblValor1
        lblValor1.Name = "lblValor1";
        lblValor1.Location = new Point(16, 32);
        lblValor1.Size = new Size(215, 38);
        lblValor1.Text = "—";
        lblValor1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        grupoIndicador1.Controls.Add(lblValor1);

        // lblDetalle1
        lblDetalle1.Name = "lblDetalle1";
        lblDetalle1.Location = new Point(16, 83);
        lblDetalle1.Size = new Size(215, 25);
        lblDetalle1.Text = "Año escolar 2026";
        lblDetalle1.ForeColor = Color.DimGray;
        grupoIndicador1.Controls.Add(lblDetalle1);

        // grupoIndicador2
        grupoIndicador2.Name = "grupoIndicador2";
        grupoIndicador2.Location = new Point(558, 111);
        grupoIndicador2.Size = new Size(250, 126);
        grupoIndicador2.Text = "Cupos disponibles";
        grupoIndicador2.BackColor = Color.White;
        panelInicio.Controls.Add(grupoIndicador2);

        // lblValor2
        lblValor2.Name = "lblValor2";
        lblValor2.Location = new Point(16, 32);
        lblValor2.Size = new Size(215, 38);
        lblValor2.Text = "—";
        lblValor2.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        grupoIndicador2.Controls.Add(lblValor2);

        // lblDetalle2
        lblDetalle2.Name = "lblDetalle2";
        lblDetalle2.Location = new Point(16, 83);
        lblDetalle2.Size = new Size(215, 25);
        lblDetalle2.Text = "Distribución por aulas";
        lblDetalle2.ForeColor = Color.DimGray;
        grupoIndicador2.Controls.Add(lblDetalle2);

        // grupoIndicador3
        grupoIndicador3.Name = "grupoIndicador3";
        grupoIndicador3.Location = new Point(825, 111);
        grupoIndicador3.Size = new Size(250, 126);
        grupoIndicador3.Text = "Registros de asistencia";
        grupoIndicador3.BackColor = Color.White;
        panelInicio.Controls.Add(grupoIndicador3);

        // lblValor3
        lblValor3.Name = "lblValor3";
        lblValor3.Location = new Point(16, 32);
        lblValor3.Size = new Size(215, 38);
        lblValor3.Text = "—";
        lblValor3.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        grupoIndicador3.Controls.Add(lblValor3);

        // lblDetalle3
        lblDetalle3.Name = "lblDetalle3";
        lblDetalle3.Location = new Point(16, 83);
        lblDetalle3.Size = new Size(215, 25);
        lblDetalle3.Text = "Presente, tardanza o falta";
        lblDetalle3.ForeColor = Color.DimGray;
        grupoIndicador3.Controls.Add(lblDetalle3);

        // grupoOcupacion
        grupoOcupacion.Name = "grupoOcupacion";
        grupoOcupacion.Location = new Point(24, 260);
        grupoOcupacion.Size = new Size(650, 202);
        grupoOcupacion.Text = "Ocupación por aula";
        grupoOcupacion.BackColor = Color.White;
        panelInicio.Controls.Add(grupoOcupacion);

        // lblAula0
        lblAula0.Name = "lblAula0";
        lblAula0.Location = new Point(18, 38);
        lblAula0.Size = new Size(170, 24);
        lblAula0.Text = "Primaria · 1.º A";
        grupoOcupacion.Controls.Add(lblAula0);

        // barraAula0
        barraAula0.Name = "barraAula0";
        barraAula0.Location = new Point(200, 38);
        barraAula0.Size = new Size(335, 22);
        barraAula0.Value = 0;
        grupoOcupacion.Controls.Add(barraAula0);

        // lblCupo0
        lblCupo0.Name = "lblCupo0";
        lblCupo0.Location = new Point(550, 38);
        lblCupo0.Size = new Size(80, 24);
        lblCupo0.Text = "Sin datos";
        grupoOcupacion.Controls.Add(lblCupo0);

        // lblAula1
        lblAula1.Name = "lblAula1";
        lblAula1.Location = new Point(18, 86);
        lblAula1.Size = new Size(170, 24);
        lblAula1.Text = "Primaria · 1.º B";
        grupoOcupacion.Controls.Add(lblAula1);

        // barraAula1
        barraAula1.Name = "barraAula1";
        barraAula1.Location = new Point(200, 86);
        barraAula1.Size = new Size(335, 22);
        barraAula1.Value = 0;
        grupoOcupacion.Controls.Add(barraAula1);

        // lblCupo1
        lblCupo1.Name = "lblCupo1";
        lblCupo1.Location = new Point(550, 86);
        lblCupo1.Size = new Size(80, 24);
        lblCupo1.Text = "Sin datos";
        grupoOcupacion.Controls.Add(lblCupo1);

        // lblAula2
        lblAula2.Name = "lblAula2";
        lblAula2.Location = new Point(18, 134);
        lblAula2.Size = new Size(170, 24);
        lblAula2.Text = "Secundaria · 1.º A";
        grupoOcupacion.Controls.Add(lblAula2);

        // barraAula2
        barraAula2.Name = "barraAula2";
        barraAula2.Location = new Point(200, 134);
        barraAula2.Size = new Size(335, 22);
        barraAula2.Value = 0;
        grupoOcupacion.Controls.Add(barraAula2);

        // lblCupo2
        lblCupo2.Name = "lblCupo2";
        lblCupo2.Location = new Point(550, 134);
        lblCupo2.Size = new Size(80, 24);
        lblCupo2.Text = "Sin datos";
        grupoOcupacion.Controls.Add(lblCupo2);

        // grupoAcciones
        grupoAcciones.Name = "grupoAcciones";
        grupoAcciones.Location = new Point(698, 260);
        grupoAcciones.Size = new Size(377, 248);
        grupoAcciones.Text = "Acciones frecuentes";
        grupoAcciones.BackColor = Color.White;
        panelInicio.Controls.Add(grupoAcciones);

        // lblAcciones
        lblAcciones.Name = "lblAcciones";
        lblAcciones.Location = new Point(18, 28);
        lblAcciones.Size = new Size(340, 42);
        lblAcciones.Text = "Continúa con el registro y seguimiento de tus estudiantes.";
        grupoAcciones.Controls.Add(lblAcciones);

        // btnRegistrar
        btnRegistrar.Name = "btnRegistrar";
        btnRegistrar.Location = new Point(18, 85);
        btnRegistrar.Size = new Size(340, 38);
        btnRegistrar.Text = "Registrar estudiante";
        grupoAcciones.Controls.Add(btnRegistrar);

        // btnTomarAsistencia
        btnTomarAsistencia.Name = "btnTomarAsistencia";
        btnTomarAsistencia.Location = new Point(18, 134);
        btnTomarAsistencia.Size = new Size(340, 38);
        btnTomarAsistencia.Text = "Tomar asistencia";
        grupoAcciones.Controls.Add(btnTomarAsistencia);

        // btnConsultarReportes
        btnConsultarReportes.Name = "btnConsultarReportes";
        btnConsultarReportes.Location = new Point(18, 183);
        btnConsultarReportes.Size = new Size(340, 38);
        btnConsultarReportes.Text = "Consultar reportes";
        grupoAcciones.Controls.Add(btnConsultarReportes);

        // grupoArchivo
        grupoArchivo.Name = "grupoArchivo";
        grupoArchivo.Location = new Point(24, 532);
        grupoArchivo.Size = new Size(1051, 100);
        grupoArchivo.Text = "Información del archivo";
        grupoArchivo.BackColor = Color.White;
        panelInicio.Controls.Add(grupoArchivo);

        // lblArchivo
        lblArchivo.Name = "lblArchivo";
        lblArchivo.Location = new Point(18, 30);
        lblArchivo.Size = new Size(1010, 50);
        lblArchivo.Text = "Indicadores y ocupación pendientes de conectar.\nAbre Estudiantes para usar el registro actual; los demás módulos tienen un espacio reservado.";
        grupoArchivo.Controls.Add(lblArchivo);

        // lblPie
        lblPie.Name = "lblPie";
        lblPie.Location = new Point(24, 660);
        lblPie.Size = new Size(1040, 26);
        lblPie.Text = "Gestión Escolar / Registro, matrícula y asistencia";
        panelInicio.Controls.Add(lblPie);

        // panelModulo
        panelModulo.Name = "panelModulo";
        panelModulo.Location = new Point(0, 0);
        panelModulo.Size = new Size(1100, 728);
        panelModulo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        panelModulo.Visible = false;
        panelContenido.Controls.Add(panelModulo);

        // lblModulo
        lblModulo.Name = "lblModulo";
        lblModulo.Location = new Point(24, 28);
        lblModulo.Size = new Size(1000, 40);
        lblModulo.Text = "Módulo";
        lblModulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        panelModulo.Controls.Add(lblModulo);

        // lblPendiente
        lblPendiente.Name = "lblPendiente";
        lblPendiente.Location = new Point(24, 82);
        lblPendiente.Size = new Size(1000, 70);
        lblPendiente.Text = "Espacio reservado para desarrollar este módulo.";
        panelModulo.Controls.Add(lblPendiente);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 9F);
        ClientSize = new Size(1320, 800);
        MinimumSize = new Size(1336, 839);
        Name = "FrmInicioVisual";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Gestión Escolar - Inicio";
        BackColor = SystemColors.Control;
        btnInicio.Click += btnInicio_Click;
        btnEstudiantes.Click += btnEstudiantes_Click;
        btnAulas.Click += btnAulas_Click;
        btnMatriculas.Click += btnMatriculas_Click;
        btnAsistencia.Click += btnAsistencia_Click;
        btnReportes.Click += btnReportes_Click;
        btnArchivos.Click += btnArchivos_Click;
        btnSalir.Click += btnSalir_Click;
        btnRegistrar.Click += btnEstudiantes_Click;
        btnNuevaMatricula.Click += btnMatriculas_Click;
        btnTomarAsistencia.Click += btnAsistencia_Click;
        btnConsultarReportes.Click += btnReportes_Click;
        ResumeLayout(false);
    }
}
