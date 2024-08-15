using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Part List")] [System.Serializable]
public class SO_PartsList : ScriptableObject
{
    public List<SO_Part> partList;

    [SerializeField]private PartType _partType;

    private void AddPart(PartType type, SO_Part soPart)
    {
        if(type != _partType) return;
        partList.Add(soPart);
    }
}