using GestionEstudiante.util;

namespace GestionEstudiante;

internal static class Program
{
    [STAThread]
    static int Main()
    {
        ApplicationConfiguration.Initialize();
        using Mutex instancia = new Mutex(true, Constantes.InstanciaAplicacion, out bool primera);
        if (!primera)
        {
            MessageBox.Show("La aplicación ya está abierta.", "Gestión Escolar");
            return 1;
        }

        Application.Run(new FrmAcceso());
        return 0;
    }
}
