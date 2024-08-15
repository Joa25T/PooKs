using UnityEngine;
using UnityEngine.Events;

namespace PooKs.UI
{
    public abstract class Panel : MonoBehaviour
    {
        public UnityEvent OnUiOpen;
        [SerializeField][ReadOnlyInspector] protected SO_PlayerCharacter _interactingPC;

        public virtual void OnOpen(SO_PlayerCharacter interactingPC)
        {
            OnUiOpen?.Invoke();
            _interactingPC = interactingPC;
        }

        public virtual void OnClose()
        {
            _interactingPC = null;
        }
    }
}