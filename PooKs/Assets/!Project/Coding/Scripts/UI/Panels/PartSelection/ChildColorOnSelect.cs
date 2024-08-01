using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PooKs.UI
{
    public class ChildColorOnSelect : MonoBehaviour , IChildSelectable
    {
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _targetColor;
        [SerializeField] private Image _targetImage;

        public void OnParentSelect(BaseEventData eventData)
        {
            if (_targetImage == null) return;
            _targetImage.color = _targetColor;
        }

        public void OnParentDeselect(BaseEventData eventData)
        {
            if (_targetImage == null) return;
            _targetImage.color = _normalColor;
        }
    }
}