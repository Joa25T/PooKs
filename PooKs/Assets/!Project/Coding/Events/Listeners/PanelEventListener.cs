using PooKs.UI;
using UnityEngine;
using UnityEngine.Events;

public class PanelEventListener : MonoBehaviour
{
    [Header("Listen to Event")] [SerializeField]
    protected PanelEvent EventChannel;

    [Tooltip("Actions performed with event call")] [SerializeField]
    protected UnityEvent<Panel ,SO_PlayerCharacter> Response;

    protected virtual void OnEnable()
    {
        if (EventChannel != null)
        {
            EventChannel.OnEventCall += OnEventCall;
        }
    }

    protected virtual void OnDisable()
    {
        if (EventChannel != null)
        {
            EventChannel.OnEventCall -= OnEventCall;
        }
    }

    public void OnEventCall(Panel panel, SO_PlayerCharacter playerCharacter)
    {
        Response?.Invoke(panel, playerCharacter);
    }
}

