public class SchematicCost
{
    public int Structural;
    public int Energetic;
    public int Mechanical;
    public int Ergonomics;

    public SchematicCost(int structural, int energetic, int mechanical, int ergonomics)
    {
        Structural = structural;
        Energetic = energetic;
        Mechanical = mechanical;
        Ergonomics = ergonomics;
    }
}