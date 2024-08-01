using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PooKs.UI
{
    public class SliderGraphicSetter : MonoBehaviour
    {

        [Tooltip("The image rect should be on center")] 
        [SerializeField] private Image _sprite;

        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _handleSlideArea;
        [SerializeField] private List<Image> _spriteList;

        public void OnSetChoices(int value)
        {
            SetImages(value);
            CalculateSpacing(value);
        }

        private void SetImages(int value)
        {
            int count = value;
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
                        new Vector2(_rectTransform.rect.height-.5f, _rectTransform.rect.height-.5f);
                }
            }
        }

        private void CalculateSpacing(int value)
        {
            _horizontalLayoutGroup.spacing =
                (_rectTransform.rect.width / value) - _rectTransform.rect.height;
            _handleSlideArea.sizeDelta = new Vector2(-((_rectTransform.rect.height) + _horizontalLayoutGroup.spacing),
                0);
        }
    }
}