using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class WHYCANTMOVE : NetworkBehaviour
{
    private float _moveInputX;
    private float _moveInputY;

    private void Update()
    {
        transform.position +=  (Vector3.up * _moveInputY) + (Vector3.right * _moveInputX);
    }

    public void OnHangarMove(InputAction.CallbackContext context)
    {
        _moveInputX = Mathf.Round(context.ReadValue<Vector2>().x);
        _moveInputY = Mathf.Round(context.ReadValue<Vector2>().y);
    }
}
