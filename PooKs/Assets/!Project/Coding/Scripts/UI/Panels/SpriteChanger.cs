using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PooKs.UI
{
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private SO_PartsList _partsList;
        
        public void SpriteChange(float i)
        {
            if (_targetImage == null) return;
            _targetImage.sprite = _partsList.partList[(int)i].Sprite;
        }
    }
}