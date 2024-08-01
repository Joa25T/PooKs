using UnityEngine.EventSystems;

namespace PooKs.UI
{
    public interface IChildSelectable
    {
        void OnParentSelect(BaseEventData eventData);
        void OnParentDeselect(BaseEventData eventData);
    }
}