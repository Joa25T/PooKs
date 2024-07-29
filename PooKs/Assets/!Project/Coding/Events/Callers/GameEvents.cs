using UnityEngine;
using UnityEngine.Events;

public abstract class GameEvents<T> : ScriptableObject
{
    public UnityAction<T> OnEventCall;

    public void CallEvent(T parameter)
    {
        OnEventCall?.Invoke(parameter);
    }
}