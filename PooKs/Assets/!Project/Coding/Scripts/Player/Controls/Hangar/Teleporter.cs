using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [Tooltip("List teleporters form bottom to top")]
    [SerializeField] private List<Teleporter> _connectedTeleporters;
    [Tooltip("The teleporter ID should match the objects number on the list, remember we start at 0")]
    [SerializeField] private int _teleporterID;
    public void OnInteract(float direction, GameObject caller)
    {
        if (_connectedTeleporters.Count<1) 
        {
            Debug.LogWarning("No connected teleporter"); // if there will be unconnected teleporters we could add a line for the PooKs here
            return;
        }
        direction = direction == 0 ? 1 : direction;
        int targetTeleporter = _teleporterID + (1 * (int)direction);
        targetTeleporter = targetTeleporter > (_connectedTeleporters.Count-1) ? 0 : targetTeleporter;
        caller.transform.position = _connectedTeleporters[targetTeleporter].transform.position;
    }
}