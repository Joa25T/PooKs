using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ControlManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
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
