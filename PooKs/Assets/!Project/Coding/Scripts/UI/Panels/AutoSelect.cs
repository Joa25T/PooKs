using UnityEngine;
using UnityEngine.EventSystems;

public class AutoSelect : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
