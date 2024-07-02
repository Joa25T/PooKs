using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Costs/Building Costs")]
public class BuildingCosts : ScriptableObject
{
    public int ScrapMetals;
    public int Oxidium;
    public SchematicCost SchematicCosts;
        
    public BuildingCosts(int scrapMetals, int oxidium, SchematicCost schematicCosts)
    {
        ScrapMetals = scrapMetals;
        Oxidium = oxidium;
        SchematicCosts = schematicCosts;
    }
}
