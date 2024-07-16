using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isLadder;
    public bool IsLadder => _isLadder;
    public void OnInteract()
    {
        Debug.Log("Shakes the ladder and makes a comment on it");
    }
}