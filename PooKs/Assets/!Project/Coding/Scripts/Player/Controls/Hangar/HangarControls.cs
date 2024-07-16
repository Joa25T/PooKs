using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Update = UnityEngine.PlayerLoop.Update;

[RequireComponent(typeof(Rigidbody))]
public class HangarControls : Controls
{
    private InputAction _moveInput;
    private InputAction _lookInput;
    private Rigidbody _rb;

    private Collider _collider;
    private float XValue => _moveInput.ReadValue<Vector2>().x * (_speed * Time.deltaTime);
    private float YValue => _moveInput.ReadValue<Vector2>().y * (_speed * Time.deltaTime);

    [SerializeField] private float _speed = 500;
    [SerializeField] private bool _touchingLadder;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _rb.velocity.Switch(Axis.X, XValue);
    }
    
    private void OnTriggerStay(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable.IsLadder)
        {
            _rb.velocity = _rb.velocity.Switch(Axis.Y, YValue);
        }
    }


    private void HangarFire(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar fire called");
    }
    
    private void HangarInteract(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar interact called");
    }

    private void HangarCancel(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar cancel called");
        // call an event that changes the controls
    }
    
    public void EnableControls()
    {
        EnableHangar();
        _moveInput = basePlayerInputs.Hangar.HangarMove;
        _moveInput.Enable();
        _lookInput = basePlayerInputs.Hangar.HangarLook;
        _lookInput.Enable();
        basePlayerInputs.Hangar.HangarInteract.performed += HangarInteract;
        basePlayerInputs.Hangar.HangarInteract.Enable();
        basePlayerInputs.Hangar.HangarCancel.performed += HangarCancel;
        basePlayerInputs.Hangar.HangarCancel.Enable();
        basePlayerInputs.Hangar.HangarFire.performed += HangarFire;
        basePlayerInputs.Hangar.HangarFire.Enable();
    }
    
    public void DisableControls()
    {
        _moveInput.Disable();
        _lookInput.Disable();
        basePlayerInputs.Hangar.HangarInteract.performed -= HangarInteract;
        basePlayerInputs.Hangar.HangarInteract.Disable();
        basePlayerInputs.Hangar.HangarCancel.performed -= HangarCancel;
        basePlayerInputs.Hangar.HangarCancel.Disable();
        basePlayerInputs.Hangar.HangarFire.performed -= HangarFire;
        basePlayerInputs.Hangar.HangarFire.Disable();
    }
}