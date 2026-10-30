#if REDISENO_VISUAL
namespace GestionEstudiante.RedisenoVisual;

internal static class ArranqueVisual
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmAccesoVisual());
    }
}
#endif
