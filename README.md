# Gestión de Estudiantes

Aplicación de escritorio en C# con Windows Forms y .NET 8. Abra GestionEstudiante.sln en Visual Studio con desarrollo de escritorio de .NET o, desde esta carpeta, ejecute:

~~~powershell
dotnet build GestionEstudiante.sln
dotnet run --project GestionEstudiante.csproj
~~~

Ingrese con usuario admin y contraseña 987987. La validación usa las constantes de la aplicación y es independiente de los registros escolares.

## Funciones disponibles

- Inicio muestra la cantidad de estudiantes activos y permite navegar a los módulos.
- Estudiantes permite registrar, editar, buscar por código, nombres o apellidos y filtrar por estado. Dar de baja cambia el estado a inactivo y conserva el registro. Al editar se conservan el código, la fecha y el estado.
- Archivos permite cargar y usar una carpeta de datos. La carpeta activa se recuerda al reiniciar.
- Matrículas incluye el listado y el formulario modal Nueva matrícula. El servicio, los modelos y la conexión con datos quedan pendientes de integración.
- Asistencia permite marcar y corregir la asistencia de los estudiantes activos por fecha. Guarda los cambios en asistencias.txt. El filtro por aula y la matriz semanal están pendientes.
- Aulas y Reportes están pendientes de integración. También quedan pendientes la persistencia de Matrículas y la consulta de matrícula activa al dar de baja a un estudiante.

## Archivos de datos

La ubicación inicial es %LOCALAPPDATA%\GestionEstudiante\datos. Si la carpeta aún no existe, la sesión comienza sin registros. Puede seleccionar una copia de los datos entregados en Archivos. Los TXT se guardan en UTF-8, sin cabecera, con un registro por línea y campos separados por barra vertical.

| Archivo | Campos |
| --- | --- |
| estudiantes.txt | id, codigo, nombre, apellidos, apoderado, telefono, correo, fechaCreacion, activo |
| catalogos.txt | nombre de catálogo, valor |
| asistencias.txt | id, codigoEstudiante, fecha (yyyy-MM-dd), estado |
| aulas.txt, matriculas.txt | Archivos vacíos que acompañan al ejemplo. Los módulos correspondientes están pendientes de integración. |

Las fechas de estudiantes se escriben en ISO 8601 con formato O. El estado es true o false. Un correo vacío conserva su posición. Los escapes son \\ para barra inversa, \p para el separador, \r para retorno de carro y \n para salto de línea. El id de un alta es la cantidad total de registros de su entidad más uno. En Estudiantes se cuentan los activos y los inactivos porque la baja conserva la fila. Si el id o el código calculado ya existe en un conjunto importado, el alta se detiene y conserva el archivo.

## Datos de la entrega

El conjunto de ejemplo está en ejemplos/. La carpeta ../datos contiene una copia del conjunto entregado. Para probarlo sin modificarlo, copie esa carpeta y seleccione la copia desde Archivos.

## Organización

model contiene los modelos de estudiantes y su contador. repository administra el estado activo y las operaciones sobre el arreglo. service valida las entradas y conecta las operaciones. archivos gestiona los TXT con LeerArchivo<T> y Guardar<T>. El repositorio indica el TXT de su entidad, sus registros y el orden de los campos. El guardado actualiza solo ese TXT. ui conserva los formularios de acceso, navegación, estudiantes y archivos. util reúne las constantes.
