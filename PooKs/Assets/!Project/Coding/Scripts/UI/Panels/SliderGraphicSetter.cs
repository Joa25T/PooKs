using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGraphicSetter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [Tooltip("The image rect should be on center")]
    [SerializeField] private Image _sprite;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _handleSlideArea;
    [SerializeField] private List<Image> _spriteList;

    private void OnEnable()
    {
        SetImages();
        CalculateSpacing();
    }
    private void SetImages()
    {
        int count = (int)_slider.maxValue;
        while (_spriteList.Count != count)
        {
            if (_spriteList.Count > count)
            {
                foreach (Image sprite in _spriteList)
                {
                    Destroy(sprite.gameObject);
                }
                _spriteList.Clear();
            }
            for (int i = 0; i < count; i++)
            {
                _spriteList.Add(Instantiate(_sprite, this.transform));
                _spriteList[i].rectTransform.sizeDelta =
                    new Vector2(_rectTransform.rect.height, _rectTransform.rect.height);
            }
        }
    }
    private void CalculateSpacing()
    {
        _horizontalLayoutGroup.spacing = (_rectTransform.rect.width / _slider.maxValue) - _rectTransform.rect.height;
        _handleSlideArea.sizeDelta = new Vector2(-((_rectTransform.rect.height) + _horizontalLayoutGroup.spacing),
            0);
    }
}