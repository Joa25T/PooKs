using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class HangarControls : NetworkBehaviour
{
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    [SerializeField] private float _speed = 500;
    //[SerializeField] private float _maxSpeed = 25f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] [ReadOnlyInspector] private SO_PlayerCharacter _playerCharacter;

    private float _moveInputX;
    private float _moveInputY;
    private IInteractable _interactable;

    public UnityEvent<SO_PlayerCharacter> AssignedPC;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("Hangar");
        AssignPC();
    }
    
    private void Update()
    {
        if(!IsOwner) return;
        transform.position += Vector3.right * (_moveInputX * _speed * Time.fixedDeltaTime);
        //_rb.AddForce(Vector3.right * (_moveInputX * _speed * Time.fixedDeltaTime));
        _rb.AddForce(Vector3.down * _gravity);
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
        _interactable?.OnInteract(_moveInputY, _playerCharacter, this.transform);
    }
    
    public void HangarFire(InputAction.CallbackContext obj)
    {
    }
    
    public void HangarCancel(InputAction.CallbackContext obj)
    {
        Debug.Log("Hangar cancel called");
        // call an event that changes the controls
    }

    public void AssignPC()
    {
        _playerCharacter = ScriptableObject.CreateInstance<SO_PlayerCharacter>();
        _playerCharacter.SetID(_playerInput.playerIndex);
        AssignedPC.Invoke(_playerCharacter);
    }
}