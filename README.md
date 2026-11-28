# Gestión de Estudiantes

Aplicación de escritorio en C# con Windows Forms y .NET 8. La rama `completo` integra los módulos sobre el proyecto existente e incorpora los cambios de Asistencia de `main`. Los formularios usan controles nativos de Windows Forms y conservan sus archivos `.Designer.cs`.

## Ejecutar

En Windows, abra `GestionEstudiante.sln` en Visual Studio con la carga de trabajo Desarrollo de escritorio de .NET, o ejecute:

```powershell
dotnet build GestionEstudiante.sln
dotnet run --project GestionEstudiante.csproj
```

Acceso local: usuario `admin`, contraseña `987987`. El año mostrado en Inicio y los textos fijos de los combos se definen en `util/Constantes.cs`. Cada formulario agrega a sus combos las constantes que usa y carga los estudiantes o aulas a partir de los registros actuales del sistema.

## Módulos

- **Inicio:** indicadores reales de estudiantes, matrículas, cupos y asistencias; ocupación de las primeras tres aulas y accesos directos.
- **Estudiantes:** altas, edición, búsqueda y baja lógica. La baja exige cancelar primero las matrículas activas.
- **Aulas:** alta, edición, filtros por nivel y año, activación y desactivación. No se permite repetir nivel/grado/sección/año, reducir capacidad por debajo de la ocupación ni desactivar un aula ocupada. Un aula con matrículas conserva su identidad; se puede cambiar capacidad y estado.
- **Matrículas:** registro, filtros y cancelación con conservación del historial. Valida estudiante y aula activos, año, fecha y cupos. Un estudiante solo puede tener una matrícula activa por año. Cancelar libera su cupo.
- **Asistencia:** selección de aula y fecha, botón **Marcar** para alternar entre Presente, Falta y Tardanza, y observación por matrícula. **Guardar asistencia** se activa cuando cambia una fila. Seleccionar **Sin registro** retira una marca anterior; una fecha sin marca no cuenta como falta. La navegación y el cierre consultan qué hacer con cambios pendientes. Incluye matriz semanal de lunes a domingo.
- **Reportes:** ocupación y asistencia por estudiante, filtros de año/aula/rango y exportación TXT con campos separados por `|`. El porcentaje es `(presentes + tardanzas) / registros`; no supone faltas en días sin registro.
- **Archivos:** consulta de la carpeta activa y selección de la carpeta que usarán todos los módulos.

La matrícula permite asistencia desde su fecha de registro hasta la fecha de cancelación inclusive, dentro del año del aula. Como el día de cancelación conserva su asistencia, una nueva matrícula del mismo estudiante y año debe empezar al día siguiente o después. No se admiten matrículas ni asistencias futuras. Las matrículas canceladas siguen disponibles para consultas históricas.

## Persistencia TXT

Después de iniciar sesión por primera vez, seleccione la carpeta de datos. La ruta se recuerda al reiniciar en `%LOCALAPPDATA%\GestionEstudiante\ubicacion-db.txt`. Cada operación guarda el TXT de su entidad mediante un archivo temporal y después publica los datos en memoria. Si la carpeta activa desaparece o falla la escritura, la sesión conserva los registros anteriores.

Los archivos de entidades usan UTF-8, sin cabecera, un registro por línea y `|` como separador. Los reportes exportados usan el mismo formato e incluyen una primera fila con los nombres de las columnas:

| Archivo | Campos, en orden |
| --- | --- |
| `estudiantes.txt` | id, codigo, nombre, apellidos, apoderado, telefono, correo, fechaCreacion, activo |
| `aulas.txt` | id, nivel, grado, seccion, anio, capacidad, activo |
| `matriculas.txt` | id, estudianteId, aulaId, fecha, fechaCancelacion |
| `asistencias.txt` | id, matriculaId, fecha, estado, observacion |

El campo `estado` de `asistencias.txt` admite los textos `Presente`, `Falta` y `Tardanza`, definidos en `util/Constantes.cs`.

Fechas: ISO 8601, formato `O`. Booleanos: `true` o `false`. `fechaCancelacion` vacía significa matrícula activa. Los escapes reversibles son `\\` para barra inversa, `\p` para `|`, `\r` y `\n` para saltos de línea. No quite las posiciones de campos vacíos.

Aulas admite Inicial (3–5 años), Primaria (1–6) y Secundaria (1–5), años 2000–2100 y capacidad 1–1000. Las observaciones de asistencia admiten 250 caracteres.

Al seleccionar una carpeta vacía, el sistema crea los cuatro TXT de la tabla. Las carpetas con datos deben contener los cuatro archivos. Un TXT sin filas representa una entidad sin registros. La carga rechaza identificadores repetidos, referencias inexistentes, matrículas con periodos superpuestos y datos inconsistentes. `catalogos.txt` se conserva como referencia del ejemplo; los valores admitidos se validan en el código.

Los repositorios calculan los identificadores a partir de la cantidad de registros más uno. Estudiantes, Aulas y Matrículas rechazan una colisión en el alta. Si una marca de Asistencia se retiró y el identificador calculado ya está ocupado, se busca el siguiente disponible. Las correcciones conservan el identificador.

## Organización y POO

- **Herencia:** `Estudiante`, `Aula`, `Matricula` y `Asistencia` derivan de `ModeloBase`.
- **Abstracción:** `ModeloBase.ObtenerCampos()` define el contrato de serialización y `Reporte.Generar()` el de los reportes.
- **Polimorfismo:** el guardado genérico usa `ObtenerCampos()` para serializar las entidades; `FrmReportes` utiliza `ReporteOcupacion` y `ReporteAsistencia` a través de `Reporte`.
- **Encapsulación:** como en `feature/asistencia`, cada módulo recibe sus servicios y cada entidad persistida tiene un repositorio concreto. Los servicios validan datos y los repositorios guardan el TXT de su entidad mediante `GestorBaseDatos.Guardar<T>`. El acceso a archivos queda en `archivos/`.

`GestorBaseDatos` se comparte durante la sesión. Los servicios consultan sus datos vigentes, por lo que cargar otra carpeta actualiza todos los módulos al volver a abrirlos.

## Datos de ejemplo

`ejemplos/` contiene datos ficticios: seis estudiantes, cuatro aulas, seis matrículas (una cancelada) y ocho asistencias. Copie esa carpeta fuera del repositorio y selecciónela al entrar o desde **Archivos → Seleccionar carpeta**. En Asistencia, consulte el 5 o 6 de octubre de 2026.

## Validación

Para compilar desde Linux con el SDK .NET 8:

```sh
dotnet build GestionEstudiante.sln -p:EnableWindowsTargeting=true
```
