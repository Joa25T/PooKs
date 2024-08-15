using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_T : MonoBehaviour
{
    public void OnSceneChange()
    {
        SceneManager.LoadScene(1);
    }
}
