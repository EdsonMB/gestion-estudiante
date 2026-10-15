# Gestión de Estudiantes

Aplicación académica de escritorio en C# con Windows Forms y .NET 8, dirigida a estudiantes de Fundamentos de Programación. Permite practicar clases, métodos, condiciones, ciclos y arreglos mediante la gestión de estudiantes.

## Funcionalidades

- Formulario lateral para registrar y editar nombre, carrera, correo y teléfono.
- Carrera seleccionada mediante un combo de cinco opciones: ADMINISTRACIÓN, CONTABILIDAD, DERECHO, INGENIERÍA DE SISTEMAS e INGENIERÍA INDUSTRIAL.
- Datos de texto guardados en mayúsculas, incluido el correo, tanto al agregar como al modificar. Se eliminan los espacios al inicio y al final.
- Código y fecha de creación asignados automáticamente. El código tiene el formato `E000001`; ambos datos se conservan al editar.
- Selección para editar o eliminar mediante doble clic en una fila.
- Tabla con búsqueda por nombre o código, sin distinguir mayúsculas de minúsculas.
- Indicadores de total de estudiantes, cupos libres y carrera con más estudiantes (incluye empate y ausencia de datos).
- Validaciones con `MessageBox`: se muestra el primer error y el cursor vuelve al campo que debe corregirse.
- Confirmación antes de eliminar un estudiante.

## Uso

1. Escriba el nombre, seleccione una carrera y complete los datos de contacto si corresponde. Pulse **Agregar estudiante**.
2. Para editar, haga doble clic en una fila, cambie los datos y pulse **Guardar cambios**. Un clic simple no carga los datos en el formulario.
3. Para eliminar, haga doble clic en una fila, pulse **Eliminar seleccionado** y confirme la operación.
4. Use **Limpiar / Cancelar** para vaciar los campos y volver al registro de un nuevo estudiante, conservando el filtro de búsqueda.
5. Busque por nombre o código. **Limpiar filtro** vuelve a mostrar todos los estudiantes. Cambiar la búsqueda cancela la edición actual.

## Validaciones y almacenamiento

- Nombre y carrera son obligatorios. El nombre no puede contener únicamente espacios.
- Correo y teléfono son opcionales. Si se completan, el correo debe tener un formato válido y el teléfono debe contener entre 7 y 15 dígitos. Se permiten espacios, `+`, guiones y paréntesis en el teléfono.
- Si ocurre un error de validación, se detiene el guardado hasta corregirlo.

Los datos se almacenan en memoria, con capacidad de 10 estudiantes, y se pierden al cerrar la aplicación. Los indicadores consideran todos los registros, incluso al filtrar la tabla.

Al alcanzar el límite se desactiva **Agregar estudiante**. Eliminar un registro libera un cupo. Los códigos siguen aumentando para no repetir los de estudiantes eliminados durante la sesión.

## Ejecución

Requiere Windows y el SDK de .NET 8. Abra `GestionEstudiante.sln` en Visual Studio con la carga de trabajo **Desarrollo de escritorio con .NET** y presione **F5**, o ejecute:

```powershell
dotnet run --project GestionEstudiante.csproj
```

## Estructura

| Archivo | Responsabilidad |
| --- | --- |
| `model/Estudiante.cs` | Agrupa los datos de cada estudiante. |
| `repository/EstudianteRepository.cs` | Guarda los estudiantes en un arreglo y realiza las operaciones por identificador. Asigna el código y la fecha de creación. |
| `service/EstudianteService.cs` | Conecta el formulario con el repositorio y convierte los textos a mayúsculas antes de guardarlos. |
| `FrmGestionEstudiantes.cs` | Maneja los eventos, validaciones, registro, edición, eliminación, búsqueda e indicadores. |
| `FrmGestionEstudiantes.Designer.cs` | Define los controles, su distribución y la conexión de sus eventos. |
| `Program.cs` | Inicia la aplicación y abre el formulario principal. |

El recorrido al guardar es: **formulario → servicio → repositorio → arreglo de estudiantes**.

## Organización del código del formulario

Los eventos de los botones llaman a métodos con tareas concretas. Por ejemplo, `btnAgregar_Click` llama a `RegistrarEstudiante()`.

- `RegistrarEstudiante()`, `GuardarCambiosEstudiante()` y `EliminarEstudianteSeleccionado()` realizan las operaciones principales.
- `ValidarDatosFormulario()` revisa los campos en orden mediante `ValidarNombre()`, `ValidarCarrera()`, `ValidarCorreo()` y `ValidarTelefono()`.
- `CopiarCamposAlEstudiante()` pasa los datos de los controles al objeto. `CargarEstudianteParaEditar()` realiza el paso inverso.
- `PrepararNuevoRegistro()` cancela la selección, limpia los campos y habilita los botones correspondientes.
- `ActualizarFormulario()` prepara un nuevo registro y actualiza la tabla y los indicadores después de una operación.
- `ActualizarListadoEstudiantes()` muestra los registros que coinciden con la búsqueda.
- `ObtenerCarreraConMasEstudiantes()` utiliza ciclos y conteos para identificar la carrera más frecuente o un empate.

Los comentarios explican las decisiones y los pasos que pueden necesitar más contexto al estudiar el código.

Los prototipos y las capturas de revisión se encuentran en `doc/`, excluida de Git.
