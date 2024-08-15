using UnityEngine;
using UnityEngine.Events;
using PooKs.UI;
public class UI_Opener : MonoBehaviour, IInteractable
{
    [SerializeField] private Panel _linkedPanel;

    public UnityEvent<Panel , SO_PlayerCharacter> OpenPanel;
    public void OnInteract(float dir, SO_PlayerCharacter playerCharacter, Transform playerPos)
    {
        OpenPanel.Invoke(_linkedPanel, playerCharacter);
    }
}
