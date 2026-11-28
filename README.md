# Gestión de Estudiantes

Aplicación de escritorio en C# con Windows Forms y .NET 8. La rama `completo` integra los módulos sobre el proyecto existente, a partir de `main` (`0753c0d`). Los formularios usan controles nativos de Windows Forms y conservan sus archivos `.Designer.cs`.

## Ejecutar

En Windows, abra `GestionEstudiante.sln` en Visual Studio con la carga de trabajo Desarrollo de escritorio de .NET, o ejecute:

```powershell
dotnet build GestionEstudiante.sln
dotnet run --project GestionEstudiante.csproj
```

Acceso local: usuario `admin`, contraseña `987987`. Se mantienen las constantes del proyecto. El año mostrado en Inicio se configura en `util/Constantes.cs`. Las opciones fijas de todos los combos se definen allí como listas de solo lectura. Los combos que seleccionan estudiantes o aulas se completan con los registros actuales del sistema.

## Módulos

- **Inicio:** indicadores reales de estudiantes, matrículas, cupos y asistencias; ocupación de las primeras tres aulas y accesos directos.
- **Estudiantes:** altas, edición, búsqueda y baja lógica. La baja exige cancelar primero las matrículas activas.
- **Aulas:** alta, edición, filtros por nivel y año, activación y desactivación. No se permite repetir nivel/grado/sección/año, reducir capacidad por debajo de la ocupación ni desactivar un aula ocupada. Un aula con matrículas conserva su identidad; se puede cambiar capacidad y estado.
- **Matrículas:** registro, filtros y cancelación con conservación del historial. Valida estudiante y aula activos, año, fecha y cupos. Un estudiante solo puede tener una matrícula activa por año. Cancelar libera su cupo.
- **Asistencia:** selección de aula y fecha, estados Presente/Falta/Tardanza, observación y corrección. Guardar actualiza el día sin duplicados. Seleccionar Sin registro elimina una marca anterior; una fecha sin marca no cuenta como falta. La navegación y el cierre consultan qué hacer con cambios pendientes. Incluye matriz semanal de lunes a domingo.
- **Reportes:** ocupación y asistencia por estudiante, filtros de año/aula/rango y exportación TXT con campos separados por `|`. El porcentaje es `(presentes + tardanzas) / registros`; no supone faltas en días sin registro.
- **Archivos y respaldo:** carga de otra carpeta, traslado, creación y recuperación de carpetas de respaldo con un TXT por entidad. La recuperación valida los cuatro TXT y activa una nueva carpeta, conservando la anterior.

La matrícula permite asistencia desde su fecha de registro hasta la fecha de cancelación inclusive, dentro del año del aula. Como el día de cancelación conserva su asistencia, una nueva matrícula del mismo estudiante y año debe empezar al día siguiente o después. No se admiten matrículas ni asistencias futuras. Las matrículas canceladas siguen disponibles para consultas históricas.

## Persistencia TXT

La ubicación inicial es `%LOCALAPPDATA%\GestionEstudiante\datos`. La carpeta elegida se recuerda al reiniciar. Cada operación guarda el TXT de su entidad mediante un archivo temporal y después publica los datos en memoria. Si falla la escritura, la sesión conserva los registros anteriores. Si la carpeta activa desaparece, el guardado se bloquea en lugar de recrear un conjunto incompleto; todavía puede crear un respaldo de los datos de la sesión.

Los archivos de entidades usan UTF-8, sin cabecera, un registro por línea y `|` como separador. Los reportes exportados usan el mismo formato e incluyen una primera fila con los nombres de las columnas:

| Archivo | Campos, en orden |
| --- | --- |
| `estudiantes.txt` | id, codigo, nombre, apellidos, apoderado, telefono, correo, fechaCreacion, activo |
| `aulas.txt` | id, nivel, grado, seccion, anio, capacidad, activo |
| `matriculas.txt` | id, estudianteId, aulaId, fecha, fechaCancelacion |
| `asistencias.txt` | id, matriculaId, fecha, estado, observacion |

Fechas: ISO 8601, formato `O`. Booleanos: `true` o `false`. `fechaCancelacion` vacía significa matrícula activa. Los escapes reversibles son `\\` para barra inversa, `\p` para `|`, `\r` y `\n` para saltos de línea. No quite las posiciones de campos vacíos.

Aulas admite Inicial (3–5 años), Primaria (1–6) y Secundaria (1–5), años 2000–2100 y capacidad 1–1000. Las observaciones de asistencia admiten 250 caracteres.

Las carpetas antiguas con solo `estudiantes.txt` siguen siendo válidas; los demás conjuntos comienzan vacíos. La carga rechaza identificadores repetidos, referencias inexistentes, matrículas con periodos superpuestos y datos inconsistentes. `catalogos.txt` se conserva como referencia del ejemplo; los valores admitidos se validan en el código y no se cargan desde ese archivo.

Estudiantes mantiene su regla anterior de id = cantidad de registros + 1, con comprobación de colisiones. Las entidades nuevas utilizan máximo id + 1. No se eliminan físicamente estudiantes, aulas ni matrículas.

## Organización y POO

- **Herencia:** `Estudiante`, `Aula`, `Matricula` y `Asistencia` derivan de `ModeloBase`.
- **Abstracción:** `ModeloBase.ObtenerCampos()` define el contrato de serialización y `Reporte.Generar()` el de los reportes.
- **Polimorfismo:** el repositorio genérico serializa las distintas entidades; `FrmReportes` utiliza `ReporteOcupacion` y `ReporteAsistencia` a través de `Reporte`.
- **Encapsulación:** los servicios contienen las reglas del módulo y mantienen privados sus repositorios. El acceso a archivos queda en `archivos/`, separado de los formularios.

`GestorBaseDatos` se comparte durante la sesión. Los servicios consultan sus datos vigentes, por lo que cargar o recuperar otra carpeta actualiza todos los módulos al volver a abrirlos.

## Datos de ejemplo

`ejemplos/` contiene datos ficticios: seis estudiantes, cuatro aulas, seis matrículas (una cancelada) y ocho asistencias. Copie esa carpeta fuera del repositorio y selecciónela en **Archivos → Cargar / usar** para no modificar el ejemplo. En Asistencia, consulte el 5 o 6 de octubre de 2026.

## Validación

Para compilar desde Linux con el SDK .NET 8:

```sh
dotnet build GestionEstudiante.sln -p:EnableWindowsTargeting=true
```
