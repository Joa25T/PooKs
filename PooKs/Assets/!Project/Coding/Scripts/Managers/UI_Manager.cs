using UnityEngine;

namespace PooKs.UI
{
    public class UI_Manager : MonoBehaviour
    {
        public void OpenUI(Panel panel)
        {
            panel.gameObject.SetActive(true);
            panel.OnOpen();
        }

        public void CloseUI()
        {
            Panel[] panels = GetComponentsInChildren<Panel>();
            foreach (Panel panel in panels)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
}