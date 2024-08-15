using UnityEngine;
using UnityEngine.Events;

public class TestingEvents : MonoBehaviour , IInteractable
{
    public UnityEvent<bool> testSceneChange;
    public void OnInteract(float dir, SO_PlayerCharacter playerCharacter, Transform playerPos)
    {
        testSceneChange.Invoke(true);
    }

#if DEVELOPMENT_BUILD
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.LogError("Opening Console");
        }
    }
#endif
}
