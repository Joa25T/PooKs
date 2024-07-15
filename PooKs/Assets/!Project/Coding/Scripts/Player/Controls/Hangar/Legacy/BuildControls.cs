/*using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildControls : Controls
{
    //private HangarControls _hangarControls;
    private Vector2 _lastMousePos;
    
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _building;
    [SerializeField] private LayerMask _baseColliderLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _minDistanceBetweenPlatforms = 10;

    private void OnEnable()
    {
        OnCall();
        basePlayerInputs.Building.FindPos.performed += PositionWithMouse;
        basePlayerInputs.Building.MoveBuilding.performed += MoveBuilding;
        //basePlayerInputs.Buildings.Confirm.performed += Confirm;
        //_hangarControls = GetComponent<HangarControls>();
    }

    private void OnDisable()
    {
        basePlayerInputs.Building.FindPos.performed -= PositionWithMouse;
        basePlayerInputs.Building.MoveBuilding.performed -= MoveBuilding;
    }

    public void OnCall()
    {
        EnableBuild();
    }

    private void MoveBuilding(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();
        Debug.Log(inputDir);
        _building.transform.position = _building.transform.position.AddValue(Axis.X, inputDir.x);
        if (inputDir.y == 0)
        {
            _building.transform.position = _building.transform.position.AddValue(Axis.Y, 1); // raising the raycastpoint a bit, so it doesn't ignore the fist floor its colliding with 
            _building.transform.position = _building.transform.position.RayHitPosition(_groundLayer, Vector3.down);
            return;
        }
        if (inputDir.y > 0)
        {
            _building.transform.position = _building.transform.position.AddValue(Axis.Y, _minDistanceBetweenPlatforms); // raising the raycastpoint above the next platform to hit the top of the collider
        }
        _building.transform.position = _building.transform.position.RayHitPosition(_groundLayer, Vector3.down);
        _building.transform.position = _building.transform.position.Int(Axis.X);
    }

    private void PositionWithMouse(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Ray mouseRay = _mainCamera.ScreenPointToRay(value);
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 200f, _baseColliderLayer))
        {
            _building.transform.position = hitInfo.point.RayHitPosition(_groundLayer, Vector3.down);
            _building.transform.position = _building.transform.position.Int(Axis.X);
        }
    }
    
    private void Confirm(InputAction.CallbackContext context)
    {
        //_hangarControls.enabled = true;
        EnableRun();
        this.enabled = false;
    }
}*/