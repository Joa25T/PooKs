using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PartManager : MonoBehaviour
{
    [SerializeReference] private List<Part> _partChoices;
    [SerializeField] private Slider _slider;

    public UnityEvent<List<Part>> OnSetChoices;
    public UnityEvent<int> OnSetGraphics;
    public UnityEvent OnLinkChildren;

    private void OnEnable()
    {
        StartCoroutine(SetChoices());
    }

    private IEnumerator SetChoices()
    {
        yield return null;
        _slider.maxValue = _partChoices.Count;
        OnSetChoices.Invoke(_partChoices);
        yield return null;
        OnSetGraphics.Invoke(_partChoices.Count);
        yield return null;
        OnLinkChildren.Invoke();
    }
}
