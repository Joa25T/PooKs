using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BuildControls))]
public class BaseControls : Controls
{
    private Vector2 _lastMousePos;
    private BuildControls _buildControls;
    private BaseControls _baseControls;
    
    private void OnEnable()
    {
        _buildControls = GetComponent<BuildControls>();
        _buildControls.enabled = false;
        basePlayerInputs.Player.Fire.performed += Interact;
        EnablePlayer();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        basePlayerInputs.Player.Fire.performed -= Interact;
        _buildControls.enabled = true;
        _buildControls.OnCall();
        this.enabled = false;
    }
    
}
