using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class HangarControls : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    [SerializeField] private float _speed = 500;
    [SerializeField] private float _gravity = 20f;
    //[SerializeField] private float _interactionCooldown = 0.5f;

    private float _moveInputX;
    private float _moveInputY;
    private IInteractable _interactable;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("Hangar");
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _rb.velocity.Switch(Axis.X, _moveInputX.SpeedWithDeltaTime(_speed));
        _rb.AddForce(new Vector3(0,-_gravity,0));
    }

    private void Update()
    {
        _rb.AddForce(Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        _interactable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        _interactable = null;
    }

    public void OnHangarMove(InputAction.CallbackContext context)
    {
        _moveInputX = Mathf.Round(context.ReadValue<Vector2>().x);
        _moveInputY = Mathf.Round(context.ReadValue<Vector2>().y);
    }

    public void OnHangarInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _interactable?.OnInteract(_moveInputY, this.gameObject);
    }
    
    private void HangarFire(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar fire called");
    }
    
    private void HangarCancel(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar cancel called");
        // call an event that changes the controls
    }
}