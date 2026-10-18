using GestionEstudiante.model;
using GestionEstudiante.service;
using System.Net.Mail;

namespace GestionEstudiante;

public partial class FrmGestionEstudiantes : Form
{
    // El formulario usa el servicio para consultar y guardar estudiantes.
    private readonly EstudianteService servicioEstudiantes = new();
    // null indica que no hay un estudiante seleccionado para editar o eliminar.
    private Estudiante? estudianteSeleccionado;

    public FrmGestionEstudiantes()
    {
        // Crea los controles y conecta sus eventos definidos en el archivo Designer.
        InitializeComponent();
    }

    // Eventos: cada evento indica la acción que debe ejecutarse.
    private void FrmGestionEstudiantes_Load(object? sender, EventArgs e)
    {
        ActualizarFormulario();
    }

    private void txtBuscar_TextChanged(object? sender, EventArgs e)
    {
        // Cambiar la búsqueda cancela la edición y vuelve a filtrar la tabla.
        PrepararNuevoRegistro();
        ActualizarListadoEstudiantes();
    }

    private void btnLimpiarFiltro_Click(object? sender, EventArgs e)
    {
        // Al cambiar el texto también se ejecuta el evento TextChanged.
        txtBuscar.Clear();
    }

    private void btnAgregar_Click(object? sender, EventArgs e)
    {
        RegistrarEstudiante();
    }

    private void btnModificar_Click(object? sender, EventArgs e)
    {
        GuardarCambiosEstudiante();
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        EliminarEstudianteSeleccionado();
    }

    private void btnLimpiar_Click(object? sender, EventArgs e)
    {
        PrepararNuevoRegistro();
    }

    private void dgvEstudiantes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        // El encabezado tiene índice -1; solo cargamos filas asociadas a un estudiante.
        if (e.RowIndex < 0 || dgvEstudiantes.Rows[e.RowIndex].Tag is not Estudiante seleccionado)
        {
            return;
        }

