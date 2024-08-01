using UnityEngine;
using UnityEngine.Events;

namespace PooKs.Events
{
    public class VoidEventListener : MonoBehaviour
    {
        [Header("Listen to Event")] 
        [SerializeField] private VoidEvent EventChannel;
        [Tooltip("Actions performed with event call")]
        [SerializeField] private UnityEvent Response;

        private void OnEnable()
        {
            if (EventChannel != null)
            {
                EventChannel.OnEventCall += OnEventCall;
            }
        }

        private void OnDisable()
        {
            if (EventChannel != null)
            {
                EventChannel.OnEventCall -= OnEventCall;
            }
        }

        public void OnEventCall()
        {
            Response?.Invoke();
        }
    }
}