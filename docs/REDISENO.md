# Diseño integrado

Abrir GestionEstudiante.sln en Visual Studio y ejecutar el proyecto habitual.

Program.cs abre FrmAccesoVisual. Ingresar abre FrmInicioVisual; Cerrar sesión vuelve al login. El acceso es de demostración, sin validar credenciales.

Los formularios y sus archivos Designer pertenecen al único proyecto GestionEstudiante.csproj. Usan controles nativos y la estructura del prototipo de Figma.

Estudiantes reutiliza FrmGestionEstudiantes sin cambiar su lógica. La instancia se conserva al navegar; al cerrar sesión se descarta su almacenamiento en memoria. Los demás módulos son espacios pendientes y los indicadores no están conectados a datos.

Comprobar en Windows: abrir login, ingresar, navegar por todos los módulos, registrar un estudiante y verificar que persista al volver desde Inicio; probar cerrar sesión y salir.
