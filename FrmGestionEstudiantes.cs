using GestionEstudiante.model;
using GestionEstudiante.service;
using System.Text;

namespace GestionEstudiante;

public partial class FrmGestionEstudiantes : Form
{

    private EstudianteService service = new EstudianteService();
    private Estudiante estudiante = null;

    public FrmGestionEstudiantes()
    {
        InitializeComponent();
    }

    private void FrmGestionEstudiantes_Load(object sender, EventArgs e)
    {
        dgvEstudiantes.AllowUserToAddRows = false;
        dgvEstudiantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEstudiantes.MultiSelect = false;
    }

    private void btnAgregar_Click(object sender, EventArgs e)
    {
        Estudiante estudiante = new Estudiante();
        estudiante.nombre = txtNombre.Text.Trim().ToUpper();
        estudiante.carrera = txtCarrera.Text.Trim().ToUpper();
        estudiante.correo = txtCorreo.Text.Trim().ToUpper();
        estudiante.telefono = txtTelefono.Text.Trim().ToUpper();

        service.Agregar(estudiante);

        LimpiarTexts();
        MostrarDatos();
        CalcularTotales();
    }

    private void btnModificar_Click(object sender, EventArgs e)
    {
        if (this.estudiante == null)
        {
            MessageBox.Show("Debe seleccionar a un estudiante", "Seleccion",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        this.estudiante.nombre = txtNombre.Text.Trim();
        this.estudiante.carrera = txtCarrera.Text.Trim();
        this.estudiante.correo = txtCorreo.Text.Trim();
        this.estudiante.telefono = txtTelefono.Text.Trim();

        service.Modificar(this.estudiante);
        this.estudiante = null;
        LimpiarTexts();
        MostrarDatos();
        CalcularTotales();

    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (this.estudiante == null)
        {
            MessageBox.Show("Debe seleccionar a un estudiante", "Seleccion",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        service.Eliminar(this.estudiante.id);
        this.estudiante = null;
        LimpiarTexts();
        MostrarDatos();
        CalcularTotales();
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarTexts();
    }

    private void btnProcesar_Click(object sender, EventArgs e)
    {
        string texto = txtBuscar.Text.Trim();

        if(string.IsNullOrWhiteSpace(texto))
        {
            MessageBox.Show("Ingrese una cadena de texto", "Cadena", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnBuscar_Click(object sender, EventArgs e)
    {
        lstResultados.Items.Clear();
        string busqueda = txtBuscar.Text.Trim();

        if(busqueda == "")
        {
            return;
        }

        Estudiante[] estudiantes = service.Listar();

        for (int i = 0; i < estudiantes.Length; i++)
        {
            if (estudiantes[i] != null 
                && estudiantes[i].nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
            {
                lstResultados.Items.Add($"{estudiantes[i].codigo} - {estudiantes[i].nombre} - {estudiantes[i].carrera}");
            }
        }

    }

    private void dgvEstudiantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        int posicionSeleccionada = e.RowIndex;
        DataGridViewRow seleccion = dgvEstudiantes.Rows[posicionSeleccionada];
        int id = int.Parse(seleccion.Cells[0].Value.ToString());
        //buscar por id
        this.estudiante = service.ObtenerPorId(id);
        if (this.estudiante != null)
        {
            txtCodigo.Text = this.estudiante.codigo;
            txtNombre.Text = this.estudiante.nombre;
            txtCarrera.Text = this.estudiante.carrera;
            txtCorreo.Text = this.estudiante.correo;
            txtTelefono.Text = this.estudiante.telefono;
        }

    }

    private void MostrarDatos()
    {
        dgvEstudiantes.Rows.Clear();
        Estudiante[] listado = service.Listar();

        for (int i = 0; i < listado.Length; i++)
        {
            Estudiante estudiante = listado[i];
            if (estudiante != null)
            {
                dgvEstudiantes.Rows.Add(estudiante.id, estudiante.codigo,
                    estudiante.nombre, estudiante.carrera, estudiante.correo, estudiante.telefono);
            }
        }

    }

    private void CalcularTotales()
    {
        lblTotalValor.Text = service.Conteo().ToString();
        lblCarreraValor.Text = "ninguno";
        lblMedicinaValor.Text = "";
    }

    private void LimpiarTexts()
    {
        txtCodigo.Clear();
        txtNombre.Clear();
        txtCarrera.Clear();
        txtCorreo.Clear();
        txtTelefono.Clear();
    }

}
