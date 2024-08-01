using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PooKs.UI
{
    public class AutoSelect : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(SelectThis());
        }

        private IEnumerator SelectThis()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }
}