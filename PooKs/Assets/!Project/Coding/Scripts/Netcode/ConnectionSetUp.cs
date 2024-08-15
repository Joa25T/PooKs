using Unity.Netcode;
using UnityEngine;

public class ConnectionSetUp : MonoBehaviour
{
    public void StartAsClient()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
    } 
    
    public void StartAsHost()
    {
        NetworkManager.Singleton.StartHost();
        Hide();
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
