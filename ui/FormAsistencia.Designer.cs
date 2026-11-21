namespace GestionEscolar
{
    partial class FormAsistencia
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDatosDemo = new System.Windows.Forms.Label();
            this.lblTrabajoLocal = new System.Windows.Forms.Label();
            this.lblDatosGuardados = new Guna.UI2.WinForms.Guna2Button();
            this.btnArchivos = new Guna.UI2.WinForms.Guna2Button();
            this.btnReportes = new Guna.UI2.WinForms.Guna2Button();
            this.btnAsistencia = new Guna.UI2.WinForms.Guna2Button();
            this.btnMatriculas = new Guna.UI2.WinForms.Guna2Button();
            this.btnAulas = new Guna.UI2.WinForms.Guna2Button();
            this.btnEstudiantes = new Guna.UI2.WinForms.Guna2Button();
            this.btnInicio = new Guna.UI2.WinForms.Guna2Button();
            this.lblAnioEscolar = new System.Windows.Forms.Label();
            this.lblSubtituloApp = new System.Windows.Forms.Label();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.pnlLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblOperador = new System.Windows.Forms.Label();
            this.lblBadgeAnio = new Guna.UI2.WinForms.Guna2Button();
            this.lblAdminAcademica = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.btnMatrizSemanal = new System.Windows.Forms.Button();
            this.badgeTardanzas = new System.Windows.Forms.Label();
            this.badgeFaltas = new System.Windows.Forms.Label();
            this.badgePresentes = new System.Windows.Forms.Label();
            this.pnlCardTabla = new System.Windows.Forms.Panel();
            this.lblNotaTabla = new System.Windows.Forms.Label();
            this.dgvAsistencia = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstudiante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAcciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTituloTabla = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.cmbAula = new System.Windows.Forms.ComboBox();
            this.lblAula = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblSubtituloSeccion = new System.Windows.Forms.Label();
            this.lblTituloSeccion = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCardTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistencia)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.pnlSidebar.BorderThickness = 1;
            this.pnlSidebar.Controls.Add(this.lblDatosDemo);
            this.pnlSidebar.Controls.Add(this.lblTrabajoLocal);
            this.pnlSidebar.Controls.Add(this.lblDatosGuardados);
            this.pnlSidebar.Controls.Add(this.btnArchivos);
            this.pnlSidebar.Controls.Add(this.btnReportes);
            this.pnlSidebar.Controls.Add(this.btnAsistencia);
            this.pnlSidebar.Controls.Add(this.btnMatriculas);
            this.pnlSidebar.Controls.Add(this.btnAulas);
            this.pnlSidebar.Controls.Add(this.btnEstudiantes);
            this.pnlSidebar.Controls.Add(this.btnInicio);
            this.pnlSidebar.Controls.Add(this.lblAnioEscolar);
            this.pnlSidebar.Controls.Add(this.lblSubtituloApp);
            this.pnlSidebar.Controls.Add(this.lblTituloApp);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.FillColor = System.Drawing.Color.White;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(293, 887);
            this.pnlSidebar.TabIndex = 0;
            // 
            // lblDatosDemo
            // 
            this.lblDatosDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDatosDemo.AutoSize = true;
            this.lblDatosDemo.BackColor = System.Drawing.Color.White;
            this.lblDatosDemo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDatosDemo.ForeColor = System.Drawing.Color.Gray;
            this.lblDatosDemo.Location = new System.Drawing.Point(21, 844);
            this.lblDatosDemo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatosDemo.Name = "lblDatosDemo";
            this.lblDatosDemo.Size = new System.Drawing.Size(151, 19);
            this.lblDatosDemo.TabIndex = 13;
            this.lblDatosDemo.Text = "Datos de demostración";
            // 
            // lblTrabajoLocal
            // 
            this.lblTrabajoLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrabajoLocal.AutoSize = true;
            this.lblTrabajoLocal.BackColor = System.Drawing.Color.White;
            this.lblTrabajoLocal.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblTrabajoLocal.ForeColor = System.Drawing.Color.Gray;
            this.lblTrabajoLocal.Location = new System.Drawing.Point(21, 822);
            this.lblTrabajoLocal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrabajoLocal.Name = "lblTrabajoLocal";
            this.lblTrabajoLocal.Size = new System.Drawing.Size(84, 19);
            this.lblTrabajoLocal.TabIndex = 12;
            this.lblTrabajoLocal.Text = "Trabajo local";
            // 
            // lblDatosGuardados
            // 
            this.lblDatosGuardados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDatosGuardados.BorderRadius = 8;
            this.lblDatosGuardados.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.lblDatosGuardados.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.lblDatosGuardados.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lblDatosGuardados.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.lblDatosGuardados.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.lblDatosGuardados.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDatosGuardados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblDatosGuardados.Location = new System.Drawing.Point(21, 778);
            this.lblDatosGuardados.Margin = new System.Windows.Forms.Padding(4);
            this.lblDatosGuardados.Name = "lblDatosGuardados";
            this.lblDatosGuardados.Size = new System.Drawing.Size(165, 34);
            this.lblDatosGuardados.TabIndex = 11;
            this.lblDatosGuardados.Text = "Datos guardados";
            // 
            // btnArchivos
            // 
            this.btnArchivos.BorderRadius = 8;
            this.btnArchivos.FillColor = System.Drawing.Color.White;
            this.btnArchivos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnArchivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnArchivos.Location = new System.Drawing.Point(24, 434);
            this.btnArchivos.Margin = new System.Windows.Forms.Padding(4);
            this.btnArchivos.Name = "btnArchivos";
            this.btnArchivos.Size = new System.Drawing.Size(259, 44);
            this.btnArchivos.TabIndex = 10;
            this.btnArchivos.Text = "Archivos y respaldo";
            // 
            // btnReportes
            // 
            this.btnReportes.BorderRadius = 8;
            this.btnReportes.FillColor = System.Drawing.Color.White;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnReportes.Location = new System.Drawing.Point(16, 382);
            this.btnReportes.Margin = new System.Windows.Forms.Padding(4);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(259, 44);
            this.btnReportes.TabIndex = 9;
            this.btnReportes.Text = "Reportes";
            // 
            // btnAsistencia
            // 
            this.btnAsistencia.BorderRadius = 8;
            this.btnAsistencia.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245)))));
            this.btnAsistencia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAsistencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.btnAsistencia.Location = new System.Drawing.Point(16, 330);
            this.btnAsistencia.Margin = new System.Windows.Forms.Padding(4);
            this.btnAsistencia.Name = "btnAsistencia";
            this.btnAsistencia.Size = new System.Drawing.Size(259, 44);
            this.btnAsistencia.TabIndex = 8;
            this.btnAsistencia.Text = "Asistencia";
            // 
            // btnMatriculas
            // 
            this.btnMatriculas.BorderRadius = 8;
            this.btnMatriculas.FillColor = System.Drawing.Color.White;
            this.btnMatriculas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMatriculas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnMatriculas.Location = new System.Drawing.Point(16, 278);
            this.btnMatriculas.Margin = new System.Windows.Forms.Padding(4);
            this.btnMatriculas.Name = "btnMatriculas";
            this.btnMatriculas.Size = new System.Drawing.Size(259, 44);
            this.btnMatriculas.TabIndex = 7;
            this.btnMatriculas.Text = "Matrículas";
            this.btnMatriculas.Click += new System.EventHandler(this.btnMatriculas_Click);
            // 
            // btnAulas
            // 
            this.btnAulas.BorderRadius = 8;
            this.btnAulas.FillColor = System.Drawing.Color.White;
            this.btnAulas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnAulas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnAulas.Location = new System.Drawing.Point(16, 226);
            this.btnAulas.Margin = new System.Windows.Forms.Padding(4);
            this.btnAulas.Name = "btnAulas";
            this.btnAulas.Size = new System.Drawing.Size(259, 44);
            this.btnAulas.TabIndex = 6;
            this.btnAulas.Text = "Aulas";
            // 
            // btnEstudiantes
            // 
            this.btnEstudiantes.BorderRadius = 8;
            this.btnEstudiantes.FillColor = System.Drawing.Color.White;
            this.btnEstudiantes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnEstudiantes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnEstudiantes.Location = new System.Drawing.Point(16, 175);
            this.btnEstudiantes.Margin = new System.Windows.Forms.Padding(4);
            this.btnEstudiantes.Name = "btnEstudiantes";
            this.btnEstudiantes.Size = new System.Drawing.Size(259, 44);
            this.btnEstudiantes.TabIndex = 5;
            this.btnEstudiantes.Text = "Estudiantes";
            // 
            // btnInicio
            // 
            this.btnInicio.BorderRadius = 8;
            this.btnInicio.FillColor = System.Drawing.Color.White;
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnInicio.Location = new System.Drawing.Point(16, 123);
            this.btnInicio.Margin = new System.Windows.Forms.Padding(4);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(259, 44);
            this.btnInicio.TabIndex = 4;
            this.btnInicio.Text = "Inicio";
            // 
            // lblAnioEscolar
            // 
            this.lblAnioEscolar.AutoSize = true;
            this.lblAnioEscolar.BackColor = System.Drawing.Color.White;
            this.lblAnioEscolar.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblAnioEscolar.ForeColor = System.Drawing.Color.Gray;
            this.lblAnioEscolar.Location = new System.Drawing.Point(21, 84);
            this.lblAnioEscolar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnioEscolar.Name = "lblAnioEscolar";
            this.lblAnioEscolar.Size = new System.Drawing.Size(129, 17);
            this.lblAnioEscolar.TabIndex = 3;
            this.lblAnioEscolar.Text = "AÑO ESCOLAR 2026";
            // 
            // lblSubtituloApp
            // 
            this.lblSubtituloApp.AutoSize = true;
            this.lblSubtituloApp.BackColor = System.Drawing.Color.White;
            this.lblSubtituloApp.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblSubtituloApp.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtituloApp.Location = new System.Drawing.Point(83, 43);
            this.lblSubtituloApp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtituloApp.Name = "lblSubtituloApp";
            this.lblSubtituloApp.Size = new System.Drawing.Size(114, 19);
            this.lblSubtituloApp.TabIndex = 2;
            this.lblSubtituloApp.Text = "Centro educativo";
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.BackColor = System.Drawing.Color.White;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.lblTituloApp.Location = new System.Drawing.Point(83, 20);
            this.lblTituloApp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(130, 23);
            this.lblTituloApp.TabIndex = 1;
            this.lblTituloApp.Text = "Gestión Escolar";
            // 
            // pnlLogo
            // 
            this.pnlLogo.BorderRadius = 8;
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.pnlLogo.Location = new System.Drawing.Point(21, 20);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(51, 47);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(51, 47);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "GE";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.pnlHeader.BorderThickness = 1;
            this.pnlHeader.Controls.Add(this.lblOperador);
            this.pnlHeader.Controls.Add(this.lblBadgeAnio);
            this.pnlHeader.Controls.Add(this.lblAdminAcademica);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(293, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1392, 64);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblOperador
            // 
            this.lblOperador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperador.AutoSize = true;
            this.lblOperador.BackColor = System.Drawing.Color.White;
            this.lblOperador.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblOperador.ForeColor = System.Drawing.Color.Gray;
            this.lblOperador.Location = new System.Drawing.Point(1260, 22);
            this.lblOperador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOperador.Name = "lblOperador";
            this.lblOperador.Size = new System.Drawing.Size(109, 20);
            this.lblOperador.TabIndex = 2;
            this.lblOperador.Text = "Operador local";
            // 
            // lblBadgeAnio
            // 
            this.lblBadgeAnio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBadgeAnio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(231)))));
            this.lblBadgeAnio.BorderRadius = 8;
            this.lblBadgeAnio.BorderThickness = 1;
            this.lblBadgeAnio.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblBadgeAnio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBadgeAnio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.lblBadgeAnio.Location = new System.Drawing.Point(1173, 15);
            this.lblBadgeAnio.Margin = new System.Windows.Forms.Padding(4);
            this.lblBadgeAnio.Name = "lblBadgeAnio";
            this.lblBadgeAnio.Size = new System.Drawing.Size(73, 34);
            this.lblBadgeAnio.TabIndex = 1;
            this.lblBadgeAnio.Text = "2026";
            // 
            // lblAdminAcademica
            // 
            this.lblAdminAcademica.AutoSize = true;
            this.lblAdminAcademica.BackColor = System.Drawing.Color.White;
            this.lblAdminAcademica.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAdminAcademica.Location = new System.Drawing.Point(32, 21);
            this.lblAdminAcademica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdminAcademica.Name = "lblAdminAcademica";
            this.lblAdminAcademica.Size = new System.Drawing.Size(213, 21);
            this.lblAdminAcademica.TabIndex = 0;
            this.lblAdminAcademica.Text = "Administración académica";
            // 
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(293, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1392, 823);
            this.pnlMain.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblTituloSeccion, this.lblSubtituloSeccion, this.lblAula, this.cmbAula,
                this.lblFecha, this.dtpFecha, this.btnGuardar, this.pnlCardTabla,
                this.badgePresentes, this.badgeFaltas, this.badgeTardanzas,
                this.btnMatrizSemanal, this.lblFooter
            });

            // Encabezado y filtros
            this.lblTituloSeccion.AutoSize = true;
            this.lblTituloSeccion.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTituloSeccion.Location = new System.Drawing.Point(28, 24);
            this.lblTituloSeccion.Name = "lblTituloSeccion";
            this.lblTituloSeccion.Text = "Asistencia";
            this.lblSubtituloSeccion.AutoSize = true;
            this.lblSubtituloSeccion.Location = new System.Drawing.Point(28, 72);
            this.lblSubtituloSeccion.Name = "lblSubtituloSeccion";
            this.lblSubtituloSeccion.Text = "Registra y corrige la asistencia de cada estudiante por fecha.";

            this.lblAula.AutoSize = true;
            this.lblAula.Location = new System.Drawing.Point(28, 101);
            this.lblAula.Name = "lblAula";
            this.lblAula.Text = "Aula";
            this.cmbAula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAula.Location = new System.Drawing.Point(28, 126);
            this.cmbAula.Name = "cmbAula";
            this.cmbAula.Size = new System.Drawing.Size(420, 32);
            this.cmbAula.TabIndex = 0;

            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(462, 101);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Text = "Fecha";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(462, 126);
            this.dtpFecha.MinDate = new System.DateTime(1753, 1, 1);
            this.dtpFecha.MaxDate = new System.DateTime(9998, 12, 31);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(210, 32);
            this.dtpFecha.TabIndex = 1;
            this.dtpFecha.Value = new System.DateTime(2026, 10, 5);
            this.dtpFecha.ValueChanged += this.dtpFecha_ValueChanged;

            this.btnGuardar.Location = new System.Drawing.Point(690, 121);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(185, 40);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar asistencia";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += this.btnGuardar_Click;

            // Tabla de asistencia
            this.pnlCardTabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlCardTabla.Location = new System.Drawing.Point(28, 165);
            this.pnlCardTabla.Name = "pnlCardTabla";
            this.pnlCardTabla.Size = new System.Drawing.Size(1336, 575);
            this.pnlCardTabla.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblTituloTabla, this.dgvAsistencia, this.lblNotaTabla
            });

            this.lblTituloTabla.AutoSize = true;
            this.lblTituloTabla.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloTabla.Location = new System.Drawing.Point(0, 0);
            this.lblTituloTabla.Name = "lblTituloTabla";
            this.lblTituloTabla.Text = "Asistencia del aula";

            this.dgvAsistencia.AllowUserToAddRows = false;
            this.dgvAsistencia.AllowUserToDeleteRows = false;
            this.dgvAsistencia.AllowUserToResizeRows = false;
            this.dgvAsistencia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvAsistencia.AutoGenerateColumns = false;
            this.dgvAsistencia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistencia.BackgroundColor = System.Drawing.Color.White;
            this.dgvAsistencia.Columns.AddRange(this.colCodigo, this.colEstudiante, this.colEstado, this.colAcciones);
            this.dgvAsistencia.Location = new System.Drawing.Point(0, 21);
            this.dgvAsistencia.MultiSelect = false;
            this.dgvAsistencia.Name = "dgvAsistencia";
            this.dgvAsistencia.ReadOnly = true;
            this.dgvAsistencia.RowHeadersVisible = false;
            this.dgvAsistencia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsistencia.Size = new System.Drawing.Size(1336, 526);
            this.dgvAsistencia.TabIndex = 3;
            this.dgvAsistencia.CellContentClick += this.dgvAsistencia_CellContentClick;

            this.colCodigo.FillWeight = 15F;
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.Name = "colCodigo";
            this.colEstudiante.FillWeight = 35F;
            this.colEstudiante.HeaderText = "Estudiante";
            this.colEstudiante.Name = "colEstudiante";
            this.colEstado.FillWeight = 25F;
            this.colEstado.HeaderText = "Estado registrado";
            this.colEstado.Name = "colEstado";
            this.colAcciones.FillWeight = 25F;
            this.colAcciones.HeaderText = "Acciones";
            this.colAcciones.Name = "colAcciones";

            this.lblNotaTabla.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblNotaTabla.AutoSize = true;
            this.lblNotaTabla.Location = new System.Drawing.Point(0, 554);
            this.lblNotaTabla.Name = "lblNotaTabla";
            this.lblNotaTabla.Text = "Una fecha sin registro se muestra como Sin registro y no se cuenta como falta.";

            // Totales y acciones
            this.badgePresentes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.badgePresentes.AutoSize = true;
            this.badgePresentes.Location = new System.Drawing.Point(28, 759);
            this.badgePresentes.Name = "badgePresentes";
            this.badgePresentes.Text = "0 presentes";
            this.badgeFaltas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.badgeFaltas.AutoSize = true;
            this.badgeFaltas.Location = new System.Drawing.Point(139, 759);
            this.badgeFaltas.Name = "badgeFaltas";
            this.badgeFaltas.Text = "0 faltas";
            this.badgeTardanzas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.badgeTardanzas.AutoSize = true;
            this.badgeTardanzas.Location = new System.Drawing.Point(226, 759);
            this.badgeTardanzas.Name = "badgeTardanzas";
            this.badgeTardanzas.Text = "0 tardanzas";

            this.btnMatrizSemanal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnMatrizSemanal.Location = new System.Drawing.Point(339, 748);
            this.btnMatrizSemanal.Name = "btnMatrizSemanal";
            this.btnMatrizSemanal.Size = new System.Drawing.Size(185, 40);
            this.btnMatrizSemanal.TabIndex = 4;
            this.btnMatrizSemanal.Text = "Ver matriz semanal";
            this.btnMatrizSemanal.UseVisualStyleBackColor = true;

            this.lblFooter.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblFooter.Location = new System.Drawing.Point(28, 799);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Text = "Gestión Escolar · Registro, matrícula y asistencia";

            // FormAsistencia
            // 
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 887);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAsistencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión Escolar - Asistencia";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlCardTabla.ResumeLayout(false);
            this.pnlCardTabla.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsistencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.Label lblSubtituloApp;
        private System.Windows.Forms.Label lblAnioEscolar;
        private Guna.UI2.WinForms.Guna2Button btnInicio;
        private Guna.UI2.WinForms.Guna2Button btnEstudiantes;
        private Guna.UI2.WinForms.Guna2Button btnAulas;
        private Guna.UI2.WinForms.Guna2Button btnMatriculas;
        private Guna.UI2.WinForms.Guna2Button btnAsistencia;
        private Guna.UI2.WinForms.Guna2Button btnReportes;
        private Guna.UI2.WinForms.Guna2Button btnArchivos;
        private Guna.UI2.WinForms.Guna2Button lblDatosGuardados;
        private System.Windows.Forms.Label lblTrabajoLocal;
        private System.Windows.Forms.Label lblDatosDemo;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblAdminAcademica;
        private Guna.UI2.WinForms.Guna2Button lblBadgeAnio;
        private System.Windows.Forms.Label lblOperador;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTituloSeccion;
        private System.Windows.Forms.Label lblSubtituloSeccion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblAula;
        private System.Windows.Forms.ComboBox cmbAula;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Panel pnlCardTabla;
        private System.Windows.Forms.Label lblTituloTabla;
        private System.Windows.Forms.DataGridView dgvAsistencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstudiante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAcciones;
        private System.Windows.Forms.Label lblNotaTabla;
        private System.Windows.Forms.Label badgePresentes;
        private System.Windows.Forms.Label badgeFaltas;
        private System.Windows.Forms.Label badgeTardanzas;
        private System.Windows.Forms.Button btnMatrizSemanal;
        private System.Windows.Forms.Label lblFooter;
    }
}
