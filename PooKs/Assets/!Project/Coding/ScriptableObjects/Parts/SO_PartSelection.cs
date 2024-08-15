using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Scriptable/BuiltShip")]
[System.Serializable]
public class SO_PartSelection : ScriptableObject
{
    //what information needs to be saved for the run scene
    [SerializeField] public string ShipName;
    [ReadOnlyInspector] public int PlayerID;
    public Part_Body Body;
    [FormerlySerializedAs("Weapon")] public Part_Weapon Weapon;

    public SO_PartSelection(string shipName, int playerID, Part_Body body, Part_Weapon weapon)
    {
        ShipName = shipName;
        PlayerID = playerID;
        Body = body;
        Weapon = weapon;
    }
}
