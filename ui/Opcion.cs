namespace GestionEstudiante;

internal record Opcion(int Id, string Texto)
{
    public override string ToString() => Texto;
}
