using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PooKs.UI
{
    public class PartAsigner : MonoBehaviour
    {
        [SerializeReference] private SO_PartsList _partChoices;
        [SerializeField] private Slider _slider;
        [SerializeField] private SO_PartSelection _playerSelection;

        public UnityEvent OnLinkChildren;

        private void OnEnable()
        {
            StartCoroutine(SetChoices());
        }

        private IEnumerator SetChoices()
        {
            _slider.maxValue = _partChoices.partList.Count -1;
            yield return null;
            OnLinkChildren.Invoke();
        }

        public SO_Part AsignedPart()
        {
            if ((int)_slider.value > _partChoices.partList.Count - 1) return null;
            return _partChoices.partList[(int)_slider.value];
        }
    }
}