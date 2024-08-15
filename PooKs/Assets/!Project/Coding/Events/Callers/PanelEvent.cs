using UnityEngine;
using PooKs.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Panel Event", menuName = "Events/Panel Event")]
public class PanelEvent : ScriptableObject
{
    public UnityAction<Panel, SO_PlayerCharacter> OnEventCall;
    
    public void CallEvent(Panel panel, SO_PlayerCharacter playerCharacter)
    {
        OnEventCall?.Invoke(panel, playerCharacter);
    }
}