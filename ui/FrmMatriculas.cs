using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestionEscolar.Matriculas
{
    // =====================================================
    // 1) COLORES (sacados del diseño)
    // =====================================================
    static class Paleta
    {
        public static readonly Color Fondo       = Color.FromArgb(245, 245, 245);
        public static readonly Color Blanco      = Color.White;
        public static readonly Color Borde       = ColorTranslator.FromHtml("#E4E4E7");
        public static readonly Color Oscuro      = ColorTranslator.FromHtml("#1F1F23");
        public static readonly Color Texto       = ColorTranslator.FromHtml("#18181B");
        public static readonly Color TextoSuave  = ColorTranslator.FromHtml("#71717A");

        public static readonly Color VerdeFondo    = ColorTranslator.FromHtml("#DCFCE7");
        public static readonly Color VerdeTexto    = ColorTranslator.FromHtml("#166534");
        public static readonly Color RojoFondo     = ColorTranslator.FromHtml("#FEE2E2");
        public static readonly Color RojoTexto     = ColorTranslator.FromHtml("#991B1B");
        public static readonly Color AmarilloFondo = ColorTranslator.FromHtml("#FEF3C7");
        public static readonly Color AmarilloTexto = ColorTranslator.FromHtml("#92400E");
    }

    // =====================================================
    // 2) MODELOS
    // =====================================================
    class Aula
    {
        public string Nombre { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public override string ToString() { return Nombre; }
    }

    class Estudiante
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public override string ToString() { return Codigo + " · " + Nombre; }
    }

    class Matricula
    {
        public string Codigo { get; set; } = string.Empty;
        public Estudiante Estudiante { get; set; } = null!;
        public Aula Aula { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public bool Activa { get; set; }
    }

    // =====================================================
    // 3) CONTROLES REUTILIZABLES
    // =====================================================
    static class UI
    {
        public static Button Boton(string texto)
        {
            return new Button
            {
                Text = texto,
                Size = new Size(150, 40),
                UseVisualStyleBackColor = true
            };
        }

        public static Label Etiqueta(string texto, float tam = 9.5f, bool negrita = false, bool suave = false)
        {
            return new Label
            {
                Text = texto,
                AutoSize = true,
                ForeColor = suave ? Paleta.TextoSuave : Paleta.Texto,
                Font = new Font("Segoe UI", tam, negrita ? FontStyle.Bold : FontStyle.Regular),
                BackColor = Color.Transparent
            };
        }

        public static ComboBox Combo(int x, int y, int ancho)
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(x, y),
                Width = ancho,
                BackColor = Color.White
            };
        }
    }

    // Panel blanco con borde gris (las "tarjetas" del diseño)
    class Tarjeta : Panel
    {
        public Tarjeta() { BackColor = Paleta.Blanco; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var p = new Pen(Paleta.Borde))
                e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        }
    }

    // Aviso con "etiqueta de color" + mensaje (verde, rojo o amarillo)
    class Banner : Tarjeta
    {
        readonly Label lblTag = UI.Etiqueta("", 9f, true);
        readonly Label lblMsg = UI.Etiqueta("", 9.5f);

        public Banner()
        {
            Height = 40;
            Visible = false;
            lblTag.Padding = new Padding(6, 2, 6, 2);
            lblTag.Location = new Point(12, 9);
            lblMsg.Top = 11;
            Controls.Add(lblTag);
            Controls.Add(lblMsg);
        }

        public void Mostrar(string etiqueta, string mensaje, Color fondo, Color texto)
        {
            lblTag.Text = etiqueta;
            lblTag.BackColor = fondo;
            lblTag.ForeColor = texto;
            lblMsg.Text = mensaje;
            lblMsg.Left = lblTag.Right + 10;
            Visible = true;
        }
    }

    // =====================================================
    // 4) VENTANA DE CONFIRMACIÓN (pantalla 6)
    // =====================================================
    class FrmCancelar : Form
    {
        public FrmCancelar(Matricula m, DateTime fecha)
        {
            Text = "Cancelar matrícula";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false; MinimizeBox = false;
            ClientSize = new Size(470, 230);
            BackColor = Paleta.Fondo;
            Font = new Font("Segoe UI", 10f);

            var tag = UI.Etiqueta("Revisar operación", 9f, true);
            tag.BackColor = Paleta.AmarilloFondo;
            tag.ForeColor = Paleta.AmarilloTexto;
            tag.Padding = new Padding(6, 2, 6, 2);
            tag.Location = new Point(20, 18);

            var titulo = UI.Etiqueta("Cancelar matrícula de " + m.Estudiante.Nombre, 12f, true);
            titulo.Location = new Point(20, 54);

            var detalle = UI.Etiqueta(
                "Se liberará un cupo de " + m.Aula.Nombre + ". La matrícula y las\n" +
                "asistencias anteriores se conservarán.\n\n" +
                "Fecha de cancelación: " + fecha.ToString("dd/MM/yyyy"), 9.5f, false, true);
            detalle.Location = new Point(20, 90);

            var btnMantener = UI.Boton("Mantener matrícula");
            btnMantener.Width = 150;
            btnMantener.Location = new Point(150, 180);
            btnMantener.DialogResult = DialogResult.Cancel;

            var btnConfirmar = UI.Boton("Cancelar matrícula");
            btnConfirmar.Width = 150;
            btnConfirmar.Location = new Point(305, 180);
            btnConfirmar.DialogResult = DialogResult.OK;

            Controls.AddRange(new Control[] { tag, titulo, detalle, btnMantener, btnConfirmar });
            AcceptButton = btnConfirmar;
            CancelButton = btnMantener;
        }
    }

    // =====================================================
    // 5) FORMULARIO PRINCIPAL DE MATRÍCULAS
    // =====================================================
    public class FrmMatriculas : Form
    {
        // ---- Datos en memoria (demo) ----
        readonly List<Aula> aulas = new List<Aula>();
        readonly List<Estudiante> estudiantes = new List<Estudiante>();
        readonly List<Matricula> matriculas = new List<Matricula>();

        // ---- Controles ----
        private Panel pnlLista = null!;
        private Panel pnlNueva = null!;
        private Banner bannerLista = null!;
        private Banner bannerNueva = null!;
        private Label tituloGrid = null!;
        private DataGridView grid = null!;
        private ComboBox cboFiltroAula = null!;
        private ComboBox cboFiltroEstado = null!;
        private ComboBox cboEstudiante = null!;
        private ComboBox cboAula = null!;
        private DateTimePicker dtpFecha = null!;
        private Label lblDisp = null!;
        private Label lblNota = null!;
        private Button btnRegistrar = null!;

        public FrmMatriculas()
        {
            Text = "Gestión Escolar - Matrículas";
            ClientSize = new Size(1150, 700);
            MinimumSize = new Size(950, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Paleta.Fondo;
            Font = new Font("Segoe UI", 10f);

            CargarDatos();
            ConstruirPrincipal();
            ConstruirLista();
            ConstruirNueva();
            ConstruirSidebar();
            MostrarLista();
        }

        // ---------- Datos de ejemplo (iguales a las pantallas) ----------
        void CargarDatos()
        {
            var a = new Aula { Nombre = "Primaria · 1.º A", Capacidad = 4 };
            var b = new Aula { Nombre = "Primaria · 1.º B", Capacidad = 2 };
            aulas.Add(a); aulas.Add(b);

            string[] nombres = { "Valeria Rojas", "Mateo Flores", "Luciana Castro",
                                 "Diego Medina", "Sofía Torres", "Camila Vega" };
            for (int i = 0; i < nombres.Length; i++)
                estudiantes.Add(new Estudiante { Codigo = "E" + (i + 1).ToString("000000"), Nombre = nombres[i] });

            Aula[] aulaDe = { a, a, a, b, b };
            for (int i = 0; i < 5; i++)
                matriculas.Add(new Matricula
                {
                    Codigo = "M" + (i + 1).ToString("000"),
                    Estudiante = estudiantes[i],
                    Aula = aulaDe[i],
                    Fecha = new DateTime(2026, 10, 2),
                    Activa = true
                });
        }

        // ---------- Reglas de negocio ----------
        int CuposLibres(Aula aula)
        {
            int ocupados = matriculas.Count(m => m.Activa && m.Aula == aula);
            return aula.Capacidad - ocupados;
        }

        bool TieneMatriculaActiva(Estudiante est)
        {
            return matriculas.Any(m => m.Activa && m.Estudiante == est);
        }

        static string TextoCupos(int n)
        {
            return n == 1 ? "1 cupo libre" : n + " cupos libres";
        }

        // ---------- Estructura general ----------
        void ConstruirPrincipal()
        {
            var pnlMain = new Panel { Dock = DockStyle.Fill, BackColor = Paleta.Fondo };

            var header = new Panel { Dock = DockStyle.Top, Height = 44, BackColor = Paleta.Blanco };
            var lblAdm = UI.Etiqueta("Administración académica", 9.5f, true);
            lblAdm.Location = new Point(24, 12);
            var lblAnio = UI.Etiqueta("2026     Operador local", 9f, false, true);
            lblAnio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAnio.Location = new Point(header.Width - 190, 13);
            header.Controls.Add(lblAdm);
            header.Controls.Add(lblAnio);
            header.Resize += (s, e) => lblAnio.Left = header.Width - lblAnio.Width - 24;
            header.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(Paleta.Borde), 0, header.Height - 1, header.Width, header.Height - 1);

            pnlLista = new Panel { Dock = DockStyle.Fill, BackColor = Paleta.Fondo };
            pnlNueva = new Panel { Dock = DockStyle.Fill, BackColor = Paleta.Fondo, Visible = false };

            pnlMain.Controls.Add(pnlLista);
            pnlMain.Controls.Add(pnlNueva);
            pnlMain.Controls.Add(header);
            pnlLista.BringToFront();
            pnlNueva.BringToFront();
            Controls.Add(pnlMain);
        }

        void ConstruirSidebar()
        {
            var side = new Panel { Dock = DockStyle.Left, Width = 190, BackColor = Paleta.Blanco };
            side.Paint += (s, e) =>
                e.Graphics.DrawLine(new Pen(Paleta.Borde), side.Width - 1, 0, side.Width - 1, side.Height);

            var logo = new Label
            {
                Text = "GE", ForeColor = Color.White, BackColor = Paleta.Oscuro,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(28, 28), Location = new Point(14, 14)
            };
            var lblApp = UI.Etiqueta("Gestión Escolar", 9.5f, true);
            lblApp.Location = new Point(48, 12);
            var lblCentro = UI.Etiqueta("Centro educativo", 8f, false, true);
            lblCentro.Location = new Point(48, 30);
            var lblAnio = UI.Etiqueta("AÑO ESCOLAR 2026", 7.5f, true, true);
            lblAnio.Location = new Point(14, 52);

            side.Controls.AddRange(new Control[] { logo, lblApp, lblCentro, lblAnio });

            string[] items = { "Inicio", "Estudiantes", "Aulas", "Matrículas",
                               "Asistencia", "Reportes", "Archivos y respaldo" };
            int y = 85;
            foreach (var it in items)
            {
                bool activo = it == "Matrículas";
                var l = new Label
                {
                    Text = it,
                    Location = new Point(10, y),
                    Size = new Size(170, 30),
                    Padding = new Padding(8, 0, 0, 0),
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = activo ? Paleta.Fondo : Paleta.Blanco,
                    ForeColor = activo ? Paleta.Texto : Paleta.TextoSuave,
                    Font = new Font("Segoe UI", 9.5f, activo ? FontStyle.Bold : FontStyle.Regular)
                };
                side.Controls.Add(l);
                y += 34;
            }

            var estado = new Label
            {
                Text = "Datos guardados",
                BackColor = Paleta.VerdeFondo, ForeColor = Paleta.VerdeTexto,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                AutoSize = true, Padding = new Padding(6, 2, 6, 2),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            var pie = UI.Etiqueta("Trabajo local\nDatos de demostración", 8f, false, true);
            pie.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            side.Controls.Add(estado);
            side.Controls.Add(pie);
            side.Resize += (s, e) =>
            {
                pie.Location = new Point(14, side.Height - pie.Height - 12);
                estado.Location = new Point(14, pie.Top - estado.Height - 8);
            };

            Controls.Add(side);
        }

        // =====================================================
        // PANTALLA 1 / 3 / 7: LISTA DE MATRÍCULAS
        // =====================================================
        void ConstruirLista()
        {
            var titulo = UI.Etiqueta("Matrículas", 20f, true);
            titulo.Location = new Point(28, 24);
            var sub = UI.Etiqueta("Revisa las matrículas activas y conserva el historial de cancelaciones.", 10f);
            sub.Location = new Point(28, 72);

            var lblA = UI.Etiqueta("Aula", 10f);
            lblA.Location = new Point(28, 101);
            cboFiltroAula = UI.Combo(28, 126, 210);
            cboFiltroAula.Items.Add("Todas las aulas");
            foreach (var a in aulas) cboFiltroAula.Items.Add(a);
            cboFiltroAula.SelectedIndex = 0;

            var lblE = UI.Etiqueta("Estado", 10f);
            lblE.Location = new Point(254, 101);
            cboFiltroEstado = UI.Combo(254, 126, 210);
            cboFiltroEstado.Items.AddRange(new object[] { "Activas y canceladas", "Solo activas", "Solo canceladas" });
            cboFiltroEstado.SelectedIndex = 0;

            var btnNueva = UI.Boton("+ Nueva matrícula");
            btnNueva.Size = new Size(185, 40);
            btnNueva.Location = new Point(482, 121);
            btnNueva.Click += (s, e) => MostrarNueva();

            cboFiltroAula.SelectedIndexChanged += (s, e) => RefrescarGrid();
            cboFiltroEstado.SelectedIndexChanged += (s, e) => RefrescarGrid();

            bannerLista = new Banner { Location = new Point(28, 166) };
            tituloGrid = UI.Etiqueta("Matrículas del año escolar", 9f, true);
            ConfigurarGrid();
            lblNota = UI.Etiqueta("La cancelación libera el cupo y conserva el historial del estudiante.", 10f);

            pnlLista.Controls.AddRange(new Control[]
            {
                titulo, sub, lblA, cboFiltroAula, lblE, cboFiltroEstado, btnNueva,
                bannerLista, tituloGrid, grid, lblNota
            });
            pnlLista.Resize += (s, e) => Reacomodar();
        }

        void Reacomodar()
        {
            int ancho = pnlLista.ClientSize.Width - 56;
            if (ancho < 300) return;

            bannerLista.Width = ancho;
            int top = bannerLista.Visible ? 234 : 186;
            tituloGrid.Location = new Point(28, top - 21);
            grid.SetBounds(28, top, ancho, Math.Max(150, pnlLista.ClientSize.Height - top - 72));
            lblNota.Location = new Point(28, grid.Bottom + 18);
        }

        void ConfigurarGrid()
        {
            grid = new DataGridView
            {
                Name = "dgvMatriculas",
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            grid.Columns.Add("cod", "Código");
            grid.Columns.Add("est", "Estudiante");
            grid.Columns.Add("aula", "Aula");
            grid.Columns.Add("fecha", "Fecha");
            grid.Columns.Add("estado", "Estado");
            var accion = new DataGridViewButtonColumn { Name = "acc", HeaderText = "Acciones" };
            grid.Columns.Add(accion);

            grid.CellContentClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex != accion.Index) return;
                var m = grid.Rows[e.RowIndex].Tag as Matricula;
                if (m != null && m.Activa) ConfirmarCancelacion(m);
            };
        }

        void RefrescarGrid()
        {
            grid.Rows.Clear();
            IEnumerable<Matricula> q = matriculas;

            var aulaSel = cboFiltroAula.SelectedItem as Aula;
            if (aulaSel != null) q = q.Where(m => m.Aula == aulaSel);

            if (cboFiltroEstado.SelectedIndex == 1) q = q.Where(m => m.Activa);
            else if (cboFiltroEstado.SelectedIndex == 2) q = q.Where(m => !m.Activa);

            foreach (var m in q)
            {
                int i = grid.Rows.Add(m.Codigo, m.Estudiante.Nombre, m.Aula.Nombre,
                                      m.Fecha.ToString("dd/MM/yyyy"),
                                      m.Activa ? "Activa" : "Cancelada",
                                      m.Activa ? "Cancelar" : "");
                grid.Rows[i].Tag = m;
            }
            grid.ClearSelection();
        }

        // =====================================================
        // PANTALLA 6 / 7: CANCELAR MATRÍCULA
        // =====================================================
        void ConfirmarCancelacion(Matricula m)
        {
            using (var dlg = new FrmCancelar(m, DateTime.Today))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
            }
            m.Activa = false; // se conserva en el historial, solo cambia el estado
            RefrescarGrid();
            bannerLista.Mostrar("Matrícula cancelada",
                "Se liberó un cupo de " + m.Aula.Nombre + ". Las asistencias anteriores se conservan.",
                Paleta.VerdeFondo, Paleta.VerdeTexto);
            Reacomodar();
        }

        // =====================================================
        // PANTALLAS 2 / 4 / 5: NUEVA MATRÍCULA
        // =====================================================
        void ConstruirNueva()
        {
            var titulo = UI.Etiqueta("Nueva matrícula", 20f, true);
            titulo.Location = new Point(28, 24);
            var sub = UI.Etiqueta("Selecciona un estudiante activo y un aula con cupos disponibles.", 10f);
            sub.Location = new Point(28, 72);

            bannerNueva = new Banner { Location = new Point(28, 104), Width = 920 };

            var card = new Panel { Location = new Point(28, 154), Size = new Size(920, 300) };

            var lblDatos = UI.Etiqueta("Datos de matrícula", 10f, true);
            lblDatos.Location = new Point(0, 0);

            var lblEst = UI.Etiqueta("Estudiante *", 10f); lblEst.Location = new Point(0, 36);
            cboEstudiante = UI.Combo(0, 62, 420);
            foreach (var e in estudiantes) cboEstudiante.Items.Add(e);

            var lblAula = UI.Etiqueta("Aula *", 10f); lblAula.Location = new Point(468, 36);
            cboAula = UI.Combo(468, 62, 420);
            foreach (var a in aulas) cboAula.Items.Add(a);

            var lblFecha = UI.Etiqueta("Fecha de matrícula *", 10f); lblFecha.Location = new Point(0, 114);
            dtpFecha = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Location = new Point(0, 140),
                Width = 420,
                Value = DateTime.Today
            };

            var lblDispT = UI.Etiqueta("Disponibilidad", 10f); lblDispT.Location = new Point(468, 114);
            lblDisp = new Label
            {
                Location = new Point(468, 140),
                Size = new Size(420, 28),
                BackColor = Paleta.Fondo,
                ForeColor = Paleta.TextoSuave,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0)
            };

            var lblRegla = UI.Etiqueta("Se permite una matrícula activa por estudiante y año escolar.", 8.5f, false, true);
            lblRegla.Location = new Point(0, 194);

            var btnCancelar = UI.Boton("Cancelar");
            btnCancelar.Width = 120;
            btnCancelar.Location = new Point(401, 242);
            btnCancelar.Click += (s, e) => MostrarLista();

            var btnCambiar = UI.Boton("Cambiar selección");
            btnCambiar.Width = 150;
            btnCambiar.Location = new Point(537, 242);
            btnCambiar.Click += (s, e) =>
            {
                cboEstudiante.SelectedIndex = 0;
                cboAula.SelectedIndex = 0;
            };

            btnRegistrar = UI.Boton("Registrar matrícula");
            btnRegistrar.Width = 185;
            btnRegistrar.Location = new Point(703, 242);
            btnRegistrar.Click += (s, e) => RegistrarMatricula();

            cboEstudiante.SelectedIndexChanged += (s, e) => ValidarSeleccion();
            cboAula.SelectedIndexChanged += (s, e) => ValidarSeleccion();

            card.Controls.AddRange(new Control[]
            {
                lblDatos, lblEst, cboEstudiante, lblAula, cboAula, lblFecha, dtpFecha,
                lblDispT, lblDisp, lblRegla, btnCancelar, btnCambiar, btnRegistrar
            });

            // Atajos de demostración (los enlaces del diseño)
            var lnkSinCupo = new LinkLabel { Text = "Ver aula sin cupos", AutoSize = true, Location = new Point(28, 466) };
            lnkSinCupo.Click += (s, e) =>
            {
                cboEstudiante.SelectedItem = estudiantes.First(x => x.Nombre == "Camila Vega");
                cboAula.SelectedItem = aulas.First(a => CuposLibres(a) == 0);
            };
            var lnkYaMat = new LinkLabel { Text = "Ver estudiante ya matriculado", AutoSize = true, Location = new Point(196, 466) };
            lnkYaMat.Click += (s, e) =>
            {
                cboEstudiante.SelectedItem = estudiantes[0];
                cboAula.SelectedIndex = 0;
            };

            pnlNueva.Controls.AddRange(new Control[] { titulo, sub, bannerNueva, card, lnkSinCupo, lnkYaMat });
        }

        // Valida y muestra el aviso que corresponda
        void ValidarSeleccion()
        {
            var est = cboEstudiante.SelectedItem as Estudiante;
            var aula = cboAula.SelectedItem as Aula;
            if (est == null || aula == null)
            {
                btnRegistrar.Enabled = false;
                return;
            }

            int libres = CuposLibres(aula);
            lblDisp.Text = TextoCupos(libres) + " de " + aula.Capacidad;

            bannerNueva.Visible = false;
            btnRegistrar.Enabled = true;

            if (TieneMatriculaActiva(est))
            {
                bannerNueva.Mostrar("Matrícula existente",
                    est.Nombre + " ya tiene una matrícula activa en el año escolar 2026.",
                    Paleta.AmarilloFondo, Paleta.AmarilloTexto);
                btnRegistrar.Enabled = false;
            }
            else if (libres <= 0)
            {
                int activas = aula.Capacidad - libres;
                bannerNueva.Mostrar("Aula sin cupos",
                    aula.Nombre + " tiene " + activas + " matrículas activas y capacidad para " + aula.Capacidad + ".",
                    Paleta.RojoFondo, Paleta.RojoTexto);
                btnRegistrar.Enabled = false;
            }
        }

        void RegistrarMatricula()
        {
            if (cboEstudiante.SelectedItem is not Estudiante est || cboAula.SelectedItem is not Aula aula)
            {
                return;
            }

            // Doble validación por seguridad
            if (TieneMatriculaActiva(est) || CuposLibres(aula) <= 0) { ValidarSeleccion(); return; }

            matriculas.Add(new Matricula
            {
                Codigo = "M" + (matriculas.Count + 1).ToString("000"),
                Estudiante = est,
                Aula = aula,
                Fecha = dtpFecha.Value.Date,
                Activa = true
            });

            bool ultimo = CuposLibres(aula) == 0;
            MostrarLista();
            bannerLista.Mostrar("Matrícula registrada",
                est.Nombre + " ocupa " + (ultimo ? "el último cupo" : "un cupo") + " de " + aula.Nombre + ".",
                Paleta.VerdeFondo, Paleta.VerdeTexto);
            Reacomodar();
        }

        // ---------- Navegación entre pantallas ----------
        void MostrarLista()
        {
            pnlNueva.Visible = false;
            pnlLista.Visible = true;
            bannerLista.Visible = false;
            RefrescarGrid();
            Reacomodar();
        }

        void MostrarNueva()
        {
            pnlLista.Visible = false;
            pnlNueva.Visible = true;
            bannerNueva.Visible = false;
            cboEstudiante.SelectedIndex = -1;
            cboAula.SelectedIndex = -1;
            lblDisp.Text = "Selecciona un aula";
            btnRegistrar.Enabled = false;
            dtpFecha.Value = DateTime.Today;
        }
    }
}

// ------------------------------------------------------------
// Para probar SOLO este módulo, en Program.cs:
//   Application.EnableVisualStyles();
//   Application.Run(new GestionEscolar.Matriculas.FrmMatriculas());
// Para abrirlo desde tu menú principal:
//   new GestionEscolar.Matriculas.FrmMatriculas().Show();
// ------------------------------------------------------------
