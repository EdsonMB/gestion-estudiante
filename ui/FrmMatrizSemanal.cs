using GestionEstudiante.util;
using GestionEstudiante.model;
using GestionEstudiante.service;
namespace GestionEstudiante;

public partial class FrmMatrizSemanal : Form
{
    public FrmMatrizSemanal() { InitializeComponent(); }
    public FrmMatrizSemanal(EscolarService servicio, int aulaId, DateTime fecha) : this()
    {
        DateTime lunes = fecha.Date.AddDays(-((7 + (int)fecha.DayOfWeek - 1) % 7));
        lblTitulo.Text = servicio.Aulas.Listar().Single(x => x.id == aulaId).Descripcion + " · Semana del " + lunes.ToString("dd/MM/yyyy");
        dgvMatriz.Columns.Add("estudiante", "Estudiante");
        for (int dia = 0; dia < 7; dia++) dgvMatriz.Columns.Add("dia" + dia, lunes.AddDays(dia).ToString("ddd dd/MM"));
        List<Asistencia> asistencias = servicio.Asistencias.Listar();
        foreach (Matricula matricula in servicio.Matriculas.Listar().Where(x => x.aulaId == aulaId &&
            Enumerable.Range(0, 7).Any(dia => x.VigenteEn(lunes.AddDays(dia)))))
        {
            object[] valores = new object[8];
            valores[0] = servicio.NombreEstudiante(matricula.estudianteId) + " (" + matricula.Codigo + ")";
            for (int dia = 0; dia < 7; dia++)
            {
                DateTime actual = lunes.AddDays(dia);
                Asistencia? asistencia = asistencias.SingleOrDefault(x => x.matriculaId == matricula.id && x.fecha.Date == actual);
                valores[dia + 1] = asistencia?.estado.ToString() ?? (matricula.VigenteEn(actual) ? Constantes.AsistenciaSinRegistro : "—");
            }
            dgvMatriz.Rows.Add(valores);
        }
    }
}
