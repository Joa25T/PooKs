using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PooKs.UI
{
    public class TextChanger : MonoBehaviour
    {
        private enum TextType {name, description}
        
        [SerializeField] private TextMeshProUGUI _tmp;
        private List<string> _texts = new List<string>();

        [SerializeField] private TextType _textType; 
        
        public void OnSetTexts(List<Part> bodies)
        {
            _texts.Clear();
            switch (_textType)
            {
                case TextType.name :
                    foreach (Part part in bodies)
                    {
                        _texts.Add(part.Name);
                    }
                    break;
                case TextType.description :
                    foreach (Part part in bodies)
                    {
                        _texts.Add(part.Description);
                    }
                    break;
            }
        }

        public void ChangeText(float i)
        {
            if (_tmp == null) return;
            _tmp.text = _texts[(int)i-1]; 
        }
    }
}

