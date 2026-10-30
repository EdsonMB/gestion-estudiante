namespace GestionEstudiante;

internal static class Program
{
    // Prepara Windows Forms y mantiene la aplicación abierta mientras exista el formulario principal.
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmAccesoVisual());
    }
}
