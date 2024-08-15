using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class ControlManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        int sceneID = SceneManager.GetActiveScene().handle;
        switch (sceneID)
        {
            case 0:
                OnResumeEris();
                break;
            case 1:
                OnResumeRun();
                break;
            default:
                OnResumeEris();
                break;
        }
    }

    public void OnUIOpen()
    {
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnResumeEris()
    {
        _playerInput.SwitchCurrentActionMap("Hangar");
    }

    public void OnResumeRun()
    {
        _playerInput.SwitchCurrentActionMap("Run");
    }
}
