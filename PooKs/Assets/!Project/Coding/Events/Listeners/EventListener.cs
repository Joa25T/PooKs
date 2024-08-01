using UnityEngine;
using UnityEngine.Events;

namespace PooKs.Events
{
    public abstract class EventListener<TEvent, TEventType> : MonoBehaviour where TEvent : GameEvents<TEventType>
    {
        [Header("Listen to Event")] [SerializeField]
        protected TEvent EventChannel;

        [Tooltip("Actions performed with event call")] [SerializeField]
        protected UnityEvent<TEventType> Response;

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

        public void OnEventCall(TEventType evt)
        {
            Response?.Invoke(evt);
        }
    }
}