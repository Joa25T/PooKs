using UnityEngine.InputSystem;

public interface IActions
{
    void Move(InputAction.CallbackContext context);
    void Look(InputAction.CallbackContext context);
    void Fire(InputAction.CallbackContext context);
    void Fire2(InputAction.CallbackContext context);
    void Repair(InputAction.CallbackContext context);
    void StarPower(InputAction.CallbackContext context);
    void Interact(InputAction.CallbackContext context);
    void Melee(InputAction.CallbackContext context);
    void Reload(InputAction.CallbackContext context);
    void Defense(InputAction.CallbackContext context);
}

public interface IBaseActions : IActions
{
    
}