        CargarEstudianteParaEditar(seleccionado);
    }

    // Guardamos un estudiante nuevo solo si hay cupos y los campos son válidos.
    private void RegistrarEstudiante()
    {
        if (!HayCuposDisponibles())
        {
            MessageBox.Show($"Se alcanzó el límite de {servicioEstudiantes.Capacidad} estudiantes.",
                "Sin cupos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!ValidarDatosFormulario())
        {
            return;
        }

        var nuevoEstudiante = new Estudiante();
        CopiarCamposAlEstudiante(nuevoEstudiante);
        // El servicio convierte los textos a mayúsculas; el repositorio asigna código y fecha.
        servicioEstudiantes.Agregar(nuevoEstudiante);
        ActualizarFormulario();
    }

    // Modificamos el estudiante seleccionado, conservando su código y fecha de creación.
    private void GuardarCambiosEstudiante()
    {
        if (estudianteSeleccionado == null || !ValidarDatosFormulario())
        {
            return;
        }

        CopiarCamposAlEstudiante(estudianteSeleccionado);
        servicioEstudiantes.Modificar(estudianteSeleccionado);
        ActualizarFormulario();
    }

    // Solo eliminamos si hay una selección y el usuario confirma la operación.
    private void EliminarEstudianteSeleccionado()
    {
        if (estudianteSeleccionado == null || !ConfirmarEliminacion(estudianteSeleccionado))
        {
            return;
        }

        servicioEstudiantes.Eliminar(estudianteSeleccionado.id);
        ActualizarFormulario();
    }

    // Devuelve true únicamente cuando el usuario elige Sí en el mensaje.
    private bool ConfirmarEliminacion(Estudiante estudiante)
    {
        DialogResult respuesta = MessageBox.Show(
            $"¿Eliminar a {estudiante.nombre} ({estudiante.codigo})?",
            "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        return respuesta == DialogResult.Yes;
    }

    private bool HayCuposDisponibles()
    {
        return servicioEstudiantes.Conteo() < servicioEstudiantes.Capacidad;
    }

    // Cada validación devuelve true si el campo es válido y false si hay un error.
    // Nos detenemos en el primer error para mostrar un solo MessageBox por intento.
    private bool ValidarDatosFormulario()
    {
        if (!ValidarNombre())
        {
            return false;
        }

        if (!ValidarCarrera())
        {
            return false;
        }

        if (!ValidarCorreo())
        {
            return false;
        }

        if (!ValidarTelefono())
        {
            return false;
        }

        return true;
    }

    private bool ValidarNombre()
    {
        // Un nombre vacío o formado únicamente por espacios no es válido.
        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            return true;
        }

        MessageBox.Show("Ingrese el nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        // Focus coloca el cursor en el campo que debe corregirse.
        txtNombre.Focus();
        return false;
    }

    private bool ValidarCarrera()
    {
        // SelectedIndex vale -1 cuando todavía no se ha elegido una opción del combo.
        if (cboCarrera.SelectedIndex >= 0)
        {
            return true;
        }

        MessageBox.Show("Seleccione la carrera.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        cboCarrera.Focus();
        return false;
    }

    // El correo es opcional; si se completa, debe ser una dirección válida.
    private bool ValidarCorreo()
    {
        string correo = txtCorreo.Text.Trim();
        // TryCreate intenta interpretar el correo sin generar una excepción.
        // La comparación evita aceptar un nombre acompañado de una dirección, como Ana <ana@correo.com>.
        if (correo.Length == 0 ||
            (MailAddress.TryCreate(correo, out var direccion) && direccion.Address == correo))
        {
            return true;
        }

        MessageBox.Show("Ingrese un correo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtCorreo.Focus();
        return false;
    }

    // El teléfono es opcional; si se completa, debe contener entre 7 y 15 dígitos.
    private bool ValidarTelefono()
    {
        string telefono = txtTelefono.Text.Trim();
        if (telefono.Length == 0)
        {
            return true;
        }

        int cantidadDigitos = 0;
        bool caracteresValidos = true;
        // Contamos solo los dígitos; permitimos espacios, +, guiones y paréntesis como formato.
        foreach (char caracter in telefono)
        {
            if (char.IsDigit(caracter))
            {
                cantidadDigitos++;
            }
            else if (caracter != '+' && caracter != ' ' && caracter != '-' && caracter != '(' && caracter != ')')
            {
                caracteresValidos = false;
            }
        }

        if (cantidadDigitos >= 7 && cantidadDigitos <= 15 && caracteresValidos)
        {
            return true;
        }

        MessageBox.Show("Ingrese un teléfono de 7 a 15 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtTelefono.Focus();
        return false;
    }

    // Reutilizamos este método al agregar y editar. Trim quita los espacios de los extremos.
    private void CopiarCamposAlEstudiante(Estudiante destino)
    {
        destino.nombre = txtNombre.Text.Trim();
        destino.carrera = cboCarrera.Text.Trim();
        destino.correo = txtCorreo.Text.Trim();
        destino.telefono = txtTelefono.Text.Trim();
    }

    // Mostramos los datos de la fila elegida y guardamos qué estudiante se está editando.
    private void CargarEstudianteParaEditar(Estudiante seleccionado)
    {
        estudianteSeleccionado = seleccionado;
        txtCodigo.Text = seleccionado.codigo;
        txtNombre.Text = seleccionado.nombre;
        cboCarrera.SelectedItem = seleccionado.carrera;
        txtCorreo.Text = seleccionado.correo;
        txtTelefono.Text = seleccionado.telefono;
        ActivarModoEdicion();
    }

    // Durante la edición se puede guardar o eliminar, pero no agregar otro registro.
    private void ActivarModoEdicion()
    {
        lblModo.Text = "Editar estudiante";
        btnModificar.Enabled = true;
        btnEliminar.Enabled = true;
        btnAgregar.Enabled = false;
    }

    // Conservamos el filtro al limpiar los campos o terminar una operación.
    private void PrepararNuevoRegistro()
    {
        estudianteSeleccionado = null;
        LimpiarCamposFormulario();
        lblModo.Text = "Nuevo estudiante";
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        btnAgregar.Enabled = HayCuposDisponibles();
        dgvEstudiantes.ClearSelection();
    }

    // Vacía los campos sin borrar estudiantes del almacenamiento.
    private void LimpiarCamposFormulario()
    {
        txtCodigo.Clear();
        txtNombre.Clear();
        cboCarrera.SelectedIndex = -1;
        txtCorreo.Clear();
        txtTelefono.Clear();
    }

    // Después de agregar, modificar o eliminar, actualizamos toda la información visible.
    private void ActualizarFormulario()
    {
        PrepararNuevoRegistro();
        ActualizarListadoEstudiantes();
        ActualizarIndicadores();
    }

    // Reconstruimos las filas que coinciden con la búsqueda sin cambiar los datos guardados.
    private void ActualizarListadoEstudiantes()
    {
        dgvEstudiantes.Rows.Clear();
        string busqueda = txtBuscar.Text.Trim();
        foreach (Estudiante registro in servicioEstudiantes.Listar())
        {
            if (CoincideConBusqueda(registro, busqueda))
            {
                AgregarEstudianteALaTabla(registro);
            }
        }

        dgvEstudiantes.ClearSelection();
        ActualizarResumenResultados();
    }

    // La búsqueda no distingue mayúsculas de minúsculas. Un texto vacío coincide con todos.
    private bool CoincideConBusqueda(Estudiante registro, string busqueda)
    {
        return registro.nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
            registro.codigo.Contains(busqueda, StringComparison.OrdinalIgnoreCase);
    }

    private void AgregarEstudianteALaTabla(Estudiante registro)
    {
        int fila = dgvEstudiantes.Rows.Add(registro.codigo, registro.nombre, registro.carrera,
            registro.correo, registro.telefono, registro.fechaCreacion.ToString("dd/MM/yyyy"));

        // Tag permite recuperar al estudiante aunque cambie la posición de la fila.
        dgvEstudiantes.Rows[fila].Tag = registro;
    }

    // Indicamos cuántos registros se ven.
    private void ActualizarResumenResultados()
    {
        string resumen = dgvEstudiantes.Rows.Count == 0
            ? "Sin resultados"
            : $"Mostrando {dgvEstudiantes.Rows.Count} de {servicioEstudiantes.Conteo()} estudiantes";

        lblResultados.Text = resumen;
    }

    // Los indicadores consideran todos los registros, incluso al filtrar la tabla.
    private void ActualizarIndicadores()
    {
        int cantidadEstudiantes = servicioEstudiantes.Conteo();
        lblTotalValor.Text = cantidadEstudiantes.ToString();
        lblCuposValor.Text = (servicioEstudiantes.Capacidad - cantidadEstudiantes).ToString();
        lblCarreraValor.Text = ObtenerCarreraConMasEstudiantes(servicioEstudiantes.Listar());
    }

    // Buscamos la mayor cantidad por carrera; si varias comparten ese máximo, hay empate.
    // Si el arreglo está vacío, el resultado permanece como "Sin datos".
    private string ObtenerCarreraConMasEstudiantes(Estudiante[] estudiantes)
    {
        int mayorCantidad = 0;
        string carreraMayor = "Sin datos";
        bool hayEmpate = false;

        foreach (Estudiante registro in estudiantes)
        {
            string carreraActual = registro.carrera.Trim();
            int cantidad = ContarEstudiantesPorCarrera(estudiantes, carreraActual);

            if (cantidad > mayorCantidad)
            {
                mayorCantidad = cantidad;
                carreraMayor = carreraActual;
                // Una cantidad mayor deja atrás cualquier empate encontrado antes.
                hayEmpate = false;
            }
            else if (cantidad == mayorCantidad &&
                !string.Equals(carreraActual, carreraMayor, StringComparison.OrdinalIgnoreCase))
            {
                // Deben ser carreras distintas: repetir la misma carrera no es un empate.
                hayEmpate = true;
            }
        }

        return hayEmpate ? "Empate" : carreraMayor;
    }

    // Recorremos todos los estudiantes y contamos los que pertenecen a la carrera indicada.
    private int ContarEstudiantesPorCarrera(Estudiante[] estudiantes, string carrera)
    {
        int cantidad = 0;
        foreach (Estudiante registro in estudiantes)
        {
            if (string.Equals(carrera, registro.carrera.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                cantidad++;
            }
        }

        return cantidad;
    }
}
