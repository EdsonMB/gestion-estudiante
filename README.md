# Gestión de Estudiantes

Aplicación educativa de C# con Windows Forms y .NET 8. Usa clases sencillas con constructores tradicionales, arreglos, condiciones, ciclos y métodos con llaves. Los objetos se crean con `new Clase()` y sus propiedades se asignan por separado. Los `if` y `else` llevan llaves. Las respuestas de mensajes y diálogos se guardan en variables antes de evaluarlas en una condición. No usa funciones flecha, LINQ ni estructuras de datos avanzadas. `System.Text.Json` se utiliza únicamente para convertir los datos del archivo.

## Alcance actual

Solo se implementan estudiantes y manejo de archivos según los prototipos:

- Registrar y editar nombres, apellidos, nombre del apoderado, teléfono de nueve dígitos y correo opcional.
- Buscar por código, nombres y apellidos sin distinguir mayúsculas, mediante el cuadro de búsqueda, el botón Buscar o Enter; filtrar activos e inactivos.
- Dar de baja con confirmación, conservando el registro.
- Asignar código y fecha automáticamente y conservarlos al editar.
- Guardar al terminar una operación, cargar y usar un JSON, mover el archivo activo, crear una copia y recuperarla con confirmación.
- Leer el JSON sin revisar los campos de los estudiantes al cargar. Los errores de lectura o de formato JSON se muestran desde el formulario.

No hay un máximo fijo de estudiantes. El arreglo aumenta al registrar. Las bajas no borran datos y los códigos no se reutilizan.

Aulas, matrículas, asistencia y reportes conservan sus espacios reservados en el menú. No tienen modelos propios, repositorios, servicios ni operaciones. La comprobación de matrícula activa antes de una baja está marcada como TODO en `EstudianteService.DarDeBaja`: corresponde a la futura integración. El acceso sigue siendo de demostración.

## Uso del módulo de estudiantes

1. Desde Inicio, seleccione Estudiantes o Registrar estudiante.
2. Pulse Nuevo estudiante, complete nombres, apellidos, apoderado y teléfono; el correo es opcional. Guardar estudiante registra los datos en el JSON.
3. Escriba una búsqueda o elija el estado. Editar abre los datos de esa fila; Guardar cambios conserva el código, la fecha y el estado. Cancelar cierra sin guardar.
4. Dar de baja pide confirmación y cambia el estado a inactivo. El estudiante permanece disponible en el filtro Inactivos.
5. Si una validación o la escritura falla, el formulario permanece abierto y conserva los campos para corregir o reintentar.

## Organización

| Carpeta | Responsabilidad |
| --- | --- |
| `archivos` | Abrir, leer y escribir el JSON con StreamReader y StreamWriter. |
| `model` | Estudiante y clases sencillas para la estructura del archivo. |
| `repository` | Recorrer y actualizar el arreglo, asignar códigos y pedir el guardado. |
| `service` | EstudianteService gestiona estudiantes; ArchivoService gestiona carga, movimiento, respaldo y recuperación. |
| `ui` | Formularios con sus archivos .cs, .Designer.cs y .resx. |
| `util` | Constantes de versión, nombre del centro y año escolar. |

`FrmGestionEstudiantes` y `FrmEstudiante` usan `EstudianteService`. `FrmArchivos` usa únicamente `ArchivoService`.

`FrmAcceso` crea una sola instancia de `GestorBaseDatos` al ingresar por primera vez y la conserva al cerrar sesión. Ese gestor reúne la ruta activa, los datos cargados y las operaciones generales de archivo. El repositorio de estudiantes y el servicio de archivos reciben esa misma instancia mediante sus constructores.

`EstudianteRepository` se ocupa solamente de consultar y guardar estudiantes a través del gestor. `ArchivoService` solicita al gestor cargar, mover, respaldar y recuperar. Así, cambiar de archivo actualiza los datos que ven las pantallas y los repositorios, sin crear otra base en memoria. Los demás módulos siguen pendientes.

Las validaciones de los campos están en `EstudianteService` y se aplican al registrar o editar. La carga del archivo y el inicio no revisan esos campos. La estructura e identificadores del JSON se revisan en métodos privados de `ArchivoBaseDatos`. No hay clases adicionales dedicadas a validaciones.

Los controles se declaran y configuran explícitamente en `InitializeComponent()`, siguiendo el patrón del diseñador de Visual Studio. La lógica de eventos queda en el `.cs`. Los formularios se llaman FrmAcceso, FrmInicio, FrmGestionEstudiantes, FrmEstudiante y FrmArchivos.

## Un solo archivo de datos

La carpeta predeterminada se define en `Constantes.CarpetaDatos`: `C:/gestion-estudiante`. El archivo inicial es `C:/gestion-estudiante/gestion-escolar.json`. La pantalla Archivos muestra la ruta activa. Al cargar o mover el archivo, la ubicación se recuerda en `C:/gestion-estudiante/ubicacion.txt` para el siguiente inicio; ese texto solo contiene la ruta, no los datos de los estudiantes. La carpeta se crea al guardar si todavía no existe. El archivo conserva todas sus secciones; el programa solo modifica `estudiantes` y `secuencias.siguienteIdEstudiante`.

