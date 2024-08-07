using UnityEngine;

[System.Serializable]
public abstract class Part : ScriptableObject 
{
    public int IDNumber; 
    public string Name; 
    public Sprite Sprite; 
    public string Description; 
}