using UnityEngine;
using UnityEngine.Events;

namespace PooKs.UI
{
    public abstract class Panel : MonoBehaviour
    {
        public UnityEvent OnUiOpen;

        public virtual void OnOpen()
        {
            OnUiOpen?.Invoke();
        }

        public virtual void OnClose()
        {

        }
    }
}