namespace GestionEstudiante.util;

internal class Opcion
{
    public int Id { get; }
    public string Texto { get; }

    public Opcion(int id, string texto)
    {
        Id = id;
        Texto = texto;
    }

    public override string ToString() => Texto;
}
