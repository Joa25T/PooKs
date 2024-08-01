using UnityEngine;
using UnityEngine.EventSystems;

namespace PooKs.UI
{
    public class SelectLinker : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private IChildSelectable[] _childSelectable;

        public void OnSelectChildren()
        {
            _childSelectable = GetComponentsInChildren<IChildSelectable>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if(_childSelectable == null) return;
            foreach (IChildSelectable child in _childSelectable)
            {
                child.OnParentSelect(eventData);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if(_childSelectable == null) return;
            foreach (IChildSelectable child in _childSelectable)
            {
                child.OnParentDeselect(eventData);
            }
        }
    }
}