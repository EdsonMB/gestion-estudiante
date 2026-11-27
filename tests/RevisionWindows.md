# Revisión de interfaz en Windows

Pendiente de ejecución visual: el desarrollo se compiló y sus reglas se probaron en Linux. Utilizar una copia de `ejemplos/`.

1. Iniciar sesión con `admin` / `987987`; cargar la copia en Archivos. Volver a Inicio: 5 estudiantes activos, 5 matrículas activas, 4 cupos y 8 registros de asistencia.
2. Abrir Estudiantes, buscar y editar. Intentar dar de baja a Ana: debe pedir cancelar su matrícula. Probar los accesos directos de Inicio.
3. Abrir Aulas, filtrar por 2026 y por nivel. Crear, editar y desactivar un aula vacía. Rechazar un duplicado y la desactivación de un aula ocupada.
4. Abrir Matrículas. Registrar en un aula con cupos; rechazar estudiante ya matriculado y aula llena. Cancelar una matrícula tras confirmar: el cupo se libera y la fila se conserva como Cancelada.
5. Abrir Asistencia, Primaria 1.º A, 05/10/2026. Cambiar un estado y una observación; guardar, salir y volver: deben conservarse. Revisar la matriz semanal.
6. Modificar asistencia y cambiar fecha, aula, módulo o cerrar. Verificar Guardar, Descartar y Cancelar. Cancelar debe conservar selección y edición. Descartar debe recuperar lo guardado.
7. Generar ambos reportes. Cambiar año, aula y fechas; comprobar el estado vacío y un rango invertido. Exportar CSV y JSON y abrirlos.
8. Crear un respaldo ZIP, modificar datos y recuperar la copia. Debe activarse otra carpeta, conservarse la anterior y actualizarse todos los módulos. Un ZIP ajeno o corrupto debe mostrar un error sin cambiar la sesión.
9. Mover la carpeta activa. Cerrar la aplicación y abrirla: debe usar la nueva ubicación.
10. Revisar foco con Tab, Enter/Escape en diálogos, redimensionado y escalas 100%/125%/150%. Comprobar que botones, mensajes y tablas sean legibles y no se superpongan.
