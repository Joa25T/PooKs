using UnityEngine;
using UnityEngine.Events;

public class UI_Opener : MonoBehaviour, IInteractable
{
    [SerializeField] private Panel _linkedPanel;

    public UnityEvent<Panel> OpenPanel;
    public void OnInteract(float dir, GameObject caller)
    {
        OpenPanel.Invoke(_linkedPanel);
    }
}
