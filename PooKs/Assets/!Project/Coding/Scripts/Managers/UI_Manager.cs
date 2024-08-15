using UnityEngine;

namespace PooKs.UI
{
    public class UI_Manager : MonoBehaviour
    {
        public void OpenUI(Panel panel, SO_PlayerCharacter interactingPC)
        {
            panel.gameObject.SetActive(true);
            panel.OnOpen(interactingPC);
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