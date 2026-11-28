namespace GestionEstudiante.util;

// Formato compartido por los archivos de datos y reportes.
public static class FormatoTxt
{
    public static string CrearFila(params string[] campos)
    {
        string[] codificados = new string[campos.Length];
        for (int i = 0; i < campos.Length; i++)
        {
            codificados[i] = campos[i].Replace("\\", "\\\\").Replace(Constantes.Separador.ToString(), "\\p")
                .Replace("\r", "\\r").Replace("\n", "\\n");
        }

        return string.Join(Constantes.Separador, codificados);
    }
}
