using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PooKs.UI
{
    public class TextChanger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;
        [SerializeField] private SO_PartsList _partsList;
        private enum TextType {name, description}

        [SerializeField] private TextType _textType;
        

        public void ChangeText(float i)
        {
            if (_tmp == null) return;
            switch (_textType)
            {
                case TextType.name:
                    _tmp.text = _partsList.partList[(int)i].Name;
                    break;
                case TextType.description:
                    _tmp.text = _partsList.partList[(int)i].Description;
                    break;
            }
        }
    }
}
