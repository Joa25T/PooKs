using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")]
public class VoidEvent : ScriptableObject
{
    public UnityAction OnEventCall;

    public void CallEvent()
    {
        OnEventCall?.Invoke();
    }
}