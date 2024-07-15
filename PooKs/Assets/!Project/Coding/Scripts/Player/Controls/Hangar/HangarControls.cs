using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HangarControls : Controls
{
    private InputAction _moveInput;
    private InputAction _lookInput;
    private Rigidbody _rb;

    [SerializeField] private float _speed = 500;
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }


    private void Update()
    {
        float XValue = _moveInput.ReadValue<Vector2>().x * (_speed * Time.deltaTime);
        _rb.velocity = _rb.velocity.Switch(Axis.X, XValue);
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


