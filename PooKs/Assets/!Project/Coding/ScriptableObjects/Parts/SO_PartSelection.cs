using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BuiltShip")]
[System.Serializable]
public class SO_PartSelection : ScriptableObject
{
    [SerializeField] private string _shipName;
    public SO_ShipBody Body;
    public SO_Weapons Weapon;
}
