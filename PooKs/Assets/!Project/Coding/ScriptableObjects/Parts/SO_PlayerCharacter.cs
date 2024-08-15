using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/PlayerCharacter")]
public class SO_PlayerCharacter : ScriptableObject
{
    //what information needs to be saved for the run scene
    public int PlayerID { get; private set; }
    public string PookName { get; private set; }
    public string ShipName{ get; private set; }
    
    public Part_Body ShipBody{ get; private set; }
    public Part_Weapon ShipWeapon{ get; private set; }
    
    public SO_PartsList UnlockedBodies { get; private set; }
    
    public SO_PartsList UnlockedWeapons { get; private set; }
    

    public void SaveShip(string shipName, Part_Body body, Part_Weapon weapon)
    {
        ShipName = shipName;
        ShipBody = body;
        ShipWeapon = weapon;
    }

    public void AsignPook(string pookName)
    {
        PookName = pookName;
    }

    public void SetID(int id)
    {
        PlayerID = id;
    }
}
