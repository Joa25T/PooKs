using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public void Awake()
    {
        // register to a generic event
        
    }

    public void OpenUI(Panel panel)
    {
        panel.gameObject.SetActive(true);
        panel.OnOpen();
    }
}