# Rediseño visual de Gestión Escolar

## Análisis y plan aplicado

Referencia estructural: https://www.figma.com/design/GUKACLnLgxSLv1pIAvDgEP/

1. Partir de main y agregar archivos en RedisenoVisual, sin editar archivos originales.
2. Agregar acceso visual con usuario, contraseña e ingreso de demostración.
3. Organizar Inicio como Figma: menú lateral, encabezado, cuatro indicadores, ocupación por aula, acciones frecuentes e información del archivo.
4. Usar controles nativos: Panel, Label, TextBox, Button, GroupBox y ProgressBar. Distribución mediante Location, Size y Anchor en archivos Designer, como en el diseñador de Visual Studio.
5. Reutilizar FrmGestionEstudiantes sin cambiar sus controles, validaciones ni almacenamiento. Reservar Aulas, Matrículas, Asistencia, Reportes y Archivos y respaldo para el equipo.

## Abrir y ejecutar

En Windows con Visual Studio 2022 y la carga de trabajo Desarrollo de escritorio de .NET, abrir **GestionEstudiante.Rediseno.csproj**, dentro de esta carpeta. No abrir la solución original para probar este arranque.

También se puede ejecutar desde la raíz del repositorio:

```powershell
dotnet run --project RedisenoVisual/GestionEstudiante.Rediseno.csproj
```

El proyecto original conserva su arranque y todos sus archivos. El proyecto de diseño incluye los archivos originales por referencia de compilación y usa su propio punto de entrada, habilitado únicamente con REDISENO_VISUAL. No hay paquetes externos.

## Alcance

- Login: solo visual, sin autenticación. Ingresar abre Inicio; Cerrar sesión vuelve al acceso.
- Estudiantes: el formulario original se conserva mientras se navega. Cerrar sesión cierra esa instancia y su información en memoria, igual que cerrar el programa original.
- Indicadores: muestran un guion y las barras permanecen vacías; todavía no tienen conexión a datos.
- Los nombres de aulas ilustran la distribución del prototipo; no son aulas cargadas desde el sistema.
- Los demás módulos muestran únicamente su espacio de desarrollo.
- La apariencia usa los controles estándar de Windows; Figma se usa para la organización, sin reproducir bordes personalizados, iconos o efectos.

## Verificación manual en Windows

1. Ejecutar el proyecto de diseño e ingresar con los campos vacíos: debe abrir Inicio por ser una demostración.
2. Revisar login, menú e Inicio al tamaño mínimo y con escalado de Windows.
3. Abrir Estudiantes, registrar un estudiante, cambiar a Inicio y volver: el registro debe conservarse.
4. Probar búsqueda, modificación y eliminación en el formulario original.
5. Abrir todos los módulos y verificar su título. Probar las tres acciones frecuentes y Nueva matrícula.
6. Cerrar sesión y volver a ingresar; cerrar el acceso debe terminar el programa.
7. Abrir la solución original: debe conservar el arranque anterior.
