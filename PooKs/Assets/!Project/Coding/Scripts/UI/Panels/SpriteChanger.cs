using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PooKs.UI
{
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Image _targetImage;
        private List<Sprite> _sprites = new List<Sprite>();
        
        public void OnSetSprites(List<Part> parts)
        {
            _sprites.Clear();
            foreach (Part part in parts)
            {
                _sprites.Add(part.Sprite);
            }
        }
        public void SpriteChange(float i)
        {
            if (_targetImage == null) return;
            _targetImage.sprite = _sprites[(int)i - 1];
        }
    }
}