El modelo contiene solamente `secuencias.siguienteIdEstudiante` y el arreglo `estudiantes`, con propiedades normales y constructores clásicos. No hay atributos de serialización ni propiedades para los módulos pendientes. Las comprobaciones se realizan con condiciones y ciclos dentro de las clases existentes.

Los datos de aulas, matrículas, asistencias, catálogos y los demás contadores se copian tal como están escritos al guardar. Se conservan también los campos adicionales. No hay modelos ni lógica para esos módulos: la capa de archivos recorre las secciones del JSON con un ciclo y copia su texto. Los modelos siguen representando solamente estudiantes y su contador.

Las opciones de Archivos funcionan así:

- **Cargar / usar:** lee y carga el JSON elegido y lo utiliza para los siguientes guardados. Conserva el archivo que se usaba antes. La nueva ubicación se recuerda al reiniciar.
- **Mover archivo:** permite elegir una carpeta y traslada allí el archivo activo completo. Mantiene su nombre y continúa guardando en la nueva ubicación. No reemplaza otro archivo que ya exista en la carpeta elegida.
- **Crear copia:** incluye todo el contenido y mantiene el archivo activo.
- **Recuperar copia:** lee la copia y reemplaza el contenido del archivo activo después de confirmar. Mantiene la ubicación activa.

Cancelar el diálogo no cambia la base ni su ubicación. Por ahora no se validan los datos al cargar o recuperar; el archivo debe poder convertirse al modelo JSON.

Los estudiantes usan `nombre`, `apellidos`, `apoderado`, `telefono` y `correo`; estos últimos son los contactos del apoderado. Se quitó `carrera`. No se limita la cantidad de registros. La versión, el nombre del centro y el año escolar son constantes en `util/Constantes.cs`; no se guardan ni se configuran desde el JSON. Si un archivo ya incluye `version` y `configuracion`, se conservan como parte de su contenido; la interfaz sigue utilizando las constantes. El ejemplo de la carpeta `ejemplos` puede cargarse desde Archivos; no se carga automáticamente.

Al guardar se escribe primero un temporal y luego se reemplaza el archivo elegido. La sesión se actualiza después de que termine el guardado. Las copias de seguridad se crean manualmente desde el botón Crear copia. Cargar o recuperar requiere la confirmación del usuario antes de cambiar la sesión.

Se utiliza un único modelo de archivo: `BaseDatosEscolar`. `Estudiante` representa a una persona y `Secuencias` guarda el próximo identificador. El formato antiguo `GestionEstudiante-1` ya no se utiliza; los archivos deben contener `secuencias` y `estudiantes`.

## Documentación del examen final revisada

Se revisaron el sílabo, la rúbrica EF, el esquema del informe, la guía del código anterior y los entregables: informe base, planificación por módulos en Excel, prototipos PDF de estudiantes, aulas, matrículas, asistencia, reportes y archivos, y HTML de acceso.

Para esta etapa se aplican los temas del sílabo: arreglos unidimensionales, búsquedas por recorrido, cadenas, funciones, clases simples y lectura/escritura de archivos. La rúbrica pide explicar el algoritmo mediante pseudocódigo o diagramas y demostrar casos correctos e incorrectos; las pruebas del proyecto verifican este alcance.

El informe base y el Excel incluyen requerimientos del sistema completo que quedan pendientes: relaciones entre matrículas y asistencias, cupos por aula, matrices, arreglos paralelos, ordenamiento y recursividad. No se agregan como parte de esta adaptación.

Hay diferencias entre las fuentes: el Excel propone `versionEsquema`, `siguientesIds`, campos como `nombres` y `nombreApoderado`, y fecha DateOnly; el esquema adoptado en esta conversación usa `secuencias`, los campos actuales de estudiante y fecha de creación con hora. Se conserva el acuerdo de esta conversación y se deberá unificar el contrato al integrar los otros módulos. La guía antigua describe diez alumnos en memoria y carrera; esas reglas ya fueron sustituidas.

El informe final todavía debe actualizarse con resultados, capturas de funcionamiento, pseudocódigo y aportes reales de cada integrante. Los prototipos y documentos de planificación no se presentan como módulos ya implementados.

## Ejecución y comprobación

Abra `GestionEstudiante.sln` en Visual Studio con Desarrollo de escritorio con .NET, o ejecute:

```powershell
dotnet run --project GestionEstudiante.csproj
```

Para compilar y ejecutar las comprobaciones sin paquetes externos:

```powershell
dotnet build
dotnet run --project tests/Verificacion/Verificacion.csproj
```

Se comprueban registro, edición, baja, reinicio, copia, recuperación, manejo de archivos que no se pueden leer como JSON, carga sin validación de datos, conservación de las secciones de módulos pendientes, fallo de escritura, registro sin el límite de diez estudiantes, cambio de archivo activo, movimiento y ubicación recordada al reiniciar. Las pruebas generan datos temporales en `tmp` y no usan el archivo principal del usuario.
