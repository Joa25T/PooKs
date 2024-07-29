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

[CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")]
public class Event : ScriptableObject
{
    public UnityAction OnEventCall;

    public void CallEvent()
    {
        OnEventCall?.Invoke();
    }
}

[CreateAssetMenu(fileName = "Float Event", menuName = "Events/Float Event")]
public class FloatEvent : GameEvents<float> { }

[CreateAssetMenu(fileName = "Bool Event", menuName = "Events/Bool Event")]
public class BoolEvent : GameEvents<bool> { }

[CreateAssetMenu(fileName = "Vector3 Event", menuName = "Events/Vector3 Event")]
public class Vector3Event : GameEvents<Vector3> { }

[CreateAssetMenu(fileName = "Panel Event", menuName = "Events/Panel Event")]
public class PanelEvent : GameEvents<Panel> { }

[CreateAssetMenu(fileName = "GameObject Event", menuName = "Events/GameObject Event")]
public class GameObjectEvent : GameEvents<GameObject> { }
