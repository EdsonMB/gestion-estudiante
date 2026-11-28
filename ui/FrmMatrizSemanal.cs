using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmMatrizSemanal : Form
{
    public FrmMatrizSemanal() { InitializeComponent(); }
    public FrmMatrizSemanal(Aula aula, DateTime fecha, EstudianteService estudiantes,
        MatriculaService matriculas, AsistenciaService asistenciaService) : this()
    {
        DateTime lunes = fecha.Date.AddDays(-((7 + (int)fecha.DayOfWeek - 1) % 7));
        lblTitulo.Text = aula.Descripcion + " · Semana del " + lunes.ToString("dd/MM/yyyy");
        dgvMatriz.Columns.Add("estudiante", "Estudiante");
        for (int dia = 0; dia < 7; dia++)
        {
            dgvMatriz.Columns.Add("dia" + dia, lunes.AddDays(dia).ToString("ddd dd/MM"));
        }

        List<Asistencia> asistencias = asistenciaService.Listar();
        foreach (Matricula matricula in matriculas.Listar())
        {
            if (matricula.aulaId != aula.id)
            {
                continue;
            }

            object[] valores = new object[8];
            bool vigenteEnSemana = false;
            for (int dia = 0; dia < 7; dia++)
            {
                DateTime actual = lunes.AddDays(dia);
                if (matricula.VigenteEn(actual))
                {
                    vigenteEnSemana = true;
                    valores[dia + 1] = BuscarEstado(asistencias, matricula.id, actual);
                }
                else
                {
                    valores[dia + 1] = "—";
                }
            }

            if (vigenteEnSemana)
            {
                valores[0] = estudiantes.NombreEstudiante(matricula.estudianteId) + " (" + matricula.Codigo + ")";
                dgvMatriz.Rows.Add(valores);
            }
        }
    }

    private static string BuscarEstado(List<Asistencia> asistencias, int matriculaId, DateTime fecha)
    {
        foreach (Asistencia asistencia in asistencias)
        {
            if (asistencia.matriculaId == matriculaId && asistencia.fecha.Date == fecha)
            {
                return asistencia.estado;
            }
        }

        return Constantes.AsistenciaSinRegistro;
    }
